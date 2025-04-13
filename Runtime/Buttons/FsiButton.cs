using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.ColorPalettes.Animation.Timing;
using Fsi.Ui.Inputs;
using Fsi.Ui.Inputs.Settings;
using Fsi.Ui.Inputs.Ui;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

namespace Fsi.Ui.Buttons
{
    public abstract class FsiButton : Button //, 
                                      // ISubmitHandler, ISelectHandler, IDeselectHandler, // Input
                                      // IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, // Pointer
                                      // IMoveHandler
    {
        protected bool IsSelected
        {
            get
            {
                if (EventSystem.current == null)
                {
                    return false;
                }
                return EventSystem.current.currentSelectedGameObject == gameObject;
            }
        } 
        
        // [Header("Colours")]

        [SerializeField] 
        private ColorPalette colorPalette;
        private ColorPalette ColorPalette => colorPalette;
        
        [SerializeField]
        private ColorPaletteReferences colorPaletteReferences;

        // Submit Prompt
        
        [SerializeField]
        private InputType inputType = InputType.SteamDeck;

        [SerializeField]
        private bool showSubmitIcon = true;

        [SerializeField]
        private bool showOnSelectOnly = true;

        [SerializeField]
        protected InputIcon submitIcon;
        
        // Input Shortcut
        
        [SerializeField]
        private bool showShortcutIcon = false;

        [SerializeField] 
        private InputActionReference shortcutActionReference;
        private InputAction shortcutAction;
        
        [SerializeField]
        protected InputIcon shortcutIcon;
        
        #region MonoBehavior Events
        
        protected new virtual void OnValidate()
        {
            UiSettings.Log("FSI Button: OnValidate");
            base.OnValidate();
            
            // Do something... 
            if (shortcutIcon)
            {
                shortcutIcon.actionReference = shortcutActionReference;
                shortcutIcon.type = inputType;
                shortcutIcon.Refresh();
            }

            if (submitIcon)
            {
                submitIcon.type = inputType;
                submitIcon.Refresh();
            }
            
            Refresh();
        }

        protected new virtual void Awake()
        {
            UiSettings.Log("FSI Button: Awake");
            base.Awake();
            
            // Do something... 
            
            if (shortcutActionReference)
            {
                shortcutIcon.actionReference = shortcutActionReference;
                shortcutAction = shortcutActionReference.ToInputAction();
            }
        }

        protected new virtual void OnEnable()
        {
            Debug.Log("FSI Button: OnEnable");
            base.OnEnable();
            
            // Do something... 

            InputController.InputChanged += OnInputChanged;
            
            Refresh();

            if (shortcutAction != null)
            {
                shortcutAction.performed += OnShortcut;
                shortcutAction.Enable();
            }
            
            // StatusChanged?.Invoke();
        }

        protected new virtual void OnDisable()
        {
            Debug.Log("FSI Button: OnDisable");
            base.OnDisable();
            
            // Do something... 

            InputController.InputChanged -= OnInputChanged;
            
            if (shortcutAction != null)
            {
                shortcutAction.performed -= OnShortcut;
                shortcutAction.Disable();
            }
            
            // StatusChanged?.Invoke();
        }

        #endregion
        
        #region Input Prompts
        
        private void OnInputChanged()
        {
            inputType = InputController.Instance.InputType;
        }
        
        #endregion
        
        #region Shortcut

        private void OnShortcut(InputAction.CallbackContext ctx)
        {
            UiSettings.Log("FSI Button: OnShortcut", gameObject);
            base.OnSelect(null);
            
            Refresh();
        }
        
        #endregion

        #region Ui Handers
        
        #region Submit
        
        public override void OnSubmit(BaseEventData evt)
        {
            UiSettings.Log($"FSI Button: OnSubmit ({name})", gameObject);
            base.OnSubmit(evt);
            
            Refresh();
        }
        
        #endregion
        
        #region Select
        
        public override void OnSelect(BaseEventData evt)
        {
            UiSettings.Log($"FSI Button: OnSelect ({name})", gameObject);
            base.OnSelect(evt);
            
            ApplyPalette(ColorPalette, colorPalette.Selected.Buttons);
            
            // Refresh();
        }
        
        #endregion
        
        #region Deselect

        public override void OnDeselect(BaseEventData evt)
        {
            UiSettings.Log($"FSI Button: OnDeselect ({name})", gameObject);
            base.OnDeselect(evt);
            
            // ApplyPalette(ColorPalette, colorPalette.Selected.Buttons);
            
            // Refresh();
        }
        
        #endregion
        
        #region Pointer Enter

        public override void OnPointerEnter(PointerEventData evt)
        {
            UiSettings.Log($"FSI Button: OnPointerEnter ({name})", gameObject);
            base.OnPointerEnter(evt);
            
            ApplyPalette(ColorPalette, colorPalette.Selected.Buttons);
            
            // Refresh();
        }
        
        #endregion
        
        #region Pointer Exit
        
        public override void OnPointerExit(PointerEventData evt)
        {
            UiSettings.Log($"FSI Button: OnPointerExit ({name})", gameObject);
            base.OnPointerExit(evt);
            
            // Refresh();
        }
        
        #endregion
        
        #region Pointer Click
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            UiSettings.Log($"FSI Button: OnPointerClick ({name})", gameObject);
            base.OnPointerClick(eventData);
            
            ApplyPalette(ColorPalette, colorPalette.Pressed.Buttons);
            
            Refresh();
        }
        
        #endregion

        #endregion
        
        #region Refresh
        
        private void Refresh()
        {
            RefreshInputIcons();
            // RefreshColors();
        }
        
        private void RefreshInputIcons()
        {
            if (shortcutIcon)
            {
                shortcutIcon.gameObject.SetActive(showShortcutIcon && shortcutAction != null);
            }
            
            if (submitIcon)
            {
                if (showSubmitIcon)
                {
                    submitIcon.gameObject.SetActive(!showOnSelectOnly 
                                                    || currentSelectionState == SelectionState.Selected 
                                                    || currentSelectionState == SelectionState.Highlighted);
                }
                else
                {
                    submitIcon.gameObject.SetActive(false);
                }
            }
        }
        
        // private void RefreshColors()
        // {
        //     ColorPaletteMultipliers multipliers = GetColorProperties().Buttons;
        //     ApplyPalette(ColorPalette, multipliers);
        // }

        private ColorProperties GetColorProperties()
        {
            return currentSelectionState switch
                   {
                       SelectionState.Normal => ColorPalette.Normal,
                       SelectionState.Highlighted => ColorPalette.Selected,
                       SelectionState.Pressed => ColorPalette.Pressed,
                       SelectionState.Selected => ColorPalette.Selected,
                       SelectionState.Disabled => IsSelected ? ColorPalette.DisabledSelected : ColorPalette.Disabled,
                       _ => throw new ArgumentOutOfRangeException()
                   };
        }
        
        #endregion
        
        #region Visuals

        private void ApplyPalette(ColorPalette palette, ColorPaletteMultipliers modifiers)
        {
            foreach (Graphic background in colorPaletteReferences.Backgrounds)
            {
                Color c = palette.GetColor(modifiers.Background);
                background.color = c;
            }

            foreach (Graphic outline in colorPaletteReferences.Outlines)
            {
                Color c = palette.GetColor(modifiers.Outline);
                outline.color = c;
            }

            foreach (Graphic accent in colorPaletteReferences.Accents)
            {
                Color c = palette.GetColor(modifiers.Accent);
                accent.color = c;
            }
            
            colorPaletteReferences.ApplyPalette(palette, modifiers);
            if (submitIcon)
            {
                submitIcon.ColorPaletteReferences?.ApplyPalette(palette, modifiers);
            }
        }
        
        #endregion
    }
}