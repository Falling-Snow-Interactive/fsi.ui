using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui.Headers
{
    [CustomPropertyDrawer(typeof(FsiHeaderAttribute))]
    public class FsiHeaderAttributeDrawer : DecoratorDrawer
    {
        private const string StylesheetPath = "Packages/com.fallingsnowinteractive.ui/Editor/FsiUi.uss";

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
            header.AddToClassList("h" + Attr.Size);
            header.style.marginTop = 10;
            header.style.marginBottom = 2;

            root.Add(header);
            return root;
        }
    }
}