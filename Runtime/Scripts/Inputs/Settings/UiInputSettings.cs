using Fantazee.Input.Information;
using UnityEditor;
using UnityEngine;

namespace Fsi.Ui.Scripts.Inputs.Settings
{
    public class UiInputSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/UiInputSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static UiInputSettings _settings;
        public static UiInputSettings Settings => _settings ??= GetOrCreateSettings();
        
        // Icons
        [SerializeField]
        private InputInformationGroup xbox;
        public InputInformationGroup Xbox => xbox;
        
        [SerializeField]
        private InputInformationGroup playStation;
        public InputInformationGroup PlayStation => playStation;

        [SerializeField]
        private InputInformationGroup switchPro;
        public InputInformationGroup SwitchPro => switchPro;
        
        [SerializeField]
        private InputInformationGroup switchJoy;
        public InputInformationGroup SwitchJoy => switchJoy;

        [SerializeField]
        private InputInformationGroup steam;
        public InputInformationGroup Steam => steam;

        [SerializeField]
        private InputInformationGroup mouseKeyboard;
        public InputInformationGroup MouseKeyboard => mouseKeyboard;
        
        // Debugging
        [SerializeField]
        private bool logs = false;
        
        [SerializeField]
        private bool warnings = false;
        
        [SerializeField]
        private bool errors = false;

        #region Settings

        public static UiInputSettings GetOrCreateSettings()
        {
            UiInputSettings settings = Resources.Load<UiInputSettings>(ResourcePath);

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

                settings = CreateInstance<UiInputSettings>();
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

        public static void Log(string message, GameObject gameObject = null)
        {
            if (Settings.logs)
            {
                Debug.Log($"Ui Input: {message}", gameObject);
            }
        }

        public static void LogWarning(string message, GameObject gameObject = null)
        {
            if (Settings.warnings)
            {
                Debug.LogWarning($"Ui Input: {message}", gameObject);
            }
        }

        public static void LogError(string message, GameObject gameObject = null)
        {
            if (Settings.errors)
            {
                Debug.LogError($"Ui Input: {message}", gameObject);
            }
        }
    }
}