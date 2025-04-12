using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorModifier
    {
        [Range(-1f, 1f)]
        [SerializeField]
        private float hAdd = 0;
        public float HAdd => hAdd;

        [Range(0f, 2f)]
        [SerializeField]
        private float hMod = 1;
        public float HMod => hMod;
        
        [Range(-1f, 1f)]
        [SerializeField]
        private float sAdd = 0;
        public float SAdd => sAdd;
        
        [Range(0f, 2f)]
        [SerializeField]
        private float sMod = 1;
        public float SMod => sMod;
        
        [Range(-1, 1)]
        [SerializeField]
        private float vAdd = 0;
        public float VAdd => vAdd;
        
        [Range(0, 2)]
        [SerializeField]
        private float vMod = 1;
        public float VMod => vMod;
    }
}