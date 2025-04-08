using System;
using System.Collections.Generic;
using DG.Tweening;
using Fsi.Ui.Scripts.Ui.ColorPalettes;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Scripts.Ui.Buttons
{
    public class FsiButton : MonoBehaviour //, 
                                // ISelectHandler, 
                                // IPointerEnterHandler, 
                                // IDeselectHandler
    {
        public event Action<FsiButton> Selected;
        public event Action<FsiButton> Deselected;

        [Header("Simple Button")]

        [Space(10)]
        [SerializeField]
        private UnityEvent onClick;
        
        [SerializeField]
        private bool lastSiblingOnSelect = false;
        
        [Header("Status")]
        
        [SerializeField]
        private bool isSelected = false;
        public bool IsSelected => isSelected;
        
        [SerializeField]
        private bool interactable = false;
        public bool Interactable => interactable;

        [Header("Colours")]

        [SerializeField]
        private ColorPalette colorPalette;
        private ColorPalette ColorPalette => colorPalette;

        [Header("Input")]

        [SerializeField]
        private bool hasInput;

        [SerializeField]
        private InputActionReference inputActionReference;
        [CanBeNull]
        private InputAction inputAction;
        
        [Header("References")]
        
        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> outlines = new();

        [SerializeField]
        protected Button button;
        public Button Button => button;

        // [SerializeField]
        // protected InputIcon inputIcon;
        
        private void OnValidate()
        {
            if (button)
            {
                button.interactable = interactable;
            }
            
            UpdateColors();
        }

        protected virtual void Awake()
        {
            if (hasInput && inputActionReference)
            {
                inputAction = inputActionReference.ToInputAction();
            }
        }

        protected virtual void OnEnable()
        {
            // if (inputIcon)
            // {
            //     inputIcon.gameObject.SetActive(IsSelected);
            // }
            UpdateColors();

            if (hasInput && inputAction != null)
            {
                inputAction.performed += OnInputActionPerformed;
                inputAction.Enable();
            }
        }

        protected virtual void OnDisable()
        {
            if (hasInput && inputAction != null)
            {
                inputAction.performed -= OnInputActionPerformed;
                inputAction.Disable();
            }
        }

        private void OnInputActionPerformed(InputAction.CallbackContext callbackContext)
        {
            OnClick();
        }

        #region Ui Events

        public virtual void OnClick()
        {
            ClickFlash();
            onClick?.Invoke();
        }

        public virtual void OnSelect()
        {
            isSelected = true;
            UpdateColors();
            // if (inputIcon)
            // {
            //     inputIcon.gameObject.SetActive(true);
            // }

            if (lastSiblingOnSelect)
            {
                transform.SetAsLastSibling();
            }
            
            Selected?.Invoke(this);
        }

        public void OnSelect(BaseEventData _)
        {
            OnSelect();
        }

        public virtual void OnDeselect()
        {
            isSelected = false;
            UpdateColors();
            // if (inputIcon)
            // {
            //     inputIcon.gameObject.SetActive(false);
            // }
            Deselected?.Invoke(this);
        }

        public void OnDeselect(BaseEventData _)
        {
            OnDeselect();
        }

        public virtual void OnPointerEnter()
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnPointerEnter(PointerEventData _)
        {
            OnPointerEnter();
        }

        public virtual void OnPointerExit()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        
        public void OnPointerExit(PointerEventData _)
        {
            OnPointerExit();
        }

        private void UpdateColors()
        {
            if (IsSelected)
            {
                ApplyColors(ColorPalette.SelectedColors);
            }
            else if (!Interactable)
            {
                ApplyColors(ColorPalette.DisabledColors);
            }
            else
            {
                ApplyColors(ColorPalette.NormalColors);
            }
        }

        public void SetInteractable(bool set)
        {
            bool back = IsSelected;

            interactable = set;
            if (button)
            {
                button.interactable = set;
            }

            if (back)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }

            UpdateColors();
        }

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
        
        #region Animation

        public void ClickFlash()
        {
            Sequence sequence = DOTween.Sequence();
            
            foreach (Graphic bg in backgrounds)
            {
                Sequence bs = FlashGraphic(bg, ColorPalette.ClickedColors.Background);
                sequence.Insert(0, bs);
            }
            
            foreach (Graphic outline in outlines)
            {
                Sequence os = FlashGraphic(outline, ColorPalette.ClickedColors.Outline);
                sequence.Insert(0, os);
            }
            
            sequence.OnComplete(UpdateColors);
            
            sequence.Play();
        }
        
        private Sequence FlashGraphic(Graphic g, Color color)
        {
            Sequence sequence = DOTween.Sequence();
            
            Color start = g.color;
                
            Tween s0 = g.DOColor(color, 0.05f);
            Tween s1 = g.DOColor(start, 0.05f);
            
            sequence.Append(s0);
            sequence.Append(s1);

            return sequence;
        }
        
        #endregion

        #endregion
    }
}