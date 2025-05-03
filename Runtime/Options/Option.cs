using System;
using System.Collections.Generic;
using Fsi.Ui.Buttons;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Selectables;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Serialization;

namespace Fsi.Ui.Options
{
    public abstract class Option : FsiSelectable
    {
        // Localization
        public LocalizedString titleLoc;
        
        // Inputs
        public List<FsiButton> inputs = new();
        
        // References
        [FormerlySerializedAs("optionName")]
        [FormerlySerializedAs("text")]
        public TMP_Text titleText;

        protected override void OnEnable()
        {
            base.OnEnable();
            LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        }

        protected void Initialize()
        {
            UpdateLoc();
        }
        
        private void OnLocaleChanged(Locale locale)
        {
            UpdateLoc();
        }

        protected virtual void UpdateLoc()
        {
            titleText.text = titleLoc.GetLocalizedString();
        }
        
        protected override void OnStateTransition(ColorGroup group)
        {
            base.OnStateTransition(group);
            foreach (FsiButton input in inputs)
            {
                input.colorPaletteReferences.ApplyPalette(group, buttonColors.FadeDuration);
            }
        }

        public override void OnMove(AxisEventData evt)
        {
            base.OnMove(evt);

            switch (evt.moveDir)
            {
                case MoveDirection.Left:
                    OnLeft();
                    break;
                case MoveDirection.Right:
                    OnRight();
                    break;
                case MoveDirection.Up:
                case MoveDirection.Down:
                case MoveDirection.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public abstract void OnLeft();
        public abstract void OnRight();
    }
}
