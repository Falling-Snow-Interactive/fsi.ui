using UnityEngine;
using UnityEngine.InputSystem;

namespace Fsi.Ui.Inputs.Ui.Prompts
{
    public class SimpleInputIcon : InputIcon
    {
        [SerializeField]
        private InputActionReference inputActionRef;

        private void Start()
        {
            SetBinding(InputController.Instance.InputType, inputActionRef);
        }
    }
}