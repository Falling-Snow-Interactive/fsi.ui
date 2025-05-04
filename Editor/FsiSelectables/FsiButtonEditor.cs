using Fsi.Ui.Buttons;
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
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            
            root.Add(base.CreateInspectorGUI());
            
            #region Script
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            PropertyField scriptField = new(scriptProp){ enabledSelf = false };
            root.Add(scriptField);
            root.Add(Spacer.BlankWide);
            #endregion
            
            #region Selectable
            
            root.Add(CreateButtonCategory());
            root.Add(Spacer.Wide);
            
            #endregion
            
            return root;
        }

        private VisualElement CreateButtonCategory()
        {
            VisualElement root = new();
            // Foldout root = new(){text = "Button", value = EditorPrefs.GetBool("FsiButton.ButtonFold")};
            // root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiSelectable.ButtonFold", evt.newValue));

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
    }
}