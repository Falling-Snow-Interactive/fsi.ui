using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui.Spacers
{
	[CustomPropertyDrawer(typeof(SpacerAttribute))]
	public class SpacerAttributeDrawer : DecoratorDrawer
	{
		public override VisualElement CreatePropertyGUI()
		{
			var attr = (SpacerAttribute)attribute;
			var spacer = new Spacer(attr.Size, attr.Orientation, attr.Color);
			return spacer;
		}
	}
}