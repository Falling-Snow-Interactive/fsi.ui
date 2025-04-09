using Fantazee.Input.Information;
using Fsi.Ui.Scripts.Inputs.Informations;
using Fsi.Ui.Scripts.Inputs.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Scripts.Inputs.Ui
{
    public class InputIcon : MonoBehaviour
    {
        [SerializeField]
        private Image icon;
        
        [SerializeField]
        private InputActionReference selectReference;
        
        public InputActionReference shortcutReference;

        [SerializeField]
        private InputType type = InputType.MouseKeyboard;

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

        private void Refresh()
        {
            if (InputController.Instance)
            {
                type = InputController.Instance.InputType;
            }

            RefreshIcon();
        }

        private void RefreshIcon()
        {
            if (UiInputSettings.Settings)
            {
                informationGroup = type switch
                                          {
                                              InputType.MouseKeyboard => UiInputSettings.Settings.MouseKeyboard,
                                              InputType.SteamDeck => UiInputSettings.Settings.Steam,
                                              InputType.Xbox => UiInputSettings.Settings.Xbox,
                                              InputType.PlayStation => UiInputSettings.Settings.PlayStation,
                                              InputType.Nintendo => UiInputSettings.Settings.SwitchPro,
                                              _ => informationGroup
                                          };

                if (icon)
                {
                    var actionReference = shortcutReference ? shortcutReference : selectReference;
                    foreach (InputBinding binding in actionReference.action.bindings)
                    {
                        if (informationGroup.TryGetInformation(binding.path, out UiInputInformation info))
                        {
                            icon.gameObject.SetActive(true);
                            icon.sprite = info.Sprite;
                            return;
                        }
                    }
            
                    icon.sprite = null;
                    icon.gameObject.SetActive(false);
                }
            }
        }
    }
}
