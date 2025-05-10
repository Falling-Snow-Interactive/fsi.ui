using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Settings
{
    public static class InputSettingsProvider
    {
        private static SerializedObject settingsProp;
        
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fsi/Ui", SettingsScope.Project)
                                        {
                                            label = "Ui",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            settingsProp = InputSettings.GetSerializedSettings();

            ScrollView scrollView = new();
            root.Add(scrollView);

            Label title = LabelUtility.Title("Ui Settings");
            scrollView.Add(title);
            
            #region Input Schemes
            
            scrollView.Add(Spacer.Wide);
            scrollView.Add(CreateSchemeCategory());
            
            #endregion
            
            #region Prompts
            
            scrollView.Add(Spacer.Wide);
            scrollView.Add(CreatePromptCategory());
            
            #endregion
            
            root.Bind(settingsProp);
        }

        private static VisualElement CreateSchemeCategory()
        {
            VisualElement root = new();
            
            Label title = LabelUtility.Category("Input Schemes");
            root.Add(title);
            
            SerializedProperty schemeProp = settingsProp.FindProperty("schemeInformation");
            PropertyField schemeField = new(schemeProp);
            root.Add(schemeField);

            return root;
        }

        private static VisualElement CreatePromptCategory()
        {
            VisualElement root = new();
            
            Label title = LabelUtility.Category("Prompts");
            root.Add(title);
            
            SerializedProperty promptProp = settingsProp.FindProperty("promptInformation");
            PropertyField promptField = new(promptProp);
            root.Add(promptField);

            return root;
        }
    }
}