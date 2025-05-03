using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Options
{
    public class OptionSlider : Option
    {
        private const float ButtonChangeValue = 0.05f;
        
        private Action<float> onValueChanged;
        
        public Slider slider;
        public TMP_Text percent;

        [Range(0, 1f)]
        public float value;
        public float Value
        {
            set
            {
                value = Mathf.Clamp01(value);
                this.value = value;
                if (slider)
                {
                    slider.value = value;
                    slider.minValue = 0;
                    slider.maxValue = 1f;
                }

                if (percent)
                {
                    percent.text = $"{value*100:0}%";
                }
                
                onValueChanged?.Invoke(value);
            }
        }

        #if UNITY_EDITOR
        
        protected override void OnValidate()
        {
            base.OnValidate();

            Value = value;
        }
        
        #endif

        public void Initialize(float value, Action<float> onValueChanged)
        {
            base.Initialize();
            this.onValueChanged = onValueChanged;

            Value = value;
        }
        
        public override void OnLeft()
        {
            Value = value - ButtonChangeValue;
        }

        public override void OnRight()
        {
            Value = value + ButtonChangeValue;
        }
    }
}