using Fsi.Ui.Buttons;
using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.FsiSelectables
{
    [CustomEditor(typeof(FsiButton))]
    public class FsiButtonEditor : ButtonEditor
    {
        private FsiButton fsiButton;
        
        public override VisualElement CreateInspectorGUI()
        {
            fsiButton = target as FsiButton;
            
            // Update properties when opening inspector.
            fsiButton?.SetBaseColors();
            
            VisualElement root = new();
            
            root.Add(base.CreateInspectorGUI());
            
            #region Script
            
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            PropertyField scriptField = new(scriptProp){ enabledSelf = false };
            root.Add(scriptField);
            root.Add(Spacer.BlankWide);
            
            #endregion
            
            #region Properties
            root.Add(CreatePropertiesCategory());
            #endregion
            
            #region Button
            root.Add(CreateButtonCategory());
            #endregion
            
            #region Visuals
            root.Add(CreateVisualsCategory());
            #endregion

            #region References
            root.Add(CreateReferencesCategory());
            #endregion
            
            return root;
        }

        protected virtual VisualElement CreatePropertiesCategory()
        {
            return new();
        }

        protected virtual VisualElement CreateButtonCategory()
        {
            VisualElement root = new();

            root.Add(Spacer.Wide);
            
            Label label = LabelUtility.Category("Button");
            root.Add(label); 

            SerializedProperty interactableProp = serializedObject.FindProperty("m_Interactable");
            PropertyField interactableField = new(interactableProp);
            root.Add(interactableField);
            
            SerializedProperty navigationProp = serializedObject.FindProperty("m_Navigation");
            PropertyField navigationField = new(navigationProp);
            root.Add(navigationField);
            
            SerializedProperty clickProp = serializedObject.FindProperty("m_OnClick");
            PropertyField clickField = new(clickProp);
            root.Add(clickField);
            
            return root;
        }

        protected virtual VisualElement CreateVisualsCategory()
        {
            VisualElement root = new();
            
            root.Add(Spacer.Wide);
            
            Label label = LabelUtility.Category("Visuals");
            root.Add(label);
            
            SerializedProperty colorsProp = serializedObject.FindProperty("colors");
            PropertyField colorsField = new(colorsProp);
            root.Add(colorsField);
            
            // Colors
            Foldout buttonStateColors = new(){ text = "Button State Colors" , value = EditorPrefs.GetBool("FsiButton.ButtonStateColors") };
            buttonStateColors.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.ButtonStateColors", evt.newValue));
            root.Add(buttonStateColors);
            
            SerializedProperty normalProp = serializedObject.FindProperty(nameof(fsiButton.normal));
            PropertyField normalField = new(normalProp){name = "normal_field"};
            buttonStateColors.Add(normalField);
            
            SerializedProperty highlightedProp = serializedObject.FindProperty(nameof(fsiButton.highlighted));
            PropertyField highlightedField = new(highlightedProp){name = "highlighted_field"};
            buttonStateColors.Add(highlightedField);
            
            SerializedProperty pressedProp = serializedObject.FindProperty(nameof(fsiButton.pressed));
            PropertyField pressedField = new(pressedProp){name = "pressed_field"};
            buttonStateColors.Add(pressedField);
            
            SerializedProperty selectedProp = serializedObject.FindProperty(nameof(fsiButton.selected));
            PropertyField selectedField = new(selectedProp){name = "selected_field"};
            buttonStateColors.Add(selectedField);
            
            SerializedProperty disabledProp = serializedObject.FindProperty(nameof(fsiButton.disabled));
            PropertyField disabledField = new(disabledProp){name = "disabled_field"};
            buttonStateColors.Add(disabledField);

            return root;
        }

        protected virtual VisualElement CreateReferencesCategory()
        {
            VisualElement root = new();
            
            root.Add(Spacer.Wide);
            
            Label label = LabelUtility.Category("References");
            root.Add(label);
            
            SerializedProperty backgroundsProp = serializedObject.FindProperty(nameof(fsiButton.backgrounds));
            PropertyField backgroundsField = new(backgroundsProp);
            root.Add(backgroundsField);
            
            SerializedProperty primaryAccentsProp = serializedObject.FindProperty(nameof(fsiButton.primaryAccents));
            PropertyField primaryAccentsField = new(primaryAccentsProp);
            root.Add(primaryAccentsField);
            
            SerializedProperty secondaryAccentsProp = serializedObject.FindProperty(nameof(fsiButton.secondaryAccents));
            PropertyField secondaryAccentsField = new(secondaryAccentsProp);
            root.Add(secondaryAccentsField);

            return root;
        }
    }
}