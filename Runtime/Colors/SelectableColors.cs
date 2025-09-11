using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Ui.Colors
{
	public class SelectableColors : MonoBehaviour
	{
		[SerializeField]
		private SelectableState state;

		// Color Fades
		[Header("Colors")]
		[SerializeField]
		private ColorProperties normal;

		[SerializeField]
		private ColorProperties highlighted;

		[SerializeField]
		private ColorProperties pressed;

		[SerializeField]
		private ColorProperties disabled;

		// References
		[Header("References")]
		[SerializeField]
		private List<Graphic> backgrounds = new();

		[SerializeField]
		private List<Graphic> primary = new();

		[SerializeField]
		private List<Graphic> secondary = new();

		[SerializeField]
		private List<Graphic> tertiary = new();

		[SerializeField]
		private Button button;

		public SelectableState State
		{
			get => state;
			set
			{
				state = value;
				SetColors(value);
			}
		}

		private void OnEnable()
		{
		}

		private void OnValidate()
		{
			State = state;
		}

		private void SetColors(SelectableState state)
		{
			switch (state)
			{
				case SelectableState.Normal:
					SetColors(normal);
					break;
				case SelectableState.Highlighted:
					SetColors(highlighted);
					break;
				case SelectableState.Pressed:
					SetColors(pressed);
					break;
				case SelectableState.Disabled:
					SetColors(disabled);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		private void SetColors(ColorProperties properties)
		{
			if (!properties)
			{
				Debug.LogWarning($"FSI UI: Missing color properties ({nameof(properties)}) on {gameObject.name}",
				                 gameObject);
				return;
			}

			foreach (Graphic b in backgrounds) b.color = properties.background;

			foreach (Graphic p in primary) p.color = properties.primary;

			foreach (Graphic s in secondary) s.color = properties.secondary;

			foreach (Graphic t in tertiary) t.color = properties.tertiary;
		}
	}
}