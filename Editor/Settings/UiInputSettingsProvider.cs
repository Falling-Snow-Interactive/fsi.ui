using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Settings
{
    public static class UiInputSettingsProvider
    {
        private static SerializedObject _settingsProp;
        
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
            _settingsProp = UiSettings.GetSerializedSettings();

            ScrollView scrollView = new();
            root.Add(scrollView);

            Label title = LabelUtility.Title("Ui Settings");
            scrollView.Add(title);
            
            scrollView.Add(Spacer.Wide);
            
            #region Input Schemes
            
            scrollView.Add(CreateSchemeCategory());
            scrollView.Add(Spacer.Wide);
            
            #endregion
            
            root.Bind(_settingsProp);
        }

        private static VisualElement CreateSchemeCategory()
        {
            VisualElement root = new();
            
            Label title = LabelUtility.Category("Input Schemes");
            root.Add(title);
            
            SerializedProperty schemeProp = _settingsProp.FindProperty("schemeInformation");
            PropertyField schemeField = new(schemeProp);
            root.Add(schemeField);

            return root;
        }
    }
}