using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Buttons
{
	[CustomEditor(typeof(FsiButton))]
	public class FsiButtonEditor : ButtonEditor
	{
		private FsiButton fsiButton;

		public override VisualElement CreateInspectorGUI()
		{
			fsiButton = target as FsiButton;

			VisualElement root = new();

			root.Add(base.CreateInspectorGUI());

			#region Script

			SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
			PropertyField scriptField = new(scriptProp) { enabledSelf = false };
			root.Add(scriptField);

			#endregion

			#region Button

			root.Add(CreateButtonCategory());

			#endregion

			#region Objects

			root.Add(CreateObjectsCategory());

			#endregion

			#region Visuals

			root.Add(CreateColorsCategory());

			#endregion

			#region Highlight

			root.Add(CreateHighlightCategory());

			#endregion

			#region References

			root.Add(CreateReferencesCategory());

			#endregion

			return root;
		}

		private VisualElement CreateButtonCategory()
		{
			VisualElement root = new();

			SerializedProperty interactableProp = serializedObject.FindProperty("m_Interactable");
			PropertyField interactableField = new(interactableProp);
			root.Add(interactableField);

			SerializedProperty navigationProp = serializedObject.FindProperty("m_Navigation");
			PropertyField navigationField = new(navigationProp);
			root.Add(navigationField);

			SerializedProperty clickProp = serializedObject.FindProperty("m_OnClick");
			PropertyField clickField = new(clickProp);
			root.Add(clickField);

			return root;
		}

		private VisualElement CreateObjectsCategory()
		{
			var root = new Foldout { text = "Objects", value = EditorPrefs.GetBool("FsiButton.Objects") };
			root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.Objects", evt.newValue));

			HelpBox helpBox = new("Normal objects will be shown when the button is interactable and " +
			                      "disabled objects will be shown when it is not.", HelpBoxMessageType.Info);
			root.Add(helpBox);

			SerializedProperty normalObjectsProp = serializedObject.FindProperty(nameof(fsiButton.normalObjects));
			PropertyField normalObjectsField = new(normalObjectsProp);
			root.Add(normalObjectsField);

			SerializedProperty disabledObjectsProp = serializedObject.FindProperty(nameof(fsiButton.disabledObjects));
			PropertyField disabledObjectsField = new(disabledObjectsProp);
			root.Add(disabledObjectsField);

			return root;
		}

		private VisualElement CreateColorsCategory()
		{
			var root = new Foldout { text = "Colors", value = EditorPrefs.GetBool("FsiButton.Colors") };
			root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.Colors", evt.newValue));

			SerializedProperty normalProp = serializedObject.FindProperty(nameof(fsiButton.normal));
			PropertyField normalField = new(normalProp) { name = "normal_field" };
			root.Add(normalField);

			SerializedProperty highlightedProp = serializedObject.FindProperty(nameof(fsiButton.highlighted));
			PropertyField highlightedField = new(highlightedProp) { name = "highlighted_field" };
			root.Add(highlightedField);

			SerializedProperty pressedProp = serializedObject.FindProperty(nameof(fsiButton.pressed));
			PropertyField pressedField = new(pressedProp) { name = "pressed_field" };
			root.Add(pressedField);

			SerializedProperty disabledProp = serializedObject.FindProperty(nameof(fsiButton.disabled));
			PropertyField disabledField = new(disabledProp) { name = "disabled_field" };
			root.Add(disabledField);

			return root;
		}

		private VisualElement CreateHighlightCategory()
		{
			var root = new Foldout { text = "Highlight", value = EditorPrefs.GetBool("FsiButton.Highlight") };
			root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.Highlight", evt.newValue));

			SerializedProperty highlightsProp = serializedObject.FindProperty(nameof(fsiButton.highlightObjects));
			PropertyField highlightsField = new(highlightsProp);
			root.Add(highlightsField);

			return root;
		}

		private VisualElement CreateReferencesCategory()
		{
			var root = new Foldout { text = "References", value = EditorPrefs.GetBool("FsiButton.References") };
			root.RegisterValueChangedCallback(evt => EditorPrefs.SetBool("FsiButton.References", evt.newValue));

			SerializedProperty backgroundsProp = serializedObject.FindProperty(nameof(fsiButton.backgrounds));
			PropertyField backgroundsField = new(backgroundsProp);
			root.Add(backgroundsField);

			SerializedProperty primaryAccentsProp = serializedObject.FindProperty(nameof(fsiButton.primary));
			PropertyField primaryAccentsField = new(primaryAccentsProp);
			root.Add(primaryAccentsField);

			SerializedProperty secondaryAccentsProp = serializedObject.FindProperty(nameof(fsiButton.secondary));
			PropertyField secondaryAccentsField = new(secondaryAccentsProp);
			root.Add(secondaryAccentsField);

			SerializedProperty tertiaryProp = serializedObject.FindProperty(nameof(fsiButton.tertiary));
			PropertyField tertiaryField = new(tertiaryProp);
			root.Add(tertiaryField);

			return root;
		}
	}
}