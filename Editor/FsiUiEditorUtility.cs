using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui
{
    public static class FsiUiEditorUtility
    {
        private const string UssPath = "Packages/com.fallingsnowinteractive.ui/Assets/FsiUi.uss";

        public static void AddUss(VisualElement root)
        {
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(UssPath);
            if (uss != null)
            {
                root.styleSheets.Add(uss);
            }
        }
    }
}