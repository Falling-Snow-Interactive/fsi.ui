using System;
using Fsi.Gameplay;
using Fsi.Ui.Inputs.Prompts;
using Fsi.Ui.Settings;
using Fsi.Ui.Settings.SchemeInformations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using InputSettings = Fsi.Ui.Settings.InputSettings;

namespace Fsi.Ui.Inputs
{
    public class InputController : MbSingleton<InputController>, IMoveHandler
    {
        public static event Action InputChanged;
        
        [SerializeField]
        private PlayerInput playerInput;
        
        public PromptType InputPrompt { get; private set; }
        
        [SerializeField]
        private bool resetSelectionWhenNull = true;

        private PromptType optionPromptType;

        protected override void Awake()
        {
            base.Awake();
            OnControlsChanged(null);
        }
        
        private void OnEnable()
        {
            playerInput.onControlsChanged += OnControlsChanged;
        }

        private void OnDisable()
        {
            playerInput.onControlsChanged -= OnControlsChanged;
        }

        private void Start()
        {
            InputChanged?.Invoke();
        }

        private void OnControlsChanged(PlayerInput _)
        {
            string scheme = playerInput.currentControlScheme;
            // UiSettings.Logger.Warning($"OnControlsChanged: {scheme}", gameObject);

            foreach (SchemeInformation info in InputSettings.Settings.SchemeInformation.Information)
            {
                if (scheme == info.Type)
                {
                    InputPrompt = info.Prompts;
                    InputChanged?.Invoke();
                    break;
                }
            }
        }
        
        public void OnMove(AxisEventData eventData)
        {
            if (resetSelectionWhenNull && EventSystem.current)
            {
                EventSystem.current?.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            }
        }
        
        public PromptType GetPromptType()
        {
            switch (optionPromptType)
            {
                case PromptType.Auto:
                    return InputPrompt;
                default:
                    return optionPromptType;
            }
        }
    }
}
