using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui
{
	public static class UiEditorUtility
	{
		public static Foldout CategoryFoldout(string category, string editorPref)
		{
			Foldout foldout = new Foldout
			                  {
				                  value = EditorPrefs.GetBool(editorPref, true),
				                  text = category,
				                  style =
				                  {
					                  unityFontStyleAndWeight = FontStyle.Bold,
				                  }
			                  };
			
			foldout.RegisterValueChangedCallback(evt =>
			                                     {
				                                     EditorPrefs.SetBool(editorPref, foldout.value);
			                                     });

			return foldout;
		} 
	}
}