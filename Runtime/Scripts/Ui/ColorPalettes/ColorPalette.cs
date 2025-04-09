using System;
using DG.Tweening;
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

        [Header("Animations")]
        
        [SerializeField]
        private ButtonColorProperty clickedColors;
        public ButtonColorProperty ClickedColors => clickedColors;

        [SerializeField]
        private float clickInTime = 0.05f;
        public float ClickInTime => clickInTime;

        [SerializeField]
        private float clickWaitTime = 0;
        public float ClickWaitTime => clickWaitTime;
        
        [SerializeField]
        private float clickOutTime = 0.05f;
        public float ClickOutTime => clickOutTime;

        [SerializeField]
        private Ease clickInEase = Ease.Linear;
        public Ease ClickInEase => clickInEase;
        
        [SerializeField]
        private Ease clickOutEase = Ease.Linear;
        public Ease ClickOutEase => clickOutEase;
    }
}