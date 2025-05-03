using System;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs.Informations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using InputSettings = Fsi.Ui.Inputs.Settings.InputSettings;

namespace Fsi.Ui.Inputs.Ui.Prompts
{
    public class InputIcon : MonoBehaviour
    {
        private InputActionReference actionRef;
        
        [Header("References")]
        
        [SerializeField]
        private Image icon;

        [SerializeField]
        private ColorPaletteReferences colorPaletteReferences;
        public ColorPaletteReferences ColorPaletteReferences => colorPaletteReferences;

        private void OnEnable()
        {
            if (actionRef)
            {
                SetBinding(InputController.Instance.InputType, actionRef);
            }
        }

        public void SetBinding(InputType type, InputActionReference inputActionRef)
        {
            this.actionRef = inputActionRef;
            if (InputSettings.Settings)
            {
                PromptInformationGroup promptInfo = type switch
                                                         {
                                                             InputType.MouseKeyboard => InputSettings.Settings.MouseKeyboard,
                                                             InputType.SteamDeck => InputSettings.Settings.Steam,
                                                             InputType.Xbox => InputSettings.Settings.Xbox,
                                                             InputType.PlayStation => InputSettings.Settings.PlayStation,
                                                             InputType.Nintendo => InputSettings.Settings.SwitchPro,
                                                             InputType.Touch => InputSettings.Settings.Touch,
                                                             _ => throw new ArgumentOutOfRangeException()
                                                         };
                
                if (icon != null && inputActionRef != null)
                {
                    foreach (InputBinding binding in inputActionRef.action.bindings)
                    {
                        if (promptInfo.TryGetInformation(binding.path, out UiInputInformation info))
                        {
                            icon.sprite = info.Sprite;
                            return;
                        }
                        // TODO - Eventually this should be expanded to support multiple inputs for the same action.
                        // eg: Keyboard can submit Ui elements with space or enter, but only space is shown.
                    }

                    InputSettings.Logger.Warning("No input binding found for prompt", gameObject);
                    icon.sprite = null;
                }
            }
            else
            {
                InputSettings.Logger.Error("Cannot find ui settings.");
            }
        }
    }
}