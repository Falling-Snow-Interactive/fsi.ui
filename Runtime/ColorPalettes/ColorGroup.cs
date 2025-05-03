using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorGroup
    {
        [SerializeField]
        private Color background = Color.white;
        public Color Background => background;

        [SerializeField]
        private Color outline = Color.black;
        public Color Outline => outline;

        [SerializeField]
        private Color accent = Color.grey;
        public Color Accent => accent;
    }
}