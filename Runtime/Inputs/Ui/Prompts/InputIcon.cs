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
            }
        }
    }
}