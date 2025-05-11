using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
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

        [FormerlySerializedAs("primaryAccent")]
        [SerializeField]
        private Color primary = Color.black;
        
        [FormerlySerializedAs("primaryAccentMultiplier")]
        [Range(0,2)]
        [SerializeField]
        private float primaryMultiplier = 1f;

        [FormerlySerializedAs("secondaryAccent")]
        [SerializeField]
        private Color secondary = Color.grey;
        
        [FormerlySerializedAs("secondaryAccentMultiplier")]
        [Range(0,2)]
        [SerializeField]
        private float secondaryMultiplier = 1f;
        
        [SerializeField]
        private Color tertiary = Color.grey;
        
        [Range(0,2)]
        [SerializeField]
        private float tertiaryMultiplier = 1f;

        public void Set(List<Graphic> background,
                        List<Graphic> primary,
                        List<Graphic> secondary,
                        List<Graphic> tertiary)
        {
            foreach (Graphic b in background)
            {
                b.color = this.background;
            }

            foreach (Graphic a in primary)
            {
                a.color = this.primary;
            }

            foreach (Graphic s in secondary)
            {
                s.color = this.secondary;
            }
            
            foreach (Graphic t in tertiary)
            {
                t.color = this.tertiary;
            }
        }
        
        public void CrossFade(List<Graphic> background,
                          List<Graphic> primary,
                          List<Graphic> secondary,
                          List<Graphic> tertiary)
        {
            CrossFadeBackground(background);
            CrossFadePrimary(primary);
            CrossFadeSecondary(secondary);
            CrossFadeTertiary(tertiary);
        }

        public void CrossFadeBackground(List<Graphic> backgrounds)
        {
            foreach (Graphic graphic in backgrounds)
            {
                graphic.CrossFadeColor(background * backgroundMultiplier, 0.1f, true, true);
            }
        }

        public void CrossFadePrimary(List<Graphic> graphics)
        {
            foreach (var graphic in graphics)
            {
                graphic.CrossFadeColor(primary * primaryMultiplier, 0.1f, true, true);
            }
        }

        public void CrossFadeSecondary(List<Graphic> graphics)
        {
            foreach (Graphic graphic in graphics)
            {
                graphic.CrossFadeColor(secondary * secondaryMultiplier, 0.1f, true, true);
            }
        }
        
        public void CrossFadeTertiary(List<Graphic> graphics)
        {
            foreach (Graphic graphic in graphics)
            {
                graphic.CrossFadeColor(tertiary * tertiaryMultiplier, 0.1f, true, true);
            }
        }
    }
}