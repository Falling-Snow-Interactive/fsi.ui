using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.ColorPalettes
{
    [CustomPropertyDrawer(typeof(ColorModifier))]
    public class ColorModifierDrawer : PropertyDrawer
    {
        private SerializedProperty hAddProp;
        private SerializedProperty hModProp;
        private SerializedProperty sAddProp;
        private SerializedProperty sModProp;
        private SerializedProperty vAddProp;
        private SerializedProperty vModProp;
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            Foldout root = new(){ text = property.displayName };
            
            SerializedProperty hAddProp = property.FindPropertyRelative("hAdd");
            SerializedProperty hModProp = property.FindPropertyRelative("hMod");
            SerializedProperty sAddProp = property.FindPropertyRelative("sAdd");
            SerializedProperty sModProp = property.FindPropertyRelative("sMod");
            SerializedProperty vAddProp = property.FindPropertyRelative("vAdd");
            SerializedProperty vModProp = property.FindPropertyRelative("vMod");
            
            PropertyField hAddField = new(hAddProp);
            PropertyField hModField = new(hModProp);
            PropertyField sAddField = new(sAddProp);
            PropertyField sModField = new(sModProp);
            PropertyField vAddField = new(vAddProp);
            PropertyField vModField = new(vModProp);
            
            root.Add(hAddField);
            root.Add(hModField);
            root.Add(sAddField);
            root.Add(sModField);
            root.Add(vAddField);
            root.Add(vModField);
            
            return root;
        }
    }
}