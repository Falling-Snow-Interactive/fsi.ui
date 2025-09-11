using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fsi.Ui.Menu
{
	public abstract class FsiPage<T> : MonoBehaviour
		where T : Enum
	{
		[Header("References")]
		[SerializeField]
		private GameObject root;
		public abstract T Page { get; }

		// State properties
		public bool IsOpen => root.activeSelf;

		// Menu References
		protected FsiMenu<T> Menu { get; private set; }

		public virtual void Initialize(FsiMenu<T> menu)
		{
			Menu = menu;
		}

		/// <summary>
		///     Open the screen.
		/// </summary>
		public virtual void Open()
		{
			root?.SetActive(true);
			if (TryGetOpenSelect(out GameObject sel)) EventSystem.current.SetSelectedGameObject(sel);
		}

		/// <summary>
		///     Close the screen.
		/// </summary>
		public virtual void Close()
		{
			root?.SetActive(false);
		}

		protected abstract bool TryGetOpenSelect(out GameObject select);
	}
}