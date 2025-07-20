using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Input.Prompts
{
    public class InputPrompt : MonoBehaviour
    {
        private PromptType PromptType => InputController.Instance.Prompt;

        [SerializeField]
        private Image icon;

        [SerializeField]
        private InputActionReference input;

        private void OnEnable()
        {
            InputController.OnPromptsChanged += RefreshPrompt;
        }

        private void OnDisable()
        {
            InputController.OnPromptsChanged -= RefreshPrompt;
        }

        private void Start()
        {
            RefreshPrompt();
        }

        private void RefreshPrompt()
        {
            if (Settings.InputSettings.Settings.InputInformation.TryGetInformation(input, out var inputInfo)
                && inputInfo.TryGetSprite(PromptType, out Sprite sprite))
            {
                Debug.Log($"Setting Icon to: {PromptType}");
                icon.sprite = sprite;
            }
        }
    }
}
