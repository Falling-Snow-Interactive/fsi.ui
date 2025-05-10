using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Colors
{
    [CreateAssetMenu(fileName = "ButtonStateProperties", menuName = "Fsi/Ui/Colors/ButtonStateProperties")]
    public class ColorProperties : ScriptableObject
    {
        // Colors
        [SerializeField]
        private Color background = Color.white;

        [Range(0,2)]
        [SerializeField]
        private float backgroundMultiplier = 1f;

        [SerializeField]
        private Color primaryAccent = Color.black;
        
        [Range(0,2)]
        [SerializeField]
        private float primaryAccentMultiplier = 1f;

        [SerializeField]
        private Color secondaryAccent = Color.grey;
        
        [Range(0,2)]
        [SerializeField]
        private float secondaryAccentMultiplier = 1f;

        public void Set(List<Graphic> backgrounds,
                        List<Graphic> primaryAccents,
                        List<Graphic> secondaryAccents)
        {
            foreach (Graphic b in backgrounds)
            {
                b.color = background;
            }

            foreach (Graphic a in primaryAccents)
            {
                a.color = primaryAccent;
            }

            foreach (Graphic s in secondaryAccents)
            {
                s.color = secondaryAccent;
            }
        }
        
        public void CrossFade(List<Graphic> backgrounds,
                          List<Graphic> primaryAccents,
                          List<Graphic> secondaryAccents)
        {
            CrossFadeBackgrounds(backgrounds);
            CrossFadePrimaryAccents(primaryAccents);
            CrossFadeSecondaryAccents(secondaryAccents);
        }

        public void CrossFadeBackgrounds(List<Graphic> backgrounds)
        {
            foreach (Graphic graphic in backgrounds)
            {
                graphic.CrossFadeColor(background * backgroundMultiplier, 0.1f, true, true);
            }
        }

        public void CrossFadePrimaryAccents(List<Graphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                graphic.CrossFadeColor(primaryAccent * primaryAccentMultiplier, 0.1f, true, true);
            }
        }

        public void CrossFadeSecondaryAccents(List<Graphic> graphics)
        {
            foreach (Graphic graphic in graphics)
            {
                graphic.CrossFadeColor(secondaryAccent * secondaryAccentMultiplier, 0.1f, true, true);
            }
        }
    }
}