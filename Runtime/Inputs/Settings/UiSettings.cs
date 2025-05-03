using Fsi.Ui.Inputs.Informations;
using UnityEditor;
using UnityEngine;
using Logger = fsi.settings.Logging.Logger;

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
        private Logger logger;
        
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
        
        #region logging

        public static Logger Logger => Settings.logger;

        #endregion
    }
}