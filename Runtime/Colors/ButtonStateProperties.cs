using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Colors
{
    [Serializable]
    public class ButtonStateProperties
    {
        // Colors
        [SerializeField]
        private Color background = Color.white;

        [SerializeField]
        private Color primaryAccent = Color.black;

        [SerializeField]
        private Color secondaryAccent = Color.grey;

        public void Apply(List<Graphic> backgrounds,
                          List<Graphic> primaryAccents,
                          List<Graphic> secondaryAccents)
        {
            ApplyBackground(backgrounds);
            ApplyPrimaryAccent(primaryAccents);
            ApplySecondaryAccent(secondaryAccents);
        }

        public void ApplyBackground(List<Graphic> backgrounds)
        {
            foreach (Graphic graphic in backgrounds)
            {
                graphic.CrossFadeColor(background, 0.1f, true, true);
            }
        }

        public void ApplyPrimaryAccent(List<Graphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                graphic.CrossFadeColor(primaryAccent, 0.1f, true, true);
            }
        }

        public void ApplySecondaryAccent(List<Graphic> graphics)
        {
            foreach (Graphic graphic in graphics)
            {
                graphic.CrossFadeColor(secondaryAccent, 0.1f, true, true);
            }
        }
    }
}