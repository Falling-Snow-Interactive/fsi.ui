using System;
using Fsi.Ui.ColorPalettes;
using Fsi.Ui.Inputs.Informations;
using Fsi.Ui.Inputs.Settings;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Inputs.Ui
{
    public class InputIcon : MonoBehaviour
    {
        public InputActionReference actionReference;
        
        public InputType type = InputType.SteamDeck;
        
        [Header("Color Palette")]
        
        [CanBeNull]
        [SerializeField]
        private ColorPaletteReferences colorPaletteReferences;
        public ColorPaletteReferences ColorPaletteReferences => colorPaletteReferences;
        
        [Header("References")]
        
        [SerializeField]
        private Image icon;

        // Information to grab glyphs
        private InputInformationGroup informationGroup;

        private void OnValidate()
        {
            Refresh();
        }
        
        private void OnEnable()
        {
            InputController.InputChanged += Refresh;
            Refresh();
        }

        private void OnDisable()
        {
            InputController.InputChanged -= Refresh;
        }

        private void Start()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (InputController.Instance)
            {
                type = InputController.Instance.InputType;
            }

            RefreshIcon();
        }

        private void RefreshIcon()
        {
            if (UiSettings.Settings)
            {
                informationGroup = type switch
                                          {
                                              InputType.MouseKeyboard => UiSettings.Settings.MouseKeyboard,
                                              InputType.SteamDeck => UiSettings.Settings.Steam,
                                              InputType.Xbox => UiSettings.Settings.Xbox,
                                              InputType.PlayStation => UiSettings.Settings.PlayStation,
                                              InputType.Nintendo => UiSettings.Settings.SwitchPro,
                                              InputType.Touch => UiSettings.Settings.Touch,
                                              _ => throw new ArgumentOutOfRangeException()
                                          };

                if (icon && actionReference)
                {
                    foreach (InputBinding binding in actionReference.action.bindings)
                    {
                        if (informationGroup.TryGetInformation(binding.path, out UiInputInformation info))
                        {
                            colorPaletteReferences?.Visible(true);
                            icon.gameObject.SetActive(true);
                            icon.sprite = info.Sprite;
                            return;
                        }
                        else
                        {
                            colorPaletteReferences?.Visible(false);
                        }
                    }
            
                    icon.sprite = null;
                    icon.gameObject.SetActive(false);
                }
            }
        }
    }
}
