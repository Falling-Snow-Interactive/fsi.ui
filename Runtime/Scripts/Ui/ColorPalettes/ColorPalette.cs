using System;
using UnityEngine;

namespace Fsi.Ui.Scripts.Ui.ColorPalettes
{

    [Serializable]
    public class ColorPalette
    {
        [SerializeField]
        private ButtonColorProperty normalColors;
        public ButtonColorProperty NormalColors => normalColors;

        [SerializeField]
        private ButtonColorProperty selectedColors;
        public ButtonColorProperty SelectedColors => selectedColors;

        [SerializeField]
        private ButtonColorProperty disabledColors;
        public ButtonColorProperty DisabledColors => disabledColors;

        [SerializeField]
        private ButtonColorProperty clickedColors;
        public ButtonColorProperty ClickedColors => clickedColors;
    }
}