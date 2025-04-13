using UnityEngine;
using UnityEngine.Serialization;

namespace Fsi.Ui.ColorPalettes
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Fsi/UI/ColorPalette")]
    public class ColorPalette : ScriptableObject
    {
        [SerializeField]
        private Color color = Color.white;
        
        [SerializeField]
        private ColorProperties normal;
        public ColorProperties Normal => normal;

        [SerializeField]
        private ColorProperties selected;
        public ColorProperties Selected => selected;

        [SerializeField]
        private ColorProperties disabled;
        public ColorProperties Disabled => disabled;
        
        [SerializeField]
        private ColorProperties pressed;
        public ColorProperties Pressed => pressed;
        
        [SerializeField]
        private ColorProperties disabledSelected;
        public ColorProperties DisabledSelected => disabledSelected;

        public Color GetColor(Color modifier)
        {
            Color c = color * modifier;
            return c;
        }
    }
}