using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Fsi/UI/ColorPalette")]
    public class ColorPalette : ScriptableObject
    {
        [SerializeField]
        private Color color = Color.white;
        public Color Color => color;

        [SerializeField]
        private ColorGroup normal;
        public ColorGroup Normal => normal;

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
        private ColorGroup selectedDisabled;
        public ColorGroup SelectedDisabled => selectedDisabled;

        [Range(1, 5)]
        [SerializeField]
        private float multiplier = 1.0f;
        public float Multiplier => multiplier;

        [SerializeField]
        private float fadeDuration = 0.05f;
        public float FadeDuration => fadeDuration;
    }
}