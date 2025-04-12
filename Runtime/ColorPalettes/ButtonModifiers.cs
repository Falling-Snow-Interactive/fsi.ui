using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ButtonModifiers
    {
        [SerializeField]
        private ColorModifier background;
        public ColorModifier Background => background;

        [SerializeField]
        private ColorModifier outline;
        public ColorModifier Outline => outline;

        [SerializeField]
        private ColorModifier accent;
        public ColorModifier Accent => accent;
    }
}