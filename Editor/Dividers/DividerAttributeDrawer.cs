using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui.Dividers
{
	[CustomPropertyDrawer(typeof(DividerAttribute))]
	public class DividerAttributeDrawer : DecoratorDrawer
	{
		public override VisualElement CreatePropertyGUI()
		{
			DividerAttribute attr = (DividerAttribute)attribute;
			VisualElement spacer = new()
			                       {
				                       style =
				                       {
					                       width = new StyleLength(StyleKeyword.Auto),
					                       height = 5,
					                       
					                       flexGrow = 0,
					                       flexShrink = 0,
					                       
					                       backgroundColor = attr.Color,
					                       
					                       marginTop = 7.5f,
					                       marginRight = 5,
					                       marginBottom = 7.5f,
					                       marginLeft = 5,
				                       }
			                       };
			return spacer; // new Divider();
		}
	}
}