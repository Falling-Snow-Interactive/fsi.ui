using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorProperties
    {
        [SerializeField]
        private ButtonModifiers buttons;
        public ButtonModifiers Buttons => buttons;
    }
}