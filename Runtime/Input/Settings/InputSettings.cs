using Fsi.Ui.Input.Information;
using Fsi.Ui.Input.Prompts.Information;
using Fsi.Ui.Settings.SchemeInformations;
using UnityEditor;
using UnityEngine;

namespace Fsi.Ui.Input.Settings
{
    public class InputSettings : ScriptableObject
    {
        private const string ResourcePath = "Settings/UiSettings";
        private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

        private static InputSettings settings;
        public static InputSettings Settings => settings ??= GetOrCreateSettings();

        [SerializeField]
        private SchemeInformationGroup schemeInformation;
        public SchemeInformationGroup SchemeInformation => schemeInformation;

        [SerializeField]
        private InputInformationGroup inputInformation;
        public InputInformationGroup InputInformation => inputInformation;

        [SerializeField]
        private PromptInformationGroup promptInformation;
        public PromptInformationGroup PromptInformation => promptInformation;
        
        #region Settings

        private static InputSettings GetOrCreateSettings()
        {
            InputSettings settings = Resources.Load<InputSettings>(ResourcePath);

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

                settings = CreateInstance<InputSettings>();
                AssetDatabase.CreateAsset(settings, FullPath);
                AssetDatabase.SaveAssets();
            }
            #endif

            return settings;
        }

        #if UNITY_EDITOR
        
        public static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        #endif

        #endregion
    }
}