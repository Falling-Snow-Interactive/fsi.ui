using System;
using Fsi.Ui.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Inputs.Prompts
{
    public class InputPrompt : MonoBehaviour
    {
        private UiSettings settings;
        
        [SerializeField]
        private Image icon;

        [SerializeField]
        private PromptType promptType;
        public PromptType PromptType
        {
            get => promptType;
            set
            {
                settings ??= UiSettings.Settings;
                promptType = value;
            }
        }

        private void OnValidate()
        {
            settings ??= UiSettings.Settings;
        }

        private void OnEnable()
        {
            InputController.InputChanged += UpdateInput;
        }

        private void OnDisable()
        {
            InputController.InputChanged -= UpdateInput;
        }

        private void UpdateInput()
        {
            switch (PromptType)
            {
                case PromptType.Auto:
                    break;
                case PromptType.MouseKeyboard:
                    break;
                case PromptType.Xbox:
                    break;
                case PromptType.PlayStation:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
