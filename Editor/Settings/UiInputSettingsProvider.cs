using Fsi.Ui.Inputs.Settings;
using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Settings
{
    public static class UiInputSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new("Fsi/Ui/Input", SettingsScope.Project)
                                        {
                                            label = "Input",
                                            activateHandler = OnActivate,
                                        };
            
            return provider;
        }

        private static void OnActivate(string searchContext, VisualElement root)
        {
            SerializedObject settingsProp = UiSettings.GetSerializedSettings();

            ScrollView scrollView = new();
            root.Add(scrollView);

            Label title = LabelUtility.Title("Ui Input Settings");
            scrollView.Add(title);
            
            scrollView.Add(Spacer.Wide());

            VisualElement infoContent = new();
            scrollView.Add(infoContent);

            Label infoLabel = LabelUtility.Section("Sprites");
            infoContent.Add(infoLabel);
            
            SerializedProperty mkProp = settingsProp.FindProperty("mouseKeyboard");
            PropertyField mkField = new(mkProp) { label = "Mouse & Keyboard", };
            infoContent.Add(mkField);
            
            SerializedProperty steamProp = settingsProp.FindProperty("steam");
            PropertyField steamField = new(steamProp){ label = "Steam" };
            infoContent.Add(steamField);
            
            SerializedProperty xboxProp = settingsProp.FindProperty("xbox");
            PropertyField xboxField = new(xboxProp){ label = "Xbox " };
            infoContent.Add(xboxField);
            
            SerializedProperty ps5Prop = settingsProp.FindProperty("playStation");
            PropertyField ps5Field = new(ps5Prop){ label = "PlayStation 5" };
            infoContent.Add(ps5Field);
            
            SerializedProperty proProp = settingsProp.FindProperty("switchPro");
            PropertyField proField = new(proProp){ label = "Switch Pro Controller" };
            infoContent.Add(proField);
            
            SerializedProperty joyProp = settingsProp.FindProperty("switchJoy");
            PropertyField joyField = new(joyProp){ label = "Switch Joycons" };
            infoContent.Add(joyField);
            
            scrollView.Add(Spacer.Wide());
            
            VisualElement debugContainer = new();
            scrollView.Add(debugContainer);
            
            Label debugLabel = LabelUtility.Section("Debugging");
            debugContainer.Add(debugLabel);
            
            SerializedProperty logProp = settingsProp.FindProperty("logger");
            PropertyField logField = new(logProp){ label = "Logger" };
            debugContainer.Add(logField);
            
            scrollView.Add(Spacer.Wide());
            
            root.Bind(settingsProp);
        }
    }
}