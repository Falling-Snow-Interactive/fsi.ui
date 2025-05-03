using Fsi.Ui.Inputs.Informations;
using UnityEditor;
using UnityEngine;
using Logger = fsi.settings.Logging.Logger;

namespace Fsi.Ui.Inputs.Settings
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "Fsi/Settings/InputSettings")]
    public class InputSettings : ScriptableObject
    {
        private const string AssetName = "InputSettings";
        private const string ResourcePath = $"Settings/{AssetName}";
        private const string FullPath = $"Assets/Resources/{ResourcePath}.asset";
        // private const string FolderPath = "Packages/com.fallingsnowinteractive.ui/Assets/Config";
        // private const string FilePath = $"{FolderPath}/{AssetName}.asset";

        private static InputSettings _settings;
        public static InputSettings Settings => _settings ??= GetOrCreateSettings();
        
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

        public static InputSettings GetOrCreateSettings()
        {
            // InputSettings settings = AssetDatabase.LoadAssetAtPath<InputSettings>(FilePath);
            InputSettings settings = Resources.Load<InputSettings>(ResourcePath);

            #if UNITY_EDITOR
            if (!settings)
            {
                settings = CreateInstance<InputSettings>();
                AssetDatabase.CreateAsset(settings, ResourcePath);
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