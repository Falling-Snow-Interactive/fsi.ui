using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Buttons
{
    [CustomEditor(typeof(FsiButton), true)]
    public class FsiButtonEditor : ButtonEditor
    {
        protected FsiButton FsiButton { get; private set; }
        
        public override VisualElement CreateInspectorGUI()
        {
            if (target is FsiButton fsiButton)
            {
                FsiButton = fsiButton;

                VisualElement root = new();
                SerializedProperty script = serializedObject.FindProperty("m_Script");
                PropertyField scriptField = new(script) { enabledSelf = false };
                root.Add(scriptField);

                #region Base Button

                Foldout buttonFoldout = new() { text = "Button" };
                root.Add(buttonFoldout);
                buttonFoldout.Add(CreateButtonInspector());

                #endregion

                #region Colors

                root.Add(Spacer.Wide());
                root.Add(BuildColorsCategory());

                #endregion

                #region Input

                root.Add(Spacer.Wide());
                root.Add(CreateInputCategory());

                #endregion

                #region References

                root.Add(Spacer.Wide());
                root.Add(CreateReferencesCategory());

                #endregion

                return root;
            }

            return new Label("target is not type FsiButton");
        }

        protected virtual VisualElement CreateButtonInspector()
        {
            IMGUIContainer container = new(base.OnInspectorGUI);
            return container;
        }

        protected virtual VisualElement CreateReferencesCategory()
        {
            Foldout root = new() { text = "References", value = EditorPrefs.GetBool("FsiButton.ReferencesFold")  };
            
            // Text ref
            SerializedProperty textRefProp = serializedObject.FindProperty(nameof(FsiButton.textRef));//"textRef");
            PropertyField textRefField = new(textRefProp);
            
            root.Add(textRefField);
            
            return root;
        }

        protected virtual VisualElement BuildColorsCategory()
        {
            Foldout root = new() { text = "Colors", value = EditorPrefs.GetBool("FsiButton.ColorFold")};
            root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.ColorFold", root.value));

            SerializedProperty colorPaletteProp = serializedObject.FindProperty(nameof(FsiButton.selectionStateColors));
            PropertyField colorPaletteField = new(colorPaletteProp);
            root.Add(colorPaletteField);
            
            SerializedProperty colorPaletteReferences = serializedObject.FindProperty(nameof(FsiButton.colorPaletteReferences));
            PropertyField colorPaletteReferencesField = new(colorPaletteReferences);
            root.Add(colorPaletteReferencesField);

            return root;
        }

        protected virtual VisualElement CreateInputCategory()
        {
            Foldout root = new() { text = "Input", value = EditorPrefs.GetBool("FsiButton.InputFold") };
            root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.InputFold", root.value));

            SerializedProperty inputTypeProp = serializedObject.FindProperty(nameof(FsiButton.inputType));
            PropertyField inputTypeField = new(inputTypeProp);
            root.Add(inputTypeField);
            
            #region Submit

            Foldout promptFold = new() { text = "Submit", value = EditorPrefs.GetBool("FsiButton.SubmitFold")};
            promptFold.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.SubmitFold", promptFold.value));
            root.Add(promptFold);
            
            SerializedProperty showSubmitIconProp = serializedObject.FindProperty(nameof(FsiButton.showSubmitIcon));
            PropertyField showSubmitIconField = new(showSubmitIconProp);
            promptFold.Add(showSubmitIconField);

            SerializedProperty showOnSelectOnlyProp = serializedObject.FindProperty(nameof(FsiButton.showOnSelectOnly));
            PropertyField showOnSelectOnlyField = new(showOnSelectOnlyProp);
            promptFold.Add(showOnSelectOnlyField);
            
            SerializedProperty submitIconProp = serializedObject.FindProperty(nameof(FsiButton.submitIcon));
            PropertyField submitIconField = new(submitIconProp);
            promptFold.Add(submitIconField);
            
            #endregion
            
            #region Shortcut
            
            Foldout shortcutFold = new() { text = "Shortcut", value = EditorPrefs.GetBool("FsiButton.ShortcutFold")};
            shortcutFold.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.ShortcutFold", shortcutFold.value));
            root.Add(shortcutFold);
            
            SerializedProperty showShortcutIconProp = serializedObject.FindProperty(nameof(FsiButton.showShortcutIcon));
            PropertyField showShortcutIconField = new(showShortcutIconProp);
            shortcutFold.Add(showShortcutIconField);
            
            SerializedProperty shortcutActionProp = serializedObject.FindProperty(nameof(FsiButton.shortcutActionRef));
            PropertyField shortcutActionField = new(shortcutActionProp);
            shortcutFold.Add(shortcutActionField);

            SerializedProperty shortcutIconProp = serializedObject.FindProperty(nameof(FsiButton.shortcutIcon));
            PropertyField shortcutIconField = new(shortcutIconProp);
            shortcutFold.Add(shortcutIconField);

            #endregion

            return root;
        }
    }
}