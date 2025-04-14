using System;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs.Informations;
using Fsi.Ui.Inputs.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Inputs.Ui.Prompts
{
    public class InputIcon : MonoBehaviour
    {
        [Header("References")]
        
        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private ColorPaletteReferences colorPaletteReferences;
        public ColorPaletteReferences ColorPaletteReferences => colorPaletteReferences;

        public void SetBinding(InputType type, InputActionReference inputActionReference)
        {
            if (UiSettings.Settings)
            {
                PromptInformationGroup promptInfo = type switch
                                                         {
                                                             InputType.MouseKeyboard => UiSettings.Settings.MouseKeyboard,
                                                             InputType.SteamDeck => UiSettings.Settings.Steam,
                                                             InputType.Xbox => UiSettings.Settings.Xbox,
                                                             InputType.PlayStation => UiSettings.Settings.PlayStation,
                                                             InputType.Nintendo => UiSettings.Settings.SwitchPro,
                                                             InputType.Touch => UiSettings.Settings.Touch,
                                                             _ => throw new ArgumentOutOfRangeException()
                                                         };
                
                if (icon && inputActionReference)
                {
                    foreach (InputBinding binding in inputActionReference.action.bindings)
                    {
                        if (promptInfo.TryGetInformation(binding.path, out UiInputInformation info))
                        {
                            colorPaletteReferences?.SetVisible(true);
                            icon.gameObject.SetActive(true);
                            icon.sprite =info.Sprite;
                            return;
                        }
                        // TODO - Eventually this should be expanded to support multiple inputs for the same action.
                        // eg: Keyboard can submit Ui elements with space or enter, but only space is shown.
                    }

                    UiSettings.Logger.Warning("No input binding found for prompt", gameObject);
                    colorPaletteReferences?.SetVisible(false);
                    icon.sprite = null;
                    icon.gameObject.SetActive(false);
                }
            }
            else
            {
                UiSettings.Logger.Error("Cannot find ui settings.");
            }
        }
    }
}