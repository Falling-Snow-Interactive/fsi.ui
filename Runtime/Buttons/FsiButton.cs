using System;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs;
using Fsi.Ui.Inputs.Settings;
using Fsi.Ui.Inputs.Ui.Prompts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Button = UnityEngine.UI.Button;

namespace Fsi.Ui.Buttons
{
    public abstract class FsiButton : Button
    {
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
        private bool showSubmitPrompt = true;
        
        [SerializeField]
        private bool showOnSelectOnly = true;
        
        [SerializeField]
        protected InputIcon submitPromptIcon;
        
        [SerializeField]
        private InputActionReference submitActionRef;
        
        // Input Shortcut
        
        [SerializeField]
        private bool showShortcutIcon = false;

        [SerializeField] 
        private InputActionReference shortcutActionRef;
        private InputAction shortcutAction;
        
        [SerializeField]
        protected InputIcon shortcutIcon;
        
        #region MonoBehavior Events
        
        protected new virtual void OnValidate()
        {
            // UiSettings.Logger.Log("FSI Button: OnValidate ({name})", gameObject);
            base.OnValidate();

            if (submitPromptIcon)
            {
                submitPromptIcon.gameObject.SetActive(showSubmitPrompt);
                submitPromptIcon.SetBinding(inputType, submitActionRef);
            }

            if (shortcutIcon)
            {
                shortcutIcon.gameObject.SetActive(showShortcutIcon);
                shortcutIcon.SetBinding(inputType, shortcutActionRef);
            }
        }

        protected new virtual void Awake()
        {
            // UiSettings.Logger.Log($"Button: Awake ({name})", gameObject);
            base.Awake();
            
            if (shortcutActionRef)
            {
                shortcutAction = shortcutActionRef.ToInputAction();
            }
            
            submitPromptIcon.gameObject.SetActive(showSubmitPrompt && !showOnSelectOnly);
        }

        protected new virtual void OnEnable()
        {
            // UiSettings.Logger.Log($"Button: OnEnable ({name})", gameObject);
            base.OnEnable();
            
            // Do something... 

            InputController.InputChanged += OnInputChanged;
            
            if (shortcutAction != null)
            {
                shortcutAction.performed += OnShortcutPerformed;
                shortcutAction.Enable();
            }
        }

        protected new virtual void OnDisable()
        {
            // UiSettings.Logger.Log($"Button: OnDisable ({name})", gameObject);
            base.OnDisable();

            InputController.InputChanged -= OnInputChanged;
            
            if (shortcutAction != null)
            {
                shortcutAction.performed -= OnShortcutPerformed;
                shortcutAction.Disable();
            }
        }

        #endregion
        
        #region Input Prompts
        
        private void OnInputChanged()
        {
            inputType = InputController.Instance.InputType;
            if (submitPromptIcon)
            {
                submitPromptIcon.SetBinding(inputType, submitActionRef);
            }

            if (shortcutIcon)
            {
                shortcutIcon.SetBinding(inputType, shortcutActionRef);
            }
        }
        
        #endregion
        
        #region Shortcut

        private void OnShortcutPerformed(InputAction.CallbackContext ctx)
        {
            UiSettings.Logger.Log($"Button: OnShortcut ({name})", gameObject);
            base.OnSubmit(null);
        }
        
        #endregion

        #region Ui Handers
        
        #region Submit
        
        public override void OnSubmit(BaseEventData evt)
        {
            UiSettings.Logger.Log($"Button: OnSubmit ({name})", gameObject);
            base.OnSubmit(evt);
        }
        
        #endregion
        
        #region Select
        
        public override void OnSelect(BaseEventData evt)
        {
            // UiSettings.Logger.Log($"FSI Button: OnSelect ({name})", gameObject);
            base.OnSelect(evt);
            // colorPaletteReferences.SetColor(ColorPalette.Color, ColorPalette.Selected, ColorPalette.Multiplier, ColorPalette.FadeDuration);
        }
        
        #endregion
        
        #region Deselect

        public override void OnDeselect(BaseEventData evt)
        {
            // UiSettings.Logger.Log($"FSI Button: OnDeselect ({name})", gameObject);
            base.OnDeselect(evt);
        }
        
        #endregion
        
        #region Pointer Enter

        public override void OnPointerEnter(PointerEventData evt)
        {
            // UiSettings.LogEvent($"FSI Button: OnPointerEnter ({name})", gameObject);
            base.OnPointerEnter(evt);
            // EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        #endregion
        
        #region Pointer Exit
        
        public override void OnPointerExit(PointerEventData evt)
        {
            // UiSettings.LogEvent($"FSI Button: OnPointerExit ({name})", gameObject);
            base.OnPointerExit(evt);
            // EventSystem.current.SetSelectedGameObject(null);
        }
        
        #endregion
        
        #region Pointer Click
        
        public override void OnPointerClick(PointerEventData eventData)
        {
            // UiSettings.LogEvent($"FSI Button: OnPointerClick ({name})", gameObject);
            base.OnPointerClick(eventData);
        }
        
        #endregion

        #endregion

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            // UiSettings.Logger.Log($"Button: DoStateTransition ({name})", gameObject);
            base.DoStateTransition(state, instant);
            
            Color c = ColorPalette.Color;

            ColorGroup group = state switch
                               {
                                   SelectionState.Normal => ColorPalette.Normal,
                                   SelectionState.Highlighted => ColorPalette.Highlighted,
                                   SelectionState.Pressed => ColorPalette.Pressed,
                                   SelectionState.Selected => ColorPalette.Selected,
                                   SelectionState.Disabled => ColorPalette.Disabled,
                                   _ => throw new ArgumentOutOfRangeException()
                               };

            colorPaletteReferences.SetColor(c, group, ColorPalette.Multiplier, ColorPalette.FadeDuration);
            submitPromptIcon.gameObject.SetActive(CanShowSubmitPrompt());
            submitPromptIcon?.ColorPaletteReferences.SetColor(ColorPalette.Color, 
                                                              ColorPalette.Normal, 
                                                              ColorPalette.Multiplier,
                                                              ColorPalette.FadeDuration);
        }

        private bool CanShowSubmitPrompt()
        {
            if (!showSubmitPrompt)
            {
                return false;
            }

            if (!Application.isPlaying)
            {
                return true;
            }

            if (showOnSelectOnly && currentSelectionState != SelectionState.Selected)
            {
                return false;
            }

            return true;
        }
    }
}