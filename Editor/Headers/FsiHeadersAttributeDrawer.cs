using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.Ui.Headers
{
    [CustomPropertyDrawer(typeof(FsiHeaderAttribute))]
    public class FsiHeaderAttributeDrawer : DecoratorDrawer
    {
        private FsiHeaderAttribute Attr => (FsiHeaderAttribute)attribute;
        
        public override VisualElement CreatePropertyGUI()
        {
            VisualElement root = new();

            Label header = new(Attr.Text);
            header.AddToClassList("h" + Attr.Size);
            header.style.marginTop = 10;
            header.style.marginBottom = 2;

            root.Add(header);
            return root;
        }
    }
}