using System;
using TMPro;

namespace Fsi.Ui.Options
{
    public abstract class OptionEnum<T> : Option where T : Enum
    {
        public T value;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                if (enumText)
                {
                    enumText.text = GetLocalizedEnum(value);
                }
                
                onChange?.Invoke(value);
            }
        }
        
        public TMP_Text enumText;
        
        private Action<T> onChange;

        #if UNITY_EDITOR
        
        protected override void OnValidate()
        {
            base.OnValidate();

            Value = value;
        }
        
        #endif

        public void Initialize(T value, Action<T> onChangedCallback)
        {
            base.Initialize();
            
            onChange = onChangedCallback;
            this.value = value;

            enumText.text = GetLocalizedEnum(value);
        }
        
        protected abstract string GetLocalizedEnum(T entry);
        
        public override void OnLeft()
        {
            Array values = Enum.GetValues(typeof(T));
            int max = values.Length;
            int v = Convert.ToInt32(value);
            v = (v + max - 1) % max;
            Value = (T)values.GetValue(v);
        }
    
        public override void OnRight()
        {
            Array values = Enum.GetValues(typeof(T));
            int max = values.Length;
            int v = Convert.ToInt32(value);
            v = (v + max + 1) % max;
            Value = (T)values.GetValue(v);
        }
        
        public void SetEnumValue(T value)
        {
            Value = value;
        }
        
        protected override void UpdateLoc()
        {
            base.UpdateLoc();
            enumText.text = GetLocalizedEnum(value);
        }
    }
}