using System;
using fsi.settings.Informations;
using Fsi.Ui.Input.Prompts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

namespace Fsi.Ui.Input.Information
{
	[Serializable]
	public class InputInformation : Information<InputActionReference>
	{
		[SerializeField]
		private InputActionReference input;

		[Header("Localization")]
		[SerializeField]
		private LocalizedString locName;

		[SerializeField]
		private LocalizedString locDescription;

		[Header("Sprites")]
		[SerializeField]
		private Sprite mouseKeyboard;

		[SerializeField]
		private Sprite steamDeck;

		[SerializeField]
		private Sprite xbox;

		[SerializeField]
		private Sprite playstation;

		[SerializeField]
		private Sprite nintendo;

		[SerializeField]
		private Sprite other;
		public override InputActionReference Type => input;
		public string Name => locName.GetLocalizedString();
		public string Description => locDescription.GetLocalizedString();

		public bool TryGetSprite(PromptType prompt, out Sprite sprite)
		{
			sprite = prompt switch
			         {
				         PromptType.MouseKeyboard => mouseKeyboard,
				         PromptType.Xbox => xbox,
				         PromptType.PlayStation => playstation,
				         PromptType.Nintendo => nintendo,
				         PromptType.SteamDeck => steamDeck,
				         _ => other
			         };

			return sprite != null;
		}
	}
}