using System;
using Fsi.General;
using Fsi.Ui.Input.Prompts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Ui.Input
{
	public class InputController : MbSingleton<InputController>
	{
		[SerializeField]
		private PlayerInput playerInput;

		private PromptType activeInputPrompt;

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

		public PromptType Prompt
		{
			get
			{
				PromptType prompt = SettingsPrompt == PromptType.Auto ? activeInputPrompt : SettingsPrompt;
				Debug.Log($"Current Prompt: {prompt}");
				return SettingsPrompt == PromptType.Auto ? activeInputPrompt : SettingsPrompt;
			}
		}

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

		public static event Action OnPromptsChanged;

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