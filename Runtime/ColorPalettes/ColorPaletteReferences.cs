using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Fsi.Ui.ColorPalettes
{
    public class ColorPaletteReferences : MonoBehaviour
    {
        [SerializeField]
        private List<Graphic> backgrounds = new();
        
        [FormerlySerializedAs("outlxines")]
        [FormerlySerializedAs("outlines")]
        [SerializeField]
        private List<Graphic> primaryAccents = new();
        
        [FormerlySerializedAs("accents")]
        [SerializeField]
        private List<Graphic> secondaryAccents = new();
        
        [FormerlySerializedAs("primaryTexts")]
        [SerializeField]
        private List<Graphic> titleTexts = new();
        
        [FormerlySerializedAs("secondaryTexts")]
        [SerializeField]
        private List<Graphic> headingTexts = new();
        
        [FormerlySerializedAs("tertiaryTexts")]
        [SerializeField]
        private List<Graphic> bodyTexts = new();

        private List<Graphic> allGraphics;
        
        public void ApplyPalette(ColorGroup group, float duration)
        {
            ApplyToGraphics(group.Background, backgrounds, duration);
            ApplyToGraphics(group.PrimaryAccent, primaryAccents, duration);
            ApplyToGraphics(group.SecondaryAccent, secondaryAccents, duration);
            ApplyToGraphics(group.Title, titleTexts, duration);
            ApplyToGraphics(group.Heading, headingTexts, duration);
            ApplyToGraphics(group.Body, bodyTexts, duration);
        }
        
        private void ApplyToGraphics(Color color, List<Graphic> graphics, float duration)
        {
            foreach (Graphic g in graphics)
            {
                if (g)
                {
                    g.CrossFadeColor(color, duration, true, true);
                }
            }
        }

        public void SetVisible(bool set)
        {
            allGraphics ??= new();
            allGraphics.AddRange(backgrounds);
            allGraphics.AddRange(primaryAccents);
            allGraphics.AddRange(secondaryAccents);
            allGraphics.AddRange(titleTexts);
            allGraphics.AddRange(headingTexts);
            allGraphics.AddRange(bodyTexts);

            foreach (Graphic g in allGraphics)
            {
                if (g)
                {
                    g.gameObject.SetActive(set);
                }
            } 
        }
    }
}