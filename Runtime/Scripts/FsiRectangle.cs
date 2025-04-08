using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fsi.Ui
{
    public class FsiRectangle : MonoBehaviour
    {
        public const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Runtime/Assets/Prefabs/FSI_Rectangle_UI.prefab";
        
        [Header("Properties")]

        [SerializeField]
        private Color background = Color.white;

        public Color Background
        {
            get => background;
            set
            {
                background = value;
                UpdateColors();
            }
        }

        [SerializeField]
        private Color outline = Color.black;

        public Color Outline
        {
            get => outline;
            set
            {
                outline = value;
                UpdateColors();
            }
        }

        [Header("References")]

        [SerializeField]
        private Image outlineImage;

        [SerializeField]
        private Image foregroundImage;
        
        private void OnValidate()
        {
            UpdateColors();
        }

        private void UpdateColors()
        {
            if (foregroundImage && outlineImage)
            {
                foregroundImage.color = background;
                outlineImage.color = outline;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        #region Create
        #if UNITY_EDITOR
        [MenuItem("GameObject/FSI/Ui/Rectangle")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            GameObject button = Instantiate(asset, parent.transform);
            button.name = "FSI_Rectangle_UI";

            Selection.activeGameObject = button.gameObject;
        }
        #endif
        #endregion
    }
}
