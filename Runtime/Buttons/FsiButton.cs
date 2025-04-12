using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs.Settings;
using Fsi.Ui.Inputs.Ui;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

namespace Fsi.Ui.Buttons
{
    public abstract class FsiButton : MonoBehaviour, 
                                      ISubmitHandler, ISelectHandler, IDeselectHandler, // Input
                                      IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler // Pointer
    {
        private const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Assets/Prefabs/Fsi_Button_Ui.prefab";
        
        public event Action StatusChanged;
        
        [Space(10)]
        [SerializeField] 
        private UnityEvent onSubmit;
        
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
        
        [Header("Input")]
        
        [SerializeField]
        protected InputIcon inputIcon;
        
        [Header("Shortcut")]

        [SerializeField]
        private bool hasShortcut;

        [SerializeField] 
        private InputActionReference shortcutActionReference;
        private InputAction shortcutAction;

        [Serializable]
        public struct ClickTiming
        {
            public float inTime;
            public float waitTime;
            public float outTime;

            public ClickTiming(float inTime, float waitTime, float outTime)
            {
                this.inTime = inTime;
                this.waitTime = waitTime;
                this.outTime = outTime;
            }
        }
        
        [Header("Animation")]
        
        [SerializeField]
        private ClickTiming clickTiming = new(0.05f, 0, 0.05f);
        
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

        private void ApplyButtonColors(ColorPalette palette, ButtonModifiers modifiers)
        {
            foreach (Graphic g in backgrounds)
            {
                g.color = palette.GetColor(modifiers.Background);
            }

            foreach (Graphic g in outlines)
            {
                g.color = palette.GetColor(modifiers.Outline);
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
                ApplyButtonColors(ColorPalette, ColorPalette.Selected.Buttons);
            }
            else if (!interactable)
            {
                ApplyButtonColors(ColorPalette, ColorPalette.Disabled.Buttons);
            }
            else
            {
                ApplyButtonColors(ColorPalette, ColorPalette.Normal.Buttons);
            }
        }
        
        #endregion
        
        #region Animation

        private void ClickFlash()
        {
            
            foreach (Graphic bg in backgrounds)
            { 
                FlashGraphic(bg, 
                             ColorPalette.GetColor(ColorPalette.Clicked.Buttons.Background), 
                             clickTiming);
            }
            
            foreach (Graphic outline in outlines)
            { 
                FlashGraphic(outline, 
                             ColorPalette.GetColor(ColorPalette.Clicked.Buttons.Outline), 
                             clickTiming);
            }
        }
        
        private void FlashGraphic(Graphic g, Color color, ClickTiming clickTiming)
        {
            Color c0 = g.color;
            StartCoroutine(DoFlash(g, c0, color, clickTiming.inTime, clickTiming.waitTime, clickTiming.outTime));
        }

        private IEnumerator DoFlash(Graphic g, Color c0, Color c1, float inTime, float waitTime, float outTime)
        {
            g.color = c0;
            
            float t = 0;
            while (t < inTime)
            {
                float v = t / inTime;
                g.color = Color.Lerp(c0, c1, v);
                
                t += Time.deltaTime;
                yield return null;
            }

            g.color = c1;
            
            yield return new WaitForSeconds(waitTime);
            
            t = 0;
            while (t < outTime)
            {
                float v = t / outTime;
                g.color = Color.Lerp(c1, c0, v);
                
                t += Time.deltaTime;
                yield return null;
            }
            
            g.color = c0;
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
    }
}