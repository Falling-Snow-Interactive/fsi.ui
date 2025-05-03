using System;
using System.Collections.Generic;
using Fsi.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using InputSettings = Fsi.Ui.Inputs.Settings.InputSettings;

namespace Fsi.Ui.Inputs
{
    public class InputController : MbSingleton<InputController>, IMoveHandler
    {
        public static event Action InputChanged;
        
        [SerializeField]
        private PlayerInput playerInput;
        
        public InputType InputType { get; private set; }
        
        [SerializeField]
        private bool resetSelectionWhenNull = true;

        [SerializeField]
        private InputActionReference submitRef;
        public InputActionReference SubmitRef => submitRef;

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
            InputSettings.Logger.Warning($"OnControlsChanged: {scheme}", gameObject);
            InputType = scheme switch
                                      {
                                          "Keyboard & Mouse" => InputType.MouseKeyboard,
                                          "Steam" => InputType.SteamDeck,
                                          "Xbox" => InputType.Xbox,
                                          "PlayStation" => InputType.PlayStation,
                                          "SwitchPro" => InputType.Nintendo,
                                          "SwitchJoy" => InputType.Nintendo,
                                          "Touch" => InputType.Touch,
                                          _ => throw new ArgumentOutOfRangeException()
                                      };

            InputChanged?.Invoke();
        }
        
        public void OnMove(AxisEventData eventData)
        {
            if (resetSelectionWhenNull && EventSystem.current)
            {
                EventSystem.current?.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
            }
        }
    }
}
