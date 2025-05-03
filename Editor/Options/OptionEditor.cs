using Fsi.Ui.Selectables;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Options
{
    [CustomEditor(typeof(Option), true)]
    public class OptionEditor : FsiSelectableEditor
    {
        private Option option;
        
        public override VisualElement CreateInspectorGUI()
        {
            option = target as Option;
            return base.CreateInspectorGUI();
        }

        protected override VisualElement CreatePropertiesCategory()
        {
            VisualElement root = base.CreatePropertiesCategory();
            
            SerializedProperty locProp = serializedObject.FindProperty(nameof(option.titleLoc));
            PropertyField locField = new(locProp);
            root.Add(locField);
            
            return root;
        }

        protected override VisualElement CreateReferencesCategory()
        {
            VisualElement root = base.CreateReferencesCategory();

            SerializedProperty textProp = serializedObject.FindProperty(nameof(option.titleText));
            PropertyField textField = new(textProp);
            root.Add(textField);
            
            return root;
        }
    }
}