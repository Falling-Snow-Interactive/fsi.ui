using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Spacers
{
	[AttributeUsage(AttributeTargets.Field
	                | AttributeTargets.Property
	                | AttributeTargets.Class
	                | AttributeTargets.Method,
	                AllowMultiple = true)]
	public class SpacerAttribute : PropertyAttribute
	{
		private const float DefaultSize = 3f;

		public SpacerAttribute(float size = DefaultSize,
		                       SpacerOrientation orientation = SpacerOrientation.Horizontal,
		                       SpacerColor color = SpacerColor.Normal)
		{
			Size = size;
			Orientation = orientation;
			Color = color;
		}

		public float Size { get; }
		public SpacerOrientation Orientation { get; }
		public SpacerColor Color { get; }
	}

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