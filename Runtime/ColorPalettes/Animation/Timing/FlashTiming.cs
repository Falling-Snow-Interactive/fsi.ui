using System;
using UnityEngine;

namespace Fsi.Ui.ColorPalettes.Animation.Timing
{
    [Serializable]
    public class FlashTiming
    {
        [SerializeField]
        private float inTime;
        public float InTime => inTime;
        
        [SerializeField]
        private float waitTime;
        public float WaitTime => waitTime;
        
        [SerializeField]
        private float outTime;
        public float OutTime => outTime;

        public FlashTiming(float inTime = 0.05f, float waitTime = 0f, float outTime = 0.05f)
        {
            this.inTime = inTime;
            this.waitTime = waitTime;
            this.outTime = outTime;
        }
    }
}