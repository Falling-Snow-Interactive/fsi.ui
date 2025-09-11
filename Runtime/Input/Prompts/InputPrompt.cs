using Fsi.Ui.Input.Information;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using InputSettings = Fsi.Ui.Input.Settings.InputSettings;

namespace Fsi.Ui.Input.Prompts
{
	public class InputPrompt : MonoBehaviour
	{
		[SerializeField]
		private Image icon;

		[SerializeField]
		private InputActionReference input;
		private PromptType PromptType => InputController.Instance.Prompt;

		private void Start()
		{
			RefreshPrompt();
		}

		private void OnEnable()
		{
			InputController.OnPromptsChanged += RefreshPrompt;
		}

		private void OnDisable()
		{
			InputController.OnPromptsChanged -= RefreshPrompt;
		}

		private void RefreshPrompt()
		{
			if (InputSettings.Settings.InputInformation.TryGetInformation(input, out InputInformation inputInfo)
			    && inputInfo.TryGetSprite(PromptType, out Sprite sprite))
			{
				Debug.Log($"Setting Icon to: {PromptType}");
				icon.sprite = sprite;
			}
		}
	}
}