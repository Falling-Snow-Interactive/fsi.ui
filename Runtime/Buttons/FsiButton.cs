using System;
using System.Collections.Generic;
using Fsi.Ui.Colors;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fsi.Ui.Buttons
{
	public class FsiButton : Button
	{
		// Colors
		public ColorProperties normal;
		public ColorProperties highlighted;
		public ColorProperties pressed;
		public ColorProperties disabled;

		// Object references
		public List<GameObject> normalObjects;
		public List<GameObject> disabledObjects;

		// Graphic references
		public List<Graphic> backgrounds = new();
		public List<Graphic> primary = new();
		public List<Graphic> secondary = new();
		public List<Graphic> tertiary = new();

		// Highlight Objects (can also be referenced for colours)
		public List<GameObject> highlightObjects;

		// Properties
		public FsiSelectionState State { get; private set; }
		public new bool IsHighlighted { get; private set; }

		protected override void Awake()
		{
			base.Awake();

			SetHighlight(false);
		}

		#if UNITY_EDITOR
		protected override void OnValidate()
		{
			transition = Transition.None;
		}
		#endif

		// Events
		public event Action<FsiSelectionState> OnEnteredState;
		public event Action<FsiSelectionState> OnExitedState;

		public event Action<bool> OnHighlightChanged;

		#region Highlight

		private void SetHighlight(bool set)
		{
			if (highlightObjects != null)
				foreach (GameObject h in highlightObjects)
					h.SetActive(set);

			IsHighlighted = set;
			OnHighlightChanged?.Invoke(set);
		}

		#endregion

		#region Selectable Overrides

		public override void OnPointerEnter(PointerEventData evt)
		{
			base.OnPointerEnter(evt);
			SetHighlight(true);
		}

		public override void OnPointerExit(PointerEventData evt)
		{
			base.OnPointerExit(evt);
			SetHighlight(false);
		}

		public override void OnSelect(BaseEventData evt)
		{
			base.OnSelect(evt);
			SetHighlight(true);
		}

		public override void OnDeselect(BaseEventData evt)
		{
			base.OnDeselect(evt);
			SetHighlight(false);
		}

		#endregion

		#region Button States

		protected override void DoStateTransition(SelectionState state, bool instant)
		{
			switch (state)
			{
				case SelectionState.Normal:
					SetColors(normal);
					UpdateObjects();
					ChangeState(FsiSelectionState.Normal);
					break;
				case SelectionState.Selected:
				case SelectionState.Highlighted:
					SetColors(highlighted);
					UpdateObjects();
					ChangeState(FsiSelectionState.Highlighted);
					break;
				case SelectionState.Pressed:
					SetColors(pressed);
					UpdateObjects();
					ChangeState(FsiSelectionState.Pressed);
					break;
				case SelectionState.Disabled:
					SetColors(disabled);
					UpdateObjects();
					ChangeState(FsiSelectionState.Disabled);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		private void ChangeState(FsiSelectionState state)
		{
			OnExitedState?.Invoke(State);
			State = state;
			OnEnteredState?.Invoke(State);
		}

		#endregion

		#region Colours

		private void SetColors(ColorProperties properties)
		{
			if (!properties)
				return;

			foreach (Graphic b in backgrounds)
			{
				if (!b)
					continue;

				b.color = properties.background;
			}

			foreach (Graphic p in primary)
			{
				if (!p)
					continue;

				p.color = properties.primary;
			}

			foreach (Graphic s in secondary)
			{
				if (!s)
					continue;

				s.color = properties.secondary;
			}

			foreach (Graphic t in tertiary)
			{
				if (!t)
					continue;

				t.color = properties.tertiary;
			}
		}

		private void UpdateObjects()
		{
			if (normalObjects != null)
				foreach (GameObject n in normalObjects)
					n?.SetActive(IsInteractable());

			if (disabledObjects != null)
				foreach (GameObject d in disabledObjects)
					d?.SetActive(!IsInteractable());
		}

		#endregion
	}
}