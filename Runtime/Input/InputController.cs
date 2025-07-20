using System;
using Fsi.Gameplay;
using Fsi.Ui.Input.Prompts;
using Fsi.Ui.Input.Settings.SchemeInformations;
using Fsi.Ui.Settings.SchemeInformations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Ui.Input
{
    public class InputController : MbSingleton<InputController>
    {
        public static event Action OnPromptsChanged;

        private PromptType settingsPrompt;
        public PromptType SettingsPrompt
        {
            get => settingsPrompt;
            set
            {
                settingsPrompt = value;
                OnPromptsChanged?.Invoke();
            }
        }

        private PromptType activeInputPrompt;

        public PromptType Prompt
        {
            get
            {
                var prompt= SettingsPrompt == PromptType.Auto ? activeInputPrompt : SettingsPrompt;
                Debug.Log($"Current Prompt: {prompt}");
                return SettingsPrompt == PromptType.Auto ? activeInputPrompt : SettingsPrompt;
            }
        }

        [SerializeField]
        private PlayerInput playerInput;
        
        protected override void Awake()
        {
            base.Awake();
            OnPlayerInputControlsChanged(null);
        }
        
        private void OnEnable()
        {
            playerInput.onControlsChanged += OnPlayerInputControlsChanged;
        }

        private void OnDisable()
        {
            playerInput.onControlsChanged -= OnPlayerInputControlsChanged;
        }

        private void OnPlayerInputControlsChanged(PlayerInput _)
        {
            // string scheme = playerInput.currentControlScheme;
            // if (Settings.InputSettings.Settings.SchemeInformation.TryGetInformation(scheme, out SchemeInformation info))
            // {
            //     activeInputPrompt = info.Prompts;
            //     Debug.Log($"Input: Active Input Changed ({activeInputPrompt})");
            //     
            //     OnPromptsChanged?.Invoke();
            // }
        }
    }
}