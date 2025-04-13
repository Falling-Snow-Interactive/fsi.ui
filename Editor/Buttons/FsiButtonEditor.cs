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
        
        // Shortcut
        private SerializedProperty showShortcutIconProp;
        private SerializedProperty shortcutActionReferenceProp;
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
            showSubmitIconProp = serializedObject.FindProperty("showSubmitIcon");
            showOnSelectOnlyProp = serializedObject.FindProperty("showOnSelectOnly");
            submitIconProp = serializedObject.FindProperty("submitIcon");
            showShortcutIconProp = serializedObject.FindProperty("showShortcutIcon");
            shortcutActionReferenceProp = serializedObject.FindProperty("shortcutActionReference");
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
            
            #region Input

            Foldout inputFold = new() { text = "Input", value = EditorPrefs.GetBool("FsiButton.InputFold")};
            inputFold.RegisterValueChangedCallback(evt =>
                                                   {
                                                       EditorPrefs.SetBool("FsiButton.InputFold", inputFold.value);
                                                   });
            root.Add(inputFold);
            
            PropertyField inputTypeField = new(inputTypeProp);
            inputFold.Add(inputTypeField);
            
            PropertyField showSubmitIconField = new(showSubmitIconProp);
            inputFold.Add(showSubmitIconField);
            
            PropertyField showOnSelectOnlyField = new(showOnSelectOnlyProp)
                                                  {
                                                      enabledSelf = showSubmitIconProp.boolValue,
                                                  };
            inputFold.Add(showOnSelectOnlyField);
            
            showSubmitIconField.RegisterValueChangeCallback(evt =>
                                                            {
                                                                showOnSelectOnlyField.enabledSelf
                                                                    = showSubmitIconProp.boolValue;
                                                            });

            PropertyField submitIconField = new(submitIconProp);
            inputFold.Add(submitIconField);
            
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
            
            PropertyField shortcutActionReferenceField = new(shortcutActionReferenceProp);
            shortcutFold.Add(shortcutActionReferenceField);
            
            PropertyField shortcutIconField = new(shortcutIconProp);
            shortcutFold.Add(shortcutIconField);
            
            #endregion
            
            return root;
        }
    }
}