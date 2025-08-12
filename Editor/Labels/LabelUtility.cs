using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Labels
{
	public static class LabelUtility
	{
		private const float TitleSize = 20f;
		private const float SectionSize = 16f;
		private const float CategorySize = 12f;

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