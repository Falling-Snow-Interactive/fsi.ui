using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Dividers
{
	[UxmlElement]
	public partial class Spacer : VisualElement
	{
		private const float DefaultSize = 3f;
		private const float DefaultMargin = 5f;
		private const float DefaultPadding = 0f;
		private const float DefaultBorderRadius = 3f;

		// ReSharper disable once MemberCanBePrivate.Global
		public Spacer()
		{
			#if UNITY_EDITOR
			VisualElement root = new()
			                     {
				                     style =
				                     {
					                     backgroundColor = NormalColor,

					                     height = DefaultSize,
					                     width = Length.Auto(),

					                     marginTop = DefaultMargin,
					                     marginBottom = DefaultMargin,
					                     marginLeft = DefaultMargin,
					                     marginRight = DefaultMargin,

					                     paddingTop = DefaultPadding,
					                     paddingBottom = DefaultPadding,
					                     paddingLeft = DefaultPadding,
					                     paddingRight = DefaultPadding,

					                     borderBottomLeftRadius = DefaultBorderRadius,
					                     borderBottomRightRadius = DefaultBorderRadius,
					                     borderTopLeftRadius = DefaultBorderRadius,
					                     borderTopRightRadius = DefaultBorderRadius
				                     }
			                     };

			Add(root);
			#endif
		}

		public Spacer(float size = DefaultSize)
		{
			Color c = DarkColor;

			#if UNITY_EDITOR
			VisualElement root = new()
			                     {
				                     style =
				                     {
					                     backgroundColor = c,
					                     
					                     height = size,
					                     width = Length.Auto(),

					                     marginTop = DefaultMargin,
					                     marginBottom = DefaultMargin,
					                     marginLeft = DefaultMargin,
					                     marginRight = DefaultMargin,

					                     paddingTop = DefaultPadding,
					                     paddingBottom = DefaultPadding,
					                     paddingLeft = DefaultPadding,
					                     paddingRight = DefaultPadding,

					                     borderBottomLeftRadius = DefaultBorderRadius,
					                     borderBottomRightRadius = DefaultBorderRadius,
					                     borderTopLeftRadius = DefaultBorderRadius,
					                     borderTopRightRadius = DefaultBorderRadius,
					                     
					                     flexGrow = 1,
					                     flexShrink = 1,
				                     }
			                     };

			Add(root);
			#endif
		}

		public static Color NormalColor => new(45f / 255f, 45f / 255f, 45f / 255f, 1f);
		public static Color LightColor => new(75f / 255f, 75f / 255f, 75f / 255f, 1f);
		public static Color DarkColor => new(25f / 255f, 25f / 255f, 25f / 255f, 1f);
	}
}