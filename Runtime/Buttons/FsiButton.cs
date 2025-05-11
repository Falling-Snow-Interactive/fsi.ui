using System;
using System.Collections.Generic;
using Fsi.Ui.Colors;
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
        public List<Graphic> primaryAccents = new();
        public List<Graphic> secondaryAccents = new();
        
        protected override void OnValidate()
        {
            transition = Transition.None;
            SetBaseColors();
            DoStateTransition(SelectionState.Normal, true);
        }

        protected override void Awake()
        {
            base.Awake();
            onClick.AddListener(OnClick);
        }

        protected virtual void OnClick()
        {
            // Do stuff for all buttons here. Ex: Play a sound.
            // ...
        }

        public void SetBaseColors()
        {
            colors?.Set(backgrounds, primaryAccents, secondaryAccents);
        }
        
        // Debating to having selected and highlighted be the same thing - KD
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            switch (state)
            {
                case SelectionState.Normal:
                    OnNormalState();
                    break;
                case SelectionState.Selected:
                case SelectionState.Highlighted:
                    OnHighlightState();
                    break;
                case SelectionState.Pressed:
                    OnPressedState();
                    break;
                case SelectionState.Disabled:
                    OnDisabledState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected virtual void OnNormalState()
        {
            normal?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
        }

        protected virtual void OnHighlightState()
        {
            highlighted?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
        }

        protected virtual void OnPressedState()
        {
            pressed?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
        }

        protected virtual void OnDisabledState()
        {
            disabled?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
        }
    }
}
