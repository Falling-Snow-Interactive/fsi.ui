using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.ColorPalettes.Animation.Timing;
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
    public abstract class FsiButton : MonoBehaviour, 
                                      ISubmitHandler, ISelectHandler, IDeselectHandler, // Input
                                      IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, // Pointer
    IMoveHandler
    {
        private const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Assets/Prefabs/Fsi_Button_Ui.prefab";
        
        public event Action StatusChanged;
        
        [Space(10)]
        [SerializeField] 
        private UnityEvent onSubmit;
        
        [SerializeField]
        private Navigation navigation;
        
        [Header("Simple Button")]
        
        [SerializeField] 
        private bool selected = false;
        public bool Selected => selected;
        
        [SerializeField] 
        private bool interactable = false;
        public bool Interactable
        {
            get => interactable;
            set
            {
                interactable = value;
                if (button)
                {
                    button.interactable = value;
                }

                Refresh();
            }
        }

        [Header("Colours")]

        [SerializeField] 
        private ColorPalette colorPalette;
        private ColorPalette ColorPalette => colorPalette;
        
        [SerializeField]
        private ColorPaletteReferences colorPaletteReferences;

        [Header("Input")]

        [SerializeField]
        private bool showSubmitIcon = true;

        [SerializeField]
        private bool showOnSelectOnly = true;

        [SerializeField]
        protected InputIcon submitIcon;
        
        [Header("Shortcut")]
        
        [SerializeField]
        private bool showShortcutIcon = false;

        [SerializeField] 
        private InputActionReference shortcutActionReference;
        private InputAction shortcutAction;
        
        [SerializeField]
        protected InputIcon shortcutIcon;
        
        [Header("Pointer")]
        
        [SerializeField]
        private bool selectOnPointerEnter;
        
        [SerializeField]
        private bool deselectOnPointerExit;
        
        [Header("Animation")]
        
        [SerializeField]
        private FlashTiming flashTiming = new();
        
        [Header("References")]

        [SerializeField]
        protected Button button;
        public Button Button => button;
        
        #region MonoBehavior Events
        
        private void OnValidate()
        {
            Interactable = interactable;
            Refresh();
        }

        protected virtual void Awake()
        {
            if (shortcutActionReference)
            {
                shortcutIcon.actionReference = shortcutActionReference;
                shortcutAction = shortcutActionReference.ToInputAction();
            }
        }

        protected virtual void OnEnable()
        {
            Refresh();

            if (shortcutAction != null)
            {
                shortcutAction.performed += OnShortcut;
                shortcutAction.Enable();
            }
            
            StatusChanged?.Invoke();
        }

        protected virtual void OnDisable()
        {
            if (shortcutAction != null)
            {
                shortcutAction.performed -= OnShortcut;
                shortcutAction.Disable();
            }
            
            StatusChanged?.Invoke();
        }
        
        #endregion
        
        #region Shortcut

        private void OnShortcut(InputAction.CallbackContext ctx)
        {
            UiSettings.Log("FSI Button: OnShortcut", gameObject);
            // OnClick();
        }
        
        #endregion

        #region Ui Handers
        
        #region Submit

        public virtual void OnSubmit()
        {
            UiSettings.Log("FSI Button: OnSubmit", gameObject);
            colorPaletteReferences.Flash(this, ColorPalette, ColorPalette.Flash.Buttons, flashTiming);
            if (submitIcon)
            {
                submitIcon.ColorPaletteReferences.Flash(this, ColorPalette, ColorPalette.Flash.Buttons, flashTiming);
            }

            if (shortcutIcon)
            {
                shortcutIcon.ColorPaletteReferences.Flash(this, ColorPalette, ColorPalette.Flash.Buttons, flashTiming);
            }
            onSubmit?.Invoke();
        }
        
        public void OnSubmit(BaseEventData _)
        {
            OnSubmit();
        }
        
        #endregion
        
        #region Select

        protected virtual void OnSelect()
        {
            UiSettings.Log("FSI Button: OnSelect", gameObject);
            selected = true;
            Refresh();
            
            StatusChanged?.Invoke();
        }
        
        public void OnSelect(BaseEventData _)
        {
            OnSelect();
        }
        
        #endregion
        
        #region Deselect

        protected virtual void OnDeselect()
        {
            UiSettings.Log("FSI Button: OnDeselect", gameObject);
            selected = false;
            Refresh();
        }

        public void OnDeselect(BaseEventData _)
        {
            OnDeselect();
        }
        
        #endregion
        
        #region Pointer Enter

        protected virtual void OnPointerEnter()
        {
            UiSettings.Log("FSI Button: OnPointerEnter", gameObject);
            if (selectOnPointerEnter)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
        }

        public void OnPointerEnter(PointerEventData _)
        {
            OnPointerEnter();
        }
        
        #endregion
        
        #region Pointer Exit

        protected virtual void OnPointerExit()
        {
            UiSettings.Log("FSI Button: OnPointerExit", gameObject);
            if (deselectOnPointerExit && EventSystem.current.currentSelectedGameObject == gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        
        public void OnPointerExit(PointerEventData _)
        {
            OnPointerExit();
        }
        
        #endregion
        
        #region Pointer Click
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnSubmit();
        }
        
        #endregion

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

        private void Refresh()
        {
            RefreshInputIcons();
            RefreshColors();
        }
        
        private void RefreshColors()
        {
            ColorPaletteMultipliers multipliers = ColorPalette.Normal.Buttons;
            if (selected)
            {
                multipliers = ColorPalette.Selected.Buttons;
            }
            else if (!interactable)
            {
                multipliers = ColorPalette.Disabled.Buttons;
            }
            ApplyPalette(ColorPalette, multipliers);
        }

        private void RefreshInputIcons()
        {
            if (shortcutIcon)
            {
                shortcutIcon.gameObject.SetActive(showShortcutIcon);
            }
            
            if (submitIcon && showSubmitIcon)
            {
                if (!showShortcutIcon || !shortcutIcon)
                {
                    submitIcon.gameObject.SetActive(!showOnSelectOnly || Selected);
                }
                else
                {
                    submitIcon.gameObject.SetActive(false);
                }
            }
        }
        
        #endregion
        
        #region Create
        
        #if UNITY_EDITOR
        
        [MenuItem("GameObject/FSI/Ui/Button")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            Object button = Instantiate(asset.gameObject, parent.transform);
            button.name = "FSI_Button_UI";
        }
        
        #endif
        
        #endregion

        public void OnMove(AxisEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}