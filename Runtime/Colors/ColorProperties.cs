using UnityEngine;

namespace Fsi.Ui.Colors
{
	[CreateAssetMenu(fileName = "ButtonStateProperties", menuName = "Fsi/Ui/Colors/ButtonStateProperties")]
	public class ColorProperties : ScriptableObject
	{
		// Colors
		public Color background;
		public Color primary;
		public Color secondary;
		public Color tertiary;
	}
}