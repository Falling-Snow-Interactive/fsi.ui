namespace Fsi.Ui.Input.Prompts
{
	public enum PromptType
	{
		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_IOS || UNITY_ANDROID
		Auto = 0,
		MouseKeyboard = 1,
		Xbox = 2,
		PlayStation = 3,
		Nintendo = 4,
		SteamDeck = 5,
		Touch = 6,
		#elif UNITY_XBOX
        Xbox = 0,
		#elif UNITY_PS5
        PlayStation = 0,
		#elif UNITY_SWITCH
		#endif
	}
}