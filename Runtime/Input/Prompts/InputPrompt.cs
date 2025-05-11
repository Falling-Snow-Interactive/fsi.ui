using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Fsi.Ui.Input.Prompts
{
    public class InputPrompt : MonoBehaviour
    {
        private PromptType PromptType
        {
            get
            {
                if (InputController.Instance)
                {
                    return InputController.Instance.Prompt;
                }

                return debugPrompt;
            }
        }

        [SerializeField]
        private Image icon;

        [SerializeField]
        private InputActionReference input;
        
        [SerializeField]
        private PromptType debugPrompt;

        private void OnValidate()
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
