using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Dividers
{
    [UxmlElement]
    public partial class Divider : VisualElement
    {
        private const float DefaultHeight = 3f;
        private const float DefaultMargin = 5f;
        private const float DefaultPadding = 0f;
        private const float DefaultBorderRadius = 3f;
        
        [UxmlAttribute]
        public float height = DefaultHeight;

        [UxmlAttribute]
        public Color color = Color.red;

        public Divider()
        {
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         backgroundColor = color,

                                         height = height,
                                         width = Length.Auto(),

                                         marginTop = DefaultMargin,
                                         marginBottom = DefaultMargin,
                                         marginLeft = DefaultMargin,
                                         marginRight = DefaultMargin,

                                         paddingTop = DefaultPadding,
                                         paddingBottom = DefaultPadding,
                                         paddingLeft = DefaultPadding,
                                         paddingRight = DefaultPadding,

                                         borderBottomLeftRadius = DefaultBorderRadius,
                                         borderBottomRightRadius = DefaultBorderRadius,
                                         borderTopLeftRadius = DefaultBorderRadius,
                                         borderTopRightRadius = DefaultBorderRadius
                                     }
                                 };

            Add(root);
        }
    }
}