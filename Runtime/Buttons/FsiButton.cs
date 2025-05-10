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
        public ColorProperties selected;
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
                    normal?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Highlighted:
                    highlighted?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Pressed:
                    pressed?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Selected:
                    selected?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Disabled:
                    disabled?.CrossFade(backgrounds, primaryAccents, secondaryAccents);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
