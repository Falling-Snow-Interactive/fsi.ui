using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui
{
	public abstract class DocumentElement : VisualElement
	{
		protected DocumentElement()
		{
			var document = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(DocumentPath);
			document.CloneTree(this);
		}

		protected abstract string DocumentPath { get; }
	}
}