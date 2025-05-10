using System;
using Fsi.Ui.Inputs.Prompts.Information;
using Fsi.Ui.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSettings = Fsi.Ui.Settings.InputSettings;

namespace Fsi.Ui.Inputs.Prompts
{
    public class InputPrompt : MonoBehaviour
    {
        private InputSettings settings;
        
        private PromptType PromptType
        {
            get
            {
                if (!Application.isPlaying || InputController.Instance == null)
                {
                    return debugPrompt;
                }

                return InputController.Instance.InputPrompt;
            }
        }

        [SerializeField]
        private Image icon;

        [SerializeField]
        private InputActionReference input;
        
        [SerializeField]
        private PromptType debugPrompt;

        private void OnValidate()
        {
            settings ??= InputSettings.Settings;
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
            if (settings.PromptInformation.TryGetInformation(PromptType, out PromptInformation info))
            {
                // icon.sprite = info.
            }
            switch (PromptType)
            {
                case PromptType.MouseKeyboard:
                    break;
                case PromptType.Xbox:
                    break;
                case PromptType.PlayStation:
                    break;
                case PromptType.Nintendo:
                    break;
                case PromptType.SteamDeck:
                    break;
                case PromptType.Touch:
                    break;
                case PromptType.Auto:
                    Debug.LogError("Auto shouldn't be called here.");
                    throw new Exception("PromptType.Auto should only be set on the settings, but shouldn't " +
                                        "actually be seen from the Input Controller.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
