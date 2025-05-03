using UnityEngine;

namespace Fsi.Ui.Popups
{
    public abstract class Popup : MonoBehaviour
    {
        public void Open()
        {
            Debug.Log($"Popup | Open ({name})", gameObject);
            OnOpen();
        }

        public void Close()
        {
            Debug.Log($"Popup | Close ({name})", gameObject);
            OnClose();

            Destroy(gameObject);
        }

        public void Focus()
        {
            Debug.Log($"Popup | Focus ({name})", gameObject);
            OnFocus();
        }

        public void Unfocus()
        {
            Debug.Log($"Popup | Unfocus ({name})", gameObject);
            OnUnfocus();
        }

        protected abstract void OnOpen();
        protected abstract void OnClose();

        protected abstract void OnFocus();
        protected abstract void OnUnfocus();
    }
}
