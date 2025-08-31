using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Spacers
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

		public Spacer(float size,
		              SpacerOrientation orientation = SpacerOrientation.Horizontal,
		              SpacerColor color = SpacerColor.Normal)
		{
			Color c = color switch
			          {
				          SpacerColor.Light => LightColor,
				          SpacerColor.Dark => DarkColor,
				          SpacerColor.Blank => Color.clear,
				          _ => NormalColor
			          };

			#if UNITY_EDITOR
			VisualElement root = new()
			                     {
				                     style =
				                     {
					                     backgroundColor = c,

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

			switch (orientation)
			{
				case SpacerOrientation.Horizontal:
					root.style.height = size;
					break;
				case SpacerOrientation.Vertical:
					root.style.width = size;
					break;
				case SpacerOrientation.Fill:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
			}

			Add(root);
			#endif
		}

		public static Color NormalColor => new(45f / 255f, 45f / 255f, 45f / 255f, 1f);
		public static Color LightColor => new(75f / 255f, 75f / 255f, 75f / 255f, 1f);
		public static Color DarkColor => new(25f / 255f, 25f / 255f, 25f / 255f, 1f);

		public static Spacer Wide => new(DefaultSize);
		public static Spacer Tall => new(DefaultSize, SpacerOrientation.Vertical);
		public static Spacer BlankWide => new(DefaultSize, SpacerOrientation.Horizontal, SpacerColor.Blank);
		public static Spacer BlankTall => new(DefaultSize, SpacerOrientation.Vertical, SpacerColor.Blank);
	}
}