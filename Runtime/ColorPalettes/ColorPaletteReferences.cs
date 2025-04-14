using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorPaletteReferences
    {
        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [SerializeField]
        private List<Graphic> outlines = new();
        
        [SerializeField]
        private List<Graphic> accents = new();
        
        public void SetColor(Color color, ColorGroup group, float multiplier, float duration)
        {
            Color background = color * (group.Background * multiplier);
            Color outline = color * (group.Outline * multiplier);
            Color accent = color * (group.Accent * multiplier);
            
            foreach (Graphic b in backgrounds)
            {
                b.CrossFadeColor(background, duration, true, true);
            }

            foreach (Graphic o in outlines)
            {
                o.CrossFadeColor(outline, duration, true, true);
            }

            foreach (Graphic a in accents)
            {
                a.CrossFadeColor(accent, duration, true, true);
            }
        }

        public void SetVisible(bool set)
        {
            foreach (Graphic b in backgrounds)
            {
                b.gameObject.SetActive(set);
            }

            foreach (Graphic o in outlines)
            {
                o.gameObject.SetActive(set);
            }

            foreach (Graphic a in accents)
            {
                a.gameObject.SetActive(set);
            }
        }
    }
}