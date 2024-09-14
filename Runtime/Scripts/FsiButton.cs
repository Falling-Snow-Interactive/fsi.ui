using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Fsi.Ui
{
    public class FsiButton : MonoBehaviour, IPointerEnterHandler
    {
        public const string PrefabPath = "Packages/com.fallingsnowinteractive.ui/Runtime/Assets/Prefabs/FSI_Button_UI.prefab";
        
        [Header("Properties")]

        [SerializeField]
        private Color background = Color.white;

        [SerializeField]
        private Color outline = Color.black;

        [SerializeField]
        private Sprite sprite;
        
        [Header("Button")]

        [SerializeField]
        private Button button;

        [SerializeField]
        private ColorBlock buttonColors = ColorBlock.defaultColorBlock;

        [Header("References")]
        
        [SerializeField]
        private Image backgroundImage;

        [SerializeField]
        private Image outlineImage;

        [SerializeField]
        private Image foregroundImage;
        
        private void OnValidate()
        {
            if (backgroundImage)
            {
                backgroundImage.color = background;
                backgroundImage.sprite = sprite;
            }

            if (foregroundImage)
            {
                foregroundImage.color = background;
                foregroundImage.sprite = sprite;
            }

            if (outlineImage)
            {
                outlineImage.color = outline;
                outlineImage.sprite = sprite;
            }

            if(button)
            {
                button.transition = Selectable.Transition.ColorTint;
                button.colors = buttonColors;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
        
        #region Create
        
        [MenuItem("GameObject/FSI/Ui/Button")]
        public static void CreateButton()
        {
            GameObject parent = Selection.activeGameObject;
            GameObject asset = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath);
            GameObject button = Instantiate(asset, parent.transform);
            button.name = "FSI_Button_UI";
        }
        
        #endregion
    }
}
