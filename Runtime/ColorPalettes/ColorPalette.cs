using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [CreateAssetMenu(fileName = "ColorPalette", menuName = "Fsi/UI/ColorPalette")]
    public class ColorPalette : ScriptableObject
    {
        [SerializeField]
        private Color color;
        
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
        private ColorProperties clicked;
        public ColorProperties Clicked => clicked;

        public Color GetColor(ColorModifier modifier)
        {
            Color.RGBToHSV(color, out float h, out float s, out float v);
            
            h += modifier.HAdd;
            s += modifier.SAdd;
            v += modifier.VAdd;

            h *= modifier.HMod;
            s *= modifier.SMod;
            v *= modifier.VMod;
            
            Color c = Color.HSVToRGB(h, s, v);
            
            return c;
        }
    }
}