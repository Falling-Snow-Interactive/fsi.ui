using Fsi.Ui.Inputs.Informations;
using UnityEditor;
using UnityEngine;

namespace Fsi.Ui.Inputs.Settings
{
    public class UiSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/UiSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static UiSettings _settings;
        public static UiSettings Settings => _settings ??= GetOrCreateSettings();
        
        // Icons
        [SerializeField]
        private PromptInformationGroup xbox;
        public PromptInformationGroup Xbox => xbox;
        
        [SerializeField]
        private PromptInformationGroup playStation;
        public PromptInformationGroup PlayStation => playStation;

        [SerializeField]
        private PromptInformationGroup switchPro;
        public PromptInformationGroup SwitchPro => switchPro;
        
        [SerializeField]
        private PromptInformationGroup switchJoy;
        public PromptInformationGroup SwitchJoy => switchJoy;

        [SerializeField]
        private PromptInformationGroup steam;
        public PromptInformationGroup Steam => steam;

        [SerializeField]
        private PromptInformationGroup mouseKeyboard;
        public PromptInformationGroup MouseKeyboard => mouseKeyboard;

        [SerializeField]
        private PromptInformationGroup touch;
        public PromptInformationGroup Touch => touch;
        
        // Debugging
        [SerializeField]
        private bool logs = false;
        
        [SerializeField]
        private bool warnings = false;
        
        [SerializeField]
        private bool errors = false;
        
        [SerializeField]
        private bool events = false;

        #region Settings

        public static UiSettings GetOrCreateSettings()
        {
            UiSettings settings = Resources.Load<UiSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!settings)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                {
                    AssetDatabase.CreateFolder("Assets", "Resources");
                }

                if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
                {
                    AssetDatabase.CreateFolder("Assets/Resources", "Settings");
                }

                settings = CreateInstance<UiSettings>();
                AssetDatabase.CreateAsset(settings, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return settings;
        }

        #if UNITY_EDITOR
        public static SerializedObject GetSerializedSettings()
        {
            return new(GetOrCreateSettings());
        }
        #endif

        #endregion
        
        #region Logging
        
        public static void Log(string message, GameObject gameObject = null)
        {
            if (Settings.logs)
            {
                Debug.Log($"Ui | {message}", gameObject);
            }
        }

        public static void LogWarning(string message, GameObject gameObject = null)
        {
            if (Settings.warnings)
            {
                Debug.LogWarning($"Ui | {message}", gameObject);
            }
        }

        public static void LogError(string message, GameObject gameObject = null)
        {
            if (Settings.errors)
            {
                Debug.LogError($"Ui | {message}", gameObject);
            }
        }

        public static void LogEvent(string message, GameObject gameObject = null)
        {
            if (Settings.events)
            {
                Debug.Log($"Ui | {message}", gameObject);
            }
    }
        
        #endregion
    }
}