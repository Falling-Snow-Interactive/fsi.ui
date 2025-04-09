using System;
using System.Collections.Generic;
using DG.Tweening;
using Fsi.Ui.Scripts.Inputs.Settings;
using Fsi.Ui.Scripts.Inputs.Ui;
using Fsi.Ui.Scripts.Ui.ColorPalettes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

namespace Fsi.Ui.Scripts.Ui.Buttons
{
    public class FsiButton : MonoBehaviour,  
                             #region Ui Interfaces
                             
                             // Button Input Interactions
                             ISubmitHandler, 
                             ISelectHandler, 
                             IDeselectHandler, 
                             
                             // Pointer Interactions
                             IPointerEnterHandler, 
                             IPointerExitHandler,
                             IPointerClickHandler
    
                             #endregion
    {
        private const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Assets/Prefabs/Fsi_Button_Ui.prefab";
        
        public event Action StatusChanged;
        
        [Space(10)]
        [SerializeField] 
        private UnityEvent onSubmit;
        
        [Header("Simple Button")]
        
        [SerializeField] 
        private bool lastSiblingOnSelect = false;
        
        [Header("Status")]
        
        [SerializeField] 
        private bool selected = false;
        
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

        [SerializeField] private ColorPalette colorPalette;
        private ColorPalette ColorPalette => colorPalette;
        
        [Header("Input")]
        
        [SerializeField]
        protected InputIcon inputIcon;
        
        [Header("Shortcut")]

        [SerializeField]
        private bool hasShortcut;

        [SerializeField] 
        private InputActionReference shortcutActionReference;
        private InputAction shortcutAction;
        
        [Header("References")]
        
        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> outlines = new();

        [SerializeField]
        protected Button button;
        public Button Button => button;
        
        #region MonoBehavior Events
        
        private void OnValidate()
        {
            Interactable = interactable;

            if (inputIcon)
            {
                inputIcon.shortcutReference = hasShortcut ? shortcutActionReference : null;
            }
            
            Refresh();
        }

        protected virtual void Awake()
        {
            if (hasShortcut && shortcutActionReference)
            {
                shortcutAction = shortcutActionReference.ToInputAction();
            }
        }

        protected virtual void OnEnable()
        {
            Refresh();

            if (hasShortcut && shortcutAction != null)
            {
                shortcutAction.performed += OnShortcut;
                shortcutAction.Enable();
            }
            
            StatusChanged?.Invoke();
        }

        protected virtual void OnDisable()
        {
            if (hasShortcut && shortcutAction != null)
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
            UiInputSettings.Log("FSI Button: OnShortcut", gameObject);
            // OnClick();
        }
        
        #endregion

        #region Ui Handers
        
         #region Submit

        public virtual void OnSubmit()
        {
            UiInputSettings.Log("FSI Button: OnSubmit", gameObject);
            ClickFlash();
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
            UiInputSettings.Log("FSI Button: OnSelect", gameObject);
            selected = true;
            Refresh();

            if (lastSiblingOnSelect)
            {
                transform.SetAsLastSibling();
            }
            
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
            UiInputSettings.Log("FSI Button: OnDeselect", gameObject);
            selected = false;
            if (inputIcon)
            {
                inputIcon.gameObject.SetActive(hasShortcut);
            }
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
            UiInputSettings.Log("FSI Button: OnPointerEnter", gameObject);
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnPointerEnter(PointerEventData _)
        {
            OnPointerEnter();
        }
        
        #endregion
        
        #region Pointer Exit

        protected virtual void OnPointerExit()
        {
            UiInputSettings.Log("FSI Button: OnPointerExit", gameObject);
            if (EventSystem.current.currentSelectedGameObject == gameObject)
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

        private void ApplyColors(ButtonColorProperty colors)
        {
            foreach (Graphic g in backgrounds)
            {
                g.color = colors.Background;
            }

            foreach (Graphic g in outlines)
            {
                g.color = colors.Outline;
            }
        }

        private void Refresh()
        {
            if (inputIcon)
            {
                inputIcon.gameObject.SetActive(selected || hasShortcut);
            }
            
            RefreshColors();
        }
        
        private void RefreshColors()
        {
            if (selected)
            {
                ApplyColors(ColorPalette.SelectedColors);
            }
            else if (!interactable)
            {
                ApplyColors(ColorPalette.DisabledColors);
            }
            else
            {
                ApplyColors(ColorPalette.NormalColors);
            }
        }
        
        #endregion
        
        #region Animation

        private void ClickFlash()
        {
            Sequence sequence = DOTween.Sequence();
            
            foreach (Graphic bg in backgrounds)
            {
                Sequence bs = FlashGraphic(bg, 
                                           ColorPalette.ClickedColors.Background, 
                                           ColorPalette.ClickInTime,
                                           ColorPalette.ClickWaitTime,
                                           ColorPalette.ClickOutTime,
                                           ColorPalette.ClickInEase,
                                           ColorPalette.ClickOutEase);
                sequence.Insert(0, bs);
            }
            
            foreach (Graphic outline in outlines)
            {
                Sequence os = FlashGraphic(outline, 
                                           ColorPalette.ClickedColors.Outline,
                                           ColorPalette.ClickInTime,
                                           ColorPalette.ClickWaitTime,
                                           ColorPalette.ClickOutTime,
                                           ColorPalette.ClickInEase,
                                           ColorPalette.ClickOutEase);
                sequence.Insert(0, os);
            }
            
            sequence.OnComplete(Refresh);
            
            sequence.Play();
        }
        
        private Sequence FlashGraphic(Graphic g, Color color, float inTime, float waitTime, float outTime, Ease inEase, Ease outEase)
        {
            Sequence sequence = DOTween.Sequence();
            
            Color start = g.color;
                
            Tween s0 = g.DOColor(color, inTime)
                        .SetEase(inEase);
            Tween s1 = g.DOColor(start, outTime)
                        .SetEase(outEase);
            
            sequence.Append(s0);
            sequence.AppendInterval(waitTime);
            sequence.Append(s1);

            return sequence;
        }
        
        #endregion
        
        #region Create
        
        #if UNITY_EDITOR
        
        [MenuItem("GameObject/FSI/Ui/Button")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            Object button = PrefabUtility.InstantiatePrefab(asset.gameObject, parent.transform);
            button.name = "FSI_Button_UI";
        }
        
        #endif
        
        #endregion
    }
}