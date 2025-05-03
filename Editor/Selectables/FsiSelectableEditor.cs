using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Selectables
{
    [CustomEditor(typeof(FsiSelectable), true)]
    public class FsiSelectableEditor : SelectableEditor
    {
        private FsiSelectable selectable;
        
        public override VisualElement CreateInspectorGUI()
        {
            if (target is FsiSelectable fsiSelectable)
            {
                selectable = fsiSelectable;
            }

            VisualElement root = new();
            SerializedProperty script = serializedObject.FindProperty("m_Script");
            PropertyField scriptField = new(script) { enabledSelf = false };
            root.Add(scriptField);
            
            #region Properties

            root.Add(CreatePropertiesCategory());
            root.Add(Spacer.Wide());
            
            #endregion

            #region IMGUI Category

            Foldout imguiFoldout = new() { text = "Selectable" };
            root.Add(imguiFoldout);
            imguiFoldout.Add(CreateImguiInspector());

            #endregion
            
            #region Colors

            root.Add(Spacer.Wide());
            root.Add(BuildColorsCategory());

            #endregion
            
            #region References

            root.Add(Spacer.Wide());
            root.Add(CreateReferencesCategory());

            #endregion

            return root;
        }

        protected virtual VisualElement CreatePropertiesCategory()
        {
            return new VisualElement();
        }
        
        protected virtual VisualElement CreateImguiInspector()
        {
            IMGUIContainer container = new(base.OnInspectorGUI);
            return container;
        }
        
        protected virtual VisualElement BuildColorsCategory()
        {
            Foldout root = new() { text = "Colors", value = EditorPrefs.GetBool("FsiSelectable.ColorFold")};
            root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiSelectable.ColorFold", evt.newValue));

            SerializedProperty colorPaletteProp = serializedObject.FindProperty(nameof(selectable.buttonColors));
            PropertyField colorPaletteField = new(colorPaletteProp);
            root.Add(colorPaletteField);

            return root;
        }

        protected virtual VisualElement CreateReferencesCategory()
        {
            Foldout root = new() { text = "References", value = EditorPrefs.GetBool("FsiSelectable.References") };
            root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiSelectable.References", evt.newValue));

            SerializedProperty colorPaletteReferences = serializedObject.FindProperty(nameof(selectable.colorPaletteReferences));
            PropertyField colorPaletteReferencesField = new(colorPaletteReferences);
            root.Add(colorPaletteReferencesField);
            
            return root;
        }
    }
}