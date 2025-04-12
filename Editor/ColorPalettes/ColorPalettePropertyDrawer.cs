using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.ColorPalettes
{
    // [CustomEditor(typeof(ColorPalette))]
    public class ColorPalettePropertyDrawer : Editor
    {
        private PropertyField colorField;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            
            SerializedProperty colorProp = serializedObject.FindProperty("color");
            SerializedProperty normalProp = serializedObject.FindProperty("normal");
            SerializedProperty selectedProp = serializedObject.FindProperty("selected");
            SerializedProperty disabledProp = serializedObject.FindProperty("disabled");
            SerializedProperty clickedProp = serializedObject.FindProperty("clicked");

            colorField = new(colorProp);
            root.Add(colorField);
            
            root.Add(Spacer.Wide());

            Foldout normalFold = new()
                                 {
                                     text = "Normal",
                                     value = false,
                                 };
            root.Add(normalFold);
            
            VisualElement normalSection = MakeColorSection(colorProp.colorValue, normalProp);
            normalFold.Add(normalSection);
            
            Foldout selectedFold = new()
                                 {
                                     text = "Selected",
                                     value = false,
                                 };
            // selectedFold.
            root.Add(selectedFold);
            
            VisualElement selectedSection = MakeColorSection(colorProp.colorValue, selectedProp);
            selectedFold.Add(selectedSection);
            
            Foldout disabledFold = new()
                                   {
                                       text = "Disabled",
                                       value = false,
                                   };
            root.Add(disabledFold);
            
            VisualElement disabledSection = MakeColorSection(colorProp.colorValue, disabledProp);
            disabledFold.Add(disabledSection);
            
            Foldout clickedFold = new()
                                   {
                                       text = "Clicked",
                                       value = false,
                                   };
            root.Add(clickedFold);
            
            VisualElement clickedSection = MakeColorSection(colorProp.colorValue, clickedProp);
            clickedFold.Add(clickedSection);

            return root;
        }

        private VisualElement MakeColorSection(Color color, SerializedProperty property)
        {
            VisualElement root = new();
            
            // buttons
            Foldout buttonsFold = new() { text = "Buttons" };
            root.Add(buttonsFold);
            
            SerializedProperty buttonsProp = property.FindPropertyRelative("buttons");

            SerializedProperty buttonsBgProp = buttonsProp.FindPropertyRelative("background");
            VisualElement bgModifier = MakeColorModifier(color, buttonsBgProp);
            buttonsFold.Add(bgModifier);
            
            buttonsFold.Add(Spacer.Wide());
            
            SerializedProperty buttonsOutlineProp = buttonsProp.FindPropertyRelative("outline");
            VisualElement outlineModifier = MakeColorModifier(color, buttonsOutlineProp);
            buttonsFold.Add(outlineModifier);
            
            buttonsFold.Add(Spacer.Wide());
            
            SerializedProperty buttonsAccentProp = buttonsProp.FindPropertyRelative("accent");
            VisualElement accentModifier = MakeColorModifier(color, buttonsAccentProp);
            buttonsFold.Add(accentModifier);
            
            return root;
        }

        private VisualElement MakeColorModifier(Color color, SerializedProperty property)
        {
            VisualElement root = new();
            
            ColorField colorField = new()
                                    {
                                        label = "",
                                        value = GetColor(color, property),
                                        // enabledSelf = false,
                                    };
            PropertyField propertyField = new(property);
            propertyField.RegisterValueChangeCallback(evt =>
                                                            {
                                                                colorField.value = GetColor(color, property);
                                                                serializedObject.ApplyModifiedProperties();
                                                                
                                                                SceneView.RepaintAll();
                                                            });
            
            root.Add(colorField);
            root.Add(propertyField);
            
            this.colorField.RegisterValueChangeCallback(evt =>
                                                        {
                                                            colorField.value = GetColor(color, property);
                                                            serializedObject.ApplyModifiedProperties();
                                                            SceneView.RepaintAll();
                                                        });
            
            return root;
        }
        
        private Color GetColor(Color color, SerializedProperty modifierProp)
        {
            float hAdd = modifierProp.FindPropertyRelative("hAdd").floatValue;
            float hMod = modifierProp.FindPropertyRelative("hMod").floatValue;
            float sAdd = modifierProp.FindPropertyRelative("sAdd").floatValue;
            float sMod = modifierProp.FindPropertyRelative("sMod").floatValue;
            float vAdd = modifierProp.FindPropertyRelative("vAdd").floatValue;
            float vMod = modifierProp.FindPropertyRelative("vMod").floatValue;

            Color.RGBToHSV(color, out float h, out float s, out float v);
            
            h += hAdd;
            s += sAdd;
            v += vAdd;
            
            h *= hMod;
            s *= sMod;
            v *= vMod;
            
            Color c = Color.HSVToRGB(h, s, v);
            return c;
        }
    }
}