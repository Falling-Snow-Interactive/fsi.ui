using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    public class PanelColorApply : MonoBehaviour
    {
        [SerializeField]
        private PanelColors colors;
        
        [SerializeField]
        private ColorPaletteReferences references;

        private void Awake()
        {
            references.ApplyPalette(colors.Normal, 0);
        }

        private void OnValidate()
        {
            references.ApplyPalette(colors.Normal, 0);
        }
    }
}