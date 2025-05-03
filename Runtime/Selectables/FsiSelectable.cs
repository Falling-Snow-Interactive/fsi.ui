using System;
using Fsi.Ui.ColorPalettes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fsi.Ui.Selectables
{
    public class FsiSelectable : Selectable
    {
        public event Action<FsiSelectable> Selected;
        
        // Colors
        public SelectionStateColors colors;
        public ColorPaletteReferences colorPaletteReferences;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            Selected?.Invoke(this);
        }
        
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            // UiSettings.Logger.Log($"FsiSelectable: DoStateTransition ({name})", gameObject);
            base.DoStateTransition(state, instant);

            if (colors)
            {
                ColorGroup group = state switch
                                   {
                                       SelectionState.Normal => buttonColors.Normal,
                                       SelectionState.Highlighted => buttonColors.Highlighted,
                                       SelectionState.Pressed => buttonColors.Pressed,
                                       SelectionState.Selected => buttonColors.Selected,
                                       SelectionState.Disabled => buttonColors.Disabled,
                                       _ => throw new ArgumentOutOfRangeException()
                                   };
                
                OnStateTransition(group);
            }
        }

        protected virtual void OnStateTransition(ColorGroup group)
        {
            colorPaletteReferences?.ApplyPalette(group, buttonColors.FadeDuration);
        }
    }
}