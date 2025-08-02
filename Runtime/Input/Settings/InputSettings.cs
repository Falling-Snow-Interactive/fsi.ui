using Fsi.Ui.Input.Information;
using Fsi.Ui.Input.Prompts.Information;
using Fsi.Ui.Settings.SchemeInformations;
using UnityEditor;
using UnityEngine;

namespace Fsi.Ui.Input.Settings
{
	[CreateAssetMenu(menuName = "Fsi/Settings/Input", fileName = "New FSI Input Settings")]
	public class InputSettings : ScriptableObject
	{
		private const string DefaultConfigPath
			= "Packages/com.fallingsnowinteractive.ui/Assets/Config/FsiInputSettings.asset";
		private const string ResourcePath = "Settings/FsiInputSettings";
		private const string FullPath = "Assets/Resources/" + ResourcePath + ".asset";

		private static InputSettings settings;

		[SerializeField]
		private SchemeInformationGroup schemeInformation;

		[SerializeField]
		private InputInformationGroup inputInformation;

		[SerializeField]
		private PromptInformationGroup promptInformation;
		public static InputSettings Settings => settings ??= GetOrCreateSettings();
		public SchemeInformationGroup SchemeInformation => schemeInformation;
		public InputInformationGroup InputInformation => inputInformation;
		public PromptInformationGroup PromptInformation => promptInformation;

		#region Settings

		private static InputSettings GetOrCreateSettings()
		{
			var settings = Resources.Load<InputSettings>(ResourcePath);
			Debug.Log("Loading input settings");

			#if UNITY_EDITOR
			if (!settings)
			{
				if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets", "Resources");

				if (!AssetDatabase.IsValidFolder("Assets/Resources/Settings"))
					AssetDatabase.CreateFolder("Assets/Resources", "Settings");

				var d = AssetDatabase.LoadAssetAtPath<InputSettings>(DefaultConfigPath);
				settings = CreateInstance<InputSettings>();
				settings.schemeInformation = d.schemeInformation;
				settings.inputInformation = d.inputInformation;
				settings.promptInformation = d.promptInformation;
				AssetDatabase.CreateAsset(settings, FullPath);
				AssetDatabase.SaveAssets();

				Debug.Log($"Copied default input settings to {FullPath}");
			}
			#endif

			return settings;
		}

		#if UNITY_EDITOR

		public static SerializedObject GetSerializedSettings()
		{
			return new SerializedObject(GetOrCreateSettings());
		}
		#endif

		#endregion
	}
}