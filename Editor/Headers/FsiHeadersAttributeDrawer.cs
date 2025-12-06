using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.Ui.Headers
{
    [CustomPropertyDrawer(typeof(FsiHeaderAttribute))]
    public class FsiHeaderAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new();

            FsiHeaderAttribute fsiHeaderAttribute = (FsiHeaderAttribute)attribute;

            // Create the header label
            Label header = new(fsiHeaderAttribute.Text);
            header.AddToClassList("h" + fsiHeaderAttribute.Size); // ← your custom USS class

            // Default Header-like spacing (imitates IMGUI HeaderAttribute)
            header.style.marginTop = 10;
            header.style.marginBottom = 2;

            container.Add(header);

            // Draw the property below it
            PropertyField field = new(property);
            container.Add(field);

            return container;
        }
    }
}