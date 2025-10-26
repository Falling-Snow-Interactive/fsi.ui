using UnityEngine;

namespace Fsi.Ui.Widgets
{
    public class Widget : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;
        public RectTransform Root => root;

        [SerializeField]
        private CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup => canvasGroup;
    }
}
