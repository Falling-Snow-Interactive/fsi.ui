using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Labels
{
	public static class LabelUtility // : Label
	{
		private const float TitleSize = 24f;
		private const float SectionSize = 20f;
		private const float CategorySize = 14f;

		// public Header(float size, string text) : base(text)
		// {
		//     Debug.Assert(size is 0 or 1 or 2);
		//     style.fontSize = size;
		// }

		public static Label Title(string text)
		{
			Label label = new(text)
			              {
				              style = { fontSize = TitleSize }
			              };
			return label;
		}

		public static Label Section(string text)
		{
			Label label = new(text)
			              {
				              style = { fontSize = SectionSize }
			              };
			return label;
		}

		public static Label Category(string text)
		{
			Label label = new(text)
			              {
				              style =
				              {
					              fontSize = CategorySize,
					              unityFontStyleAndWeight = FontStyle.Bold
				              }
			              };
			return label;
		}
	}
}