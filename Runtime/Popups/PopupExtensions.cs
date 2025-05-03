namespace Fsi.Ui.Popups
{
    public static class PopupExtensions
    {
        public static void Open(this Popup prefab)
        {
            PopupController.Instance.OpenPopup(prefab);
        }
    }
}