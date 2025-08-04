using System.Collections.Generic;
using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui
{
	public abstract class FsiEditor : Editor
	{
		private Dictionary<string, VisualElement> categories = new();

		private VisualElement root;
		
		public override VisualElement CreateInspectorGUI()
		{
			categories = new Dictionary<string, VisualElement>();
			
			root = new VisualElement();
			
			// Create a SerializedObject to hold the script reference
			var scriptSo = new SerializedObject(target);
			var scriptProperty = scriptSo.FindProperty("m_Script");
			var scriptField = new PropertyField(scriptProperty) { label = "Script" };

			// Disable the field
			scriptField.Bind(scriptSo);
			scriptField.SetEnabled(false);
			scriptField.bindingPath = "m_Script";
    
			root.Add(scriptField);
			root.Add(DrawCategories());
			
			Foldout defaultInspector = new(){ text = "Default Inspector", value = false};
			Box defaultBox = new();
			defaultInspector.Add(defaultBox);
			InspectorElement.FillDefaultInspector(defaultBox, serializedObject, this);
			root.Add(defaultInspector);

			return root;
		}

		protected abstract VisualElement DrawCategories();

		public VisualElement CreateTitle(string title, string subtitle)
		{
			VisualElement root = new();
			
			Label titleLabel = LabelUtility.Title(title);
			Label subtitleLabel = new(subtitle);

			root.Add(titleLabel);
			root.Add(subtitleLabel);
			
			root.Add(new Spacer());

			return root;
		}

		public VisualElement AddCategory(string category, string[] properties)
		{
			VisualElement categoryGroup = GetOrCreateCategory(category);

			foreach (string s in properties)
			{
				SerializedProperty prop = serializedObject.FindProperty(s);
				PropertyField field = new(prop);
				categoryGroup.Add(field);
			}

			return categoryGroup;
		}

		private VisualElement GetOrCreateCategory(string category)
		{
			if (categories.TryGetValue(category, out VisualElement c))
			{
				return c;
			}

			string pref = $"{GetType().Name}.{category}";
			Foldout foldout = UiEditorUtility.CategoryFoldout(category, pref);
			VisualElement content = new() { style = { unityFontStyleAndWeight = FontStyle.Normal } };
			root.Add(foldout);
			root.Add(new Spacer());

			foldout.Add(content);

			categories.Add(category, content);
			return content;
		}
	}
}