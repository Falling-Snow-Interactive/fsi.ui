using Fsi.Ui.Input.Settings;
using Fsi.Ui.Labels;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using Spacer = Fsi.Ui.Dividers.Spacer;

namespace Fsi.Ui.Settings
{
	public static class InputSettingsProvider
	{
		private static SerializedObject settingsProp;

		[SettingsProvider]
		public static SettingsProvider CreateSettingsProvider()
		{
			SettingsProvider provider = new("Fsi/Input", SettingsScope.Project)
			                            {
				                            label = "Input",
				                            activateHandler = OnActivate
			                            };

			return provider;
		}

		private static void OnActivate(string searchContext, VisualElement root)
		{
			settingsProp = InputSettings.GetSerializedSettings();

			ScrollView scrollView = new();
			root.Add(scrollView);

			Label title = LabelUtility.Title("Input Settings");
			scrollView.Add(title);

			#region Input Schemes

			scrollView.Add(new Spacer());
			scrollView.Add(CreateInputCategory());

			#endregion

			root.Bind(settingsProp);
		}

		private static VisualElement CreateInputCategory()
		{
			VisualElement root = new();

			Label title = LabelUtility.Category("Information");
			root.Add(title);

			SerializedProperty inputProp = settingsProp.FindProperty("inputInformation");
			PropertyField inputField = new(inputProp);
			root.Add(inputField);

			SerializedProperty schemeProp = settingsProp.FindProperty("schemeInformation");
			PropertyField schemeField = new(schemeProp);
			root.Add(schemeField);

			SerializedProperty promptProp = settingsProp.FindProperty("promptInformation");
			PropertyField promptField = new(promptProp);
			root.Add(promptField);

			return root;
		}
	}
}