using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Scripts.Ui.ColorPalettes
{
    [Serializable]
    public class ButtonColorProperty
    {
        [Header("Buttons")]
        
        [SerializeField]
        private Color background;
        public Color Background => background;

        [SerializeField]
        private Color outline;
        public Color Outline => outline;
    }
}