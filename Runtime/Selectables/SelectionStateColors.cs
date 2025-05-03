using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [CreateAssetMenu(fileName = "ButtonColors", menuName = "Fsi/Ui/Button Colors", order = 1)]
    public class SelectionStateColors : PanelColors
    {
        [SerializeField]
        private ColorGroup highlighted;
        public ColorGroup Highlighted => highlighted;

        [SerializeField]
        private ColorGroup pressed;
        public ColorGroup Pressed => pressed;

        [SerializeField]
        private ColorGroup selected;
        public ColorGroup Selected => selected;

        [SerializeField]
        private ColorGroup disabled;
        public ColorGroup Disabled => disabled;

        [SerializeField]
        private float fadeDuration = 0.05f;
        public float FadeDuration => fadeDuration;
    }
}