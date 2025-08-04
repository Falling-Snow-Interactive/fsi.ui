using System.Collections.Generic;
using Fsi.Ui.Labels;
using Fsi.Ui.Spacers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui
{
	public abstract class FsiInspector : Editor
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
			root.Add(CreateInspector());
			root.Add(new Spacer());
			
			Foldout defaultInspector = new(){ text = "Default Inspector", value = false};
			InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);
			root.Add(defaultInspector);

			return root;
		}

		protected abstract VisualElement CreateInspector();

		public VisualElement CreateTitle(string title, string subtitle)
		{
			VisualElement root = new();
			
			Label titleLabel = LabelUtility.Title(title);
			Label subtitleLabel = new Label(subtitle);

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

			VisualElement newCategory = new();

			Label label = LabelUtility.Category(category);
			newCategory.Add(label);

			root.Add(newCategory);
			
			categories.Add(category, newCategory);
			return newCategory;
		}
	}
}