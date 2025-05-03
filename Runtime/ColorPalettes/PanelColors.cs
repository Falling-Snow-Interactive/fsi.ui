using UnityEngine;

namespace Fsi.Ui.ColorPalettes
{
    [CreateAssetMenu(fileName = "UiColors", menuName = "Fsi/Ui/Ui Colors", order = 0)]
    public class PanelColors : ScriptableObject
    {
        [SerializeField]
        private ColorGroup normal;
        public ColorGroup Normal => normal;
    }
}