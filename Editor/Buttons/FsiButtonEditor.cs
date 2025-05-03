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
        // Serialized Properties
        // Color Palette
        private SerializedProperty colorPaletteProp;
        private SerializedProperty colorPaletteReferences;
        
        // Input
        private SerializedProperty inputTypeProp;
        private SerializedProperty showSubmitIconProp;
        private SerializedProperty showOnSelectOnlyProp;
        private SerializedProperty submitIconProp;
        private SerializedProperty submitActionProp;
        
        // Shortcut
        private SerializedProperty showShortcutIconProp;
        private SerializedProperty shortcutActionProp;
        private SerializedProperty shortcutIconProp;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new();
            root.Bind(serializedObject);
            
            // Base button inspector
            IMGUIContainer container = new(base.OnInspectorGUI);
            root.Add(container);
            
            root.Add(Spacer.Wide());
            
            #region Properties
            
            colorPaletteProp = serializedObject.FindProperty("colorPalette");
            colorPaletteReferences = serializedObject.FindProperty("colorPaletteReferences");
            
            inputTypeProp = serializedObject.FindProperty("inputType");
            showSubmitIconProp = serializedObject.FindProperty("showSubmitPrompt");
            showOnSelectOnlyProp = serializedObject.FindProperty("showOnSelectOnly");
            submitIconProp = serializedObject.FindProperty("submitPromptIcon");
            submitActionProp = serializedObject.FindProperty("submitActionRef");
            
            showShortcutIconProp = serializedObject.FindProperty("showShortcutIcon");
            shortcutActionProp = serializedObject.FindProperty("shortcutActionRef");
            shortcutIconProp = serializedObject.FindProperty("shortcutIcon");
            
            #endregion
            
            #region Color Palette
            
            Foldout colorFold = new() { text = "Color Palette", value = EditorPrefs.GetBool("FsiButton.ColorFold")};
            colorFold.RegisterValueChangedCallback(evt =>
                                                   {
                                                       EditorPrefs.SetBool("FsiButton.ColorFold", colorFold.value);
                                                   });
            root.Add(colorFold);

            PropertyField colorPaletteField = new(colorPaletteProp);
            colorFold.Add(colorPaletteField);
            
            PropertyField colorPaletteReferencesField = new(colorPaletteReferences);
            colorFold.Add(colorPaletteReferencesField);
            
            #endregion
            
            #region Prompt

            Foldout promptFold = new() { text = "Prompt", value = EditorPrefs.GetBool("FsiButton.PromptFold")};
            promptFold.RegisterValueChangedCallback(evt =>
                                                    {
                                                        EditorPrefs.SetBool("FsiButton.PromptFold", promptFold.value);
                                                    });
            root.Add(promptFold);
            
            PropertyField inputTypeField = new(inputTypeProp);
            promptFold.Add(inputTypeField);
            
            PropertyField showSubmitIconField = new(showSubmitIconProp);
            promptFold.Add(showSubmitIconField);
            
            PropertyField showOnSelectOnlyField = new(showOnSelectOnlyProp)
                                                  {
                                                      enabledSelf = showSubmitIconProp.boolValue,
                                                  };
            promptFold.Add(showOnSelectOnlyField);
            
            showSubmitIconField.RegisterValueChangeCallback(evt =>
                                                            {
                                                                showOnSelectOnlyField.enabledSelf = showSubmitIconProp.boolValue;
                                                            });

            PropertyField submitIconField = new(submitIconProp);
            promptFold.Add(submitIconField);
            
            PropertyField submitActionField = new(submitActionProp);
            promptFold.Add(submitActionField);
            
            #endregion
            
            #region Shortcut
            
            Foldout shortcutFold = new() { text = "Shortcut", value = EditorPrefs.GetBool("FsiButton.ShortcutFold")};
            shortcutFold.RegisterValueChangedCallback(evt =>
                                                      {
                                                          EditorPrefs.SetBool("FsiButton.ShortcutFold", shortcutFold.value);
                                                      });
            root.Add(shortcutFold);
            
            PropertyField showShortcutIconField = new(showShortcutIconProp);
            shortcutFold.Add(showShortcutIconField);
            
            PropertyField shortcutActionField = new(shortcutActionProp);
            shortcutFold.Add(shortcutActionField);
            
            PropertyField shortcutIconField = new(shortcutIconProp);
            shortcutFold.Add(shortcutIconField);
            
            #endregion
            
            return root;
        }
    }
}