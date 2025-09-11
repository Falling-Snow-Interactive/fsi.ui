using System;
using fsi.settings.Informations;
using UnityEngine;
using UnityEngine.Localization;

namespace Fsi.Ui.Input.Prompts.Information
{
	[Serializable]
	public class PromptInformation : Information<PromptType>
	{
		[SerializeField]
		private PromptType prompt;

		[SerializeField]
		private LocalizedString locName;
		public override PromptType Type => prompt;
		public string Name => locName.IsEmpty ? "no_loc" : locName.GetLocalizedString();
	}
}