using System.Collections.Generic;
using Fsi.Gameplay;
using UnityEngine;

namespace Fsi.Ui.Popups
{
    public class PopupController : MbSingleton<PopupController>
    {
        private readonly List<Popup> popups = new();

        [SerializeField]
        private Transform content;
        
        public void OpenPopup(Popup prefab)
        {
            Popup popup = Instantiate(prefab, content);
            popups.Add(popup);
        }

        public void ClosePopup(Popup popup)
        {
            if (popups.Contains(popup))
            {
                popups.Remove(popup);
                Destroy(popup.gameObject);
            }
        }
    }
}
