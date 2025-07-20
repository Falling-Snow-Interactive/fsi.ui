using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Fsi.Ui
{
	public static class UiUtility
	{
		public static List<RaycastResult> GetUiElementsUnderPointer()
		{
			PointerEventData pointerData = new PointerEventData(EventSystem.current)
			                               {
				                               position = Pointer.current.position.value,
			                               };

			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerData, raycastResults);
			return raycastResults;
		}
	}
}