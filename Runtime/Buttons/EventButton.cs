using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Buttons
{
	public class EventButton : Button
	{
		// Debugging
		[SerializeField]
		private SelectionState debugState;

		protected override void OnValidate()
		{
			if (!Application.isPlaying) DoStateTransition(debugState, true);
		}

		// State Events
		public event Action OnNormal;
		public event Action OnHighlight;
		public event Action OnPressed;
		public event Action OnDisabled;

		#region Selectable

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			switch (state)
			{
				case SelectionState.Normal:
					OnNormal?.Invoke();
					break;
				case SelectionState.Selected:
				case SelectionState.Highlighted:
					OnHighlight?.Invoke();
					break;
				case SelectionState.Pressed:
					OnPressed?.Invoke();
					break;
				case SelectionState.Disabled:
					OnDisabled?.Invoke();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		#endregion
	}
}