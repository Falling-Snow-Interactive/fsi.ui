using System;
using System.Collections;
using System.Collections.Generic;
using Fsi.Ui.ColorPalettes.Animation.Timing;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorPaletteReferences
    {
        [SerializeField]
        private List<Graphic> backgrounds = new();
        public List<Graphic> Backgrounds => backgrounds;
        
        [SerializeField]
        private List<Graphic> outlines = new();
        public List<Graphic> Outlines => outlines;
        
        [SerializeField]
        private List<Graphic> accents = new();
        public List<Graphic> Accents => accents;

        public void ApplyPalette(ColorPalette palette, ColorPaletteMultipliers modifiers)
        {
            foreach (Graphic background in backgrounds)
            {
                Color c = palette.GetColor(modifiers.Background);
                background.color = c;
            }

            foreach (Graphic outline in outlines)
            {
                Color c = palette.GetColor(modifiers.Outline);
                outline.color = c;
            }

            foreach (Graphic accent in accents)
            {
                Color c = palette.GetColor(modifiers.Accent);
                accent.color = c;
            }
        }

        public void Flash(MonoBehaviour target, ColorPalette palette, ColorPaletteMultipliers modifiers, FlashTiming timing)
        {
            foreach (Graphic background in backgrounds)
            {
                Color c0 = background.color;
                Color c1 = palette.GetColor(modifiers.Background);
                target.StartCoroutine(DoFlash(background, c0, c1, timing));
            }
        }
        
        public IEnumerator DoFlash(Graphic g, Color c0, Color c1, FlashTiming timing)
        {
            g.color = c0;
            
            float t = 0;
            while (t < timing.InTime)
            {
                float v = t / timing.InTime;
                g.color = Color.Lerp(c0, c1, v);
                
                t += Time.deltaTime;
                yield return null;
            }

            g.color = c1;
            
            yield return new WaitForSeconds(timing.WaitTime);
            
            t = 0;
            while (t < timing.OutTime)
            {
                float v = t / timing.OutTime;
                g.color = Color.Lerp(c1, c0, v);
                
                t += Time.deltaTime;
                yield return null;
            }
            
            g.color = c0;
        }

        public void Visible(bool set)
        {
            foreach (Graphic background in backgrounds)
            {
                background.gameObject.SetActive(set);
            }

            foreach (var outline in outlines)
            {
                outline.gameObject.SetActive(set);
            }

            foreach (var accent in accents)
            {
                accent.gameObject.SetActive(set);
            }
        }
    }
}