using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorProperties
    {
        [SerializeField]
        private ColorPaletteMultipliers buttons;
        public ColorPaletteMultipliers Buttons => buttons;
    }
}