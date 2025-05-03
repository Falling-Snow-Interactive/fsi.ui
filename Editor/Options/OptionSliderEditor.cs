using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Options
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(OptionSlider))]
    public class OptionSliderEditor : OptionEditor
    {
        private OptionSlider optionSlider;
        
        public override VisualElement CreateInspectorGUI()
        {
            optionSlider = target as OptionSlider;
            return base.CreateInspectorGUI();
        }

        protected override VisualElement CreatePropertiesCategory()
        {
            VisualElement root = base.CreatePropertiesCategory();

            SerializedProperty valueProp = serializedObject.FindProperty(nameof(optionSlider.value));
            PropertyField valueField = new(valueProp);
            root.Add(valueField);
            
            return root;
        }

        protected override VisualElement CreateReferencesCategory()
        {
            VisualElement root = base.CreateReferencesCategory();
            
            SerializedProperty sliderProp = serializedObject.FindProperty(nameof(optionSlider.slider));
            PropertyField sliderField = new(sliderProp);
            root.Add(sliderField);
            
            SerializedProperty percentageProp = serializedObject.FindProperty(nameof(optionSlider.percent));
            PropertyField percentageField = new(percentageProp);
            root.Add(percentageField);
            
            return root;
        }
    }
}