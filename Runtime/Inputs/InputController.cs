using System;
using Fsi.Gameplay;
using Fsi.Ui.Inputs.Settings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Ui.Inputs
{
    public class InputController : MbSingleton<InputController>
    {
        public static event Action InputChanged;
        
        [SerializeField]
        private PlayerInput playerInput;
        
        public InputType InputType { get; private set; }

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
            UiInputSettings.Log($"OnControlsChanged: {scheme}", gameObject);
            InputType = scheme switch
                                      {
                                          "Keyboard & Mouse" => InputType.MouseKeyboard,
                                          "Steam" => InputType.SteamDeck,
                                          "Xbox" => InputType.Xbox,
                                          "PlayStation5" => InputType.PlayStation,
                                          "SwitchPro" => InputType.Nintendo,
                                          "SwitchJoy" => InputType.Nintendo,
                                          _ => InputType
                                      };

            InputChanged?.Invoke();
        }
    }
}
