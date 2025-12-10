using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Headers
{
    [CustomPropertyDrawer(typeof(FsiHeaderAttribute))]
    public class FsiHeaderAttributeDrawer : DecoratorDrawer
    {
        private const string StylesheetPath = "Packages/com.fallingsnowinteractive.ui/Assets/FsiUi.uss";

        private FsiHeaderAttribute Attr => (FsiHeaderAttribute)attribute;

        public override VisualElement CreatePropertyGUI()
        {
            VisualElement root = new();
            
            StyleSheet uss = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylesheetPath);
            if (uss)
            {
                root.styleSheets.Add(uss);
            }
            
            Label header = new(Attr.Text);
            header.AddToClassList("header");
            header.style.marginTop = 10;
            header.style.marginBottom = 2;

            root.Add(header);
            return root;
        }
        
        // ---------- IMGUI path ----------
        public override float GetHeight()
        {
            // Height of the header line + a bit of spacing
            return EditorGUIUtility.singleLineHeight + 6f;
        }

        public override void OnGUI(Rect position)
        {
            position = EditorGUI.IndentedRect(position);
            GUIStyle style = new(EditorStyles.boldLabel);

            position.y += 2f;
            EditorGUI.LabelField(position, Attr.Text, style);
        }
    }
}