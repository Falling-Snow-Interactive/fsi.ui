using System;
using System.Collections.Generic;
using Fsi.Ui.Colors;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fsi.Ui.Buttons
{
    public class FsiButton : Button
    {
        // Colors
        public new ColorProperties colors;
        
        // Color Fades
        public ColorProperties normal;
        public ColorProperties highlighted;
        public ColorProperties pressed;
        public ColorProperties disabled;
        
        // Graphic references
        public List<Graphic> backgrounds = new();
        [FormerlySerializedAs("primaryAccents")]
        public List<Graphic> primary = new();
        [FormerlySerializedAs("secondaryAccents")]
        public List<Graphic> secondary = new();
        public List<Graphic> tertiary = new();

        // Highlight Objects (can also be referenced for colours)
        public List<GameObject> highlightObjects;
        
        protected override void OnValidate()
        {
            transition = Transition.None;
            // SetBaseColors();
            DoStateTransition(SelectionState.Normal, true);
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(OnClick);
        }

        #region Selectable Overrides
        
        // public override void OnPointerEnter(PointerEventData evt)
        // {
        //     base.OnPointerEnter(evt);
        //     SetHighlight(true);
        // }
        //
        // public override void OnPointerExit(PointerEventData evt)
        // {
        //     base.OnPointerExit(evt);
        //     SetHighlight(false);
        // }
        //
        // public override void OnSelect(BaseEventData evt)
        // {
        //     base.OnSelect(evt);
        //     SetHighlight(true);
        // }
        //
        // public override void OnDeselect(BaseEventData evt)
        // {
        //     base.OnDeselect(evt);
        //     SetHighlight(false);
        // }
        
        #endregion

        protected virtual void OnClick()
        {
            // Do stuff for all buttons here. Ex: Play a sound.
            // ...
        }

        // public void SetBaseColors()
        // {
        //     colors?.Set(backgrounds, primary, secondary, tertiary);
        // }
        
        // Debating to having selected and highlighted be the same thing - KD
        // protected override void DoStateTransition(SelectionState state, bool instant)
        // {
        //     switch (state)
        //     {
        //         case SelectionState.Normal:
        //             OnNormalState();
        //             break;
        //         case SelectionState.Selected:
        //         case SelectionState.Highlighted:
        //             OnHighlightState();
        //             break;
        //         case SelectionState.Pressed:
        //             OnPressedState();
        //             break;
        //         case SelectionState.Disabled:
        //             OnDisabledState();
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(state), state, null);
        //     }
        // }

        // protected virtual void OnNormalState()
        // {
        //     normal?.CrossFade(backgrounds, primary, secondary, tertiary);
        // }
        //
        // protected virtual void OnHighlightState()
        // {
        //     highlighted?.CrossFade(backgrounds, primary, secondary, tertiary);
        // }
        //
        // protected virtual void OnPressedState()
        // {
        //     pressed?.CrossFade(backgrounds, primary, secondary, tertiary);
        // }
        //
        // protected virtual void OnDisabledState()
        // {
        //     disabled?.CrossFade(backgrounds, primary, secondary, tertiary);
        // }
        //
        // private void SetHighlight(bool set)
        // {
        //     foreach (var h in highlightObjects)
        //     {
        //         h.SetActive(set);
        //     }
        // }
    }
}
