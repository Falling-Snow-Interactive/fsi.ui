using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Spacers
{
    [UxmlElement]
    public partial class Spacer : VisualElement
    {
        private static Color DefaultColor => new(42f/255f, 42f/255f, 42f/255f, 1f);
        private static Color LightColor => new(72f/255f, 72f/255f, 72f/255f, 1f);

        private const float DefaultSize = 3f;
        private const float DefaultMargin = 5f;
        private const float DefaultPadding = 0f;
        private const float DefaultBorderRadius = 3f;
        
        public Spacer()
        {
            #if UNITY_EDITOR
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         backgroundColor = DefaultColor,
                                         
                                         height = DefaultSize,
                                         
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
                                         borderTopRightRadius = DefaultBorderRadius,
                                     }
                                 };
            
            Add(root);
            #endif
        }
        
        public Spacer(float size = DefaultSize, 
                      bool wide = true, 
                      bool light = false, 
                      float margin = DefaultMargin, 
                      float padding = DefaultPadding,
                      float borderRadius = DefaultBorderRadius)
        {
            #if UNITY_EDITOR

            VisualElement root = new()
                                 {
                                     style = {
                                                 backgroundColor = light ? LightColor : DefaultColor,
                                         
                                                 marginTop = margin,
                                                 marginBottom = margin,
                                                 marginLeft = margin,
                                                 marginRight = margin,
                                         
                                                 paddingTop = padding,
                                                 paddingBottom = padding,
                                                 paddingLeft = padding,
                                                 paddingRight = padding,
                                         
                                                 borderBottomLeftRadius = borderRadius,
                                                 borderBottomRightRadius = borderRadius,
                                                 borderTopLeftRadius = borderRadius,
                                                 borderTopRightRadius = borderRadius,
                                             }
                                 };

            if (wide)
            {
                root.style.height = size;
            }
            else
            {
                root.style.width = size;
            }
            
            Add(root);
            #endif
        }

        public static Spacer Wide(float height = DefaultSize, bool light = false)
        {
            return new(size: height, light: light);
        }
        
        public static VisualElement Tall(float width = DefaultSize, bool light = false)
        {
            return new Spacer(size: width, wide: false, light: light);
        }
    }
}
