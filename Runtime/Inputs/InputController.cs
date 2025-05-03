using System;
using Fsi.Gameplay;
using Fsi.Ui.Settings;
using Fsi.Ui.Settings.SchemeInformations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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

            foreach (SchemeInformation info in UiSettings.Settings.SchemeInformation.Information)
            {
                if (scheme == info.Type)
                {
                    InputType = info.Input;
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
    }
}
