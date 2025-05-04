using System;
using System.Collections.Generic;
using Fsi.Ui.Colors;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Buttons
{
    public class FsiButton : Button
    {
        // Colors
        [SerializeField]
        private ButtonStateProperties normal;

        [SerializeField]
        private ButtonStateProperties highlighted;
        
        [SerializeField]
        private ButtonStateProperties pressed;
        
        [SerializeField]
        private ButtonStateProperties selected;
        
        [SerializeField]
        private ButtonStateProperties disabled;
        
        // Graphic references
        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> primaryAccents = new();
        
        [SerializeField]
        private List<Graphic> secondaryAccents = new();
        
        protected override void OnValidate()
        {
            transition = Transition.None;
        }
        
        // Debating to having selected and highlighted be the same thing - KD
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            switch (state)
            {
                case SelectionState.Normal:
                    normal.Apply(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Highlighted:
                    highlighted.Apply(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Pressed:
                    pressed.Apply(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Selected:
                    selected.Apply(backgrounds, primaryAccents, secondaryAccents);
                    break;
                case SelectionState.Disabled:
                    disabled.Apply(backgrounds, primaryAccents, secondaryAccents);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
