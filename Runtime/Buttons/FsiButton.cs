using System;
using System.Collections.Generic;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs;
using Fsi.Ui.Inputs.Ui.Prompts;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;
using InputSettings = Fsi.Ui.Inputs.Settings.InputSettings;

namespace Fsi.Ui.Buttons
{
    public class FsiButton : Button
    {
        public string Text
        {
            get => textRef ? textRef.text : "";
            set
            {
                if (textRef != null)
                {
                    textRef.text = value;
                }
            }
        }
        
        public TMP_Text textRef;
        
        // Colors
        [FormerlySerializedAs("buttonColors")]
        public SelectionStateColors selectionStateColors;
        public List<ColorPaletteReferences> colorPaletteReferences = new();
        
        #region Create Menu
        #if UNITY_EDITOR
        
        private const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Assets/Prefabs/Ui/Fsi_Button.prefab";
        
        [MenuItem("GameObject/Fsi/Ui/Button")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            GameObject button = parent 
                                ? PrefabUtility.InstantiatePrefab(asset, parent.transform) as GameObject
                                : PrefabUtility.InstantiatePrefab(asset) as GameObject;
            
            PrefabUtility.UnpackPrefabInstance(button, PrefabUnpackMode.OutermostRoot, InteractionMode.UserAction);
            // Object button = Instantiate(asset.gameObject, parent.transform);
            if (button != null)
            {
                button.name = "Fsi_Button";
                Selection.activeObject = button;
            }
        }
        
        #endif
        #endregion
        
        /*
        #region Input Prompts
        
        private void OnInputChanged()
        {
            inputType = InputController.Instance.InputType;
            if (submitIcon && InputController.Instance && InputController.Instance.SubmitRef)
            {
                submitIcon.SetBinding(inputType, InputController.Instance.SubmitRef);
            }

            if (shortcutIcon)
            {
                shortcutIcon.SetBinding(inputType, InputController.Instance.SubmitRef);
            }
        }
        
        #endregion
        */

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            if (selectionStateColors)
            {
                ColorGroup group = state switch
                                   {
                                       SelectionState.Normal => selectionStateColors.Normal,
                                       SelectionState.Highlighted => selectionStateColors.Highlighted,
                                       SelectionState.Pressed => selectionStateColors.Pressed,
                                       SelectionState.Selected => selectionStateColors.Selected,
                                       SelectionState.Disabled => selectionStateColors.Disabled,
                                       _ => throw new ArgumentOutOfRangeException()
                                   };

                foreach (var colorPaletteReference in colorPaletteReferences)
                {
                    colorPaletteReference.ApplyPalette(group, selectionStateColors.FadeDuration);
                }
            }
        }
    }
}