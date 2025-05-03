using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Options
{
    [CustomEditor(typeof(OptionEnum<>))]
    public class OptionEnumEditor<T> : OptionEditor where T : Enum
    {
        private OptionEnum<T> optionEnum;
        
        public override VisualElement CreateInspectorGUI()
        {
            optionEnum = target as OptionEnum<T>;
            return base.CreateInspectorGUI();
        }

        protected override VisualElement CreatePropertiesCategory()
        {
            VisualElement root = base.CreatePropertiesCategory();

            SerializedProperty valueProp = serializedObject.FindProperty("value");
            PropertyField valueField = new PropertyField(valueProp);
            root.Add(valueField);
            
            return root;
        }

        protected override VisualElement CreateReferencesCategory()
        {
            VisualElement root = base.CreateReferencesCategory();
            
            SerializedProperty textProp = serializedObject.FindProperty(nameof(optionEnum.enumText));
            PropertyField textField = new(textProp);
            root.Add(textField);

            return root;
        }
    }
}