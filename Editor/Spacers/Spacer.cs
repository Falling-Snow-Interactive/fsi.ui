using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.Ui.Spacers
{
    [UxmlElement]
    public partial class Spacer : VisualElement
    {
        private static Color DefaultColor => new(45f/255f, 45f/255f, 45f/255f, 1f);
        private static Color LightColor => new(75f/255f, 75f/255f, 75f/255f, 1f);
        private static Color DarkColor => new(25f/255f, 25f/255f, 25f/255f, 1f);

        private const float DefaultSize = 3f;
        private const float DefaultMargin = 5f;
        private const float DefaultPadding = 0f;
        private const float DefaultBorderRadius = 3f;
        
        // ReSharper disable once MemberCanBePrivate.Global
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

        public Spacer(SpacerOrientation orientation, SpacerColor color)
        {
            Color c = color switch
                      {
                          SpacerColor.Light => LightColor,
                          SpacerColor.Dark => DarkColor,
                          _ => DefaultColor,
                      };
            
            #if UNITY_EDITOR
            VisualElement root = new()
                                 {
                                     style =
                                     {
                                         backgroundColor = c,
                                         
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

            if (orientation == SpacerOrientation.Horizontal)
            {
                root.style.height = DefaultSize;
            }
            else
            {
                root.style.width = DefaultSize;
            }
            
            Add(root);
            #endif
        }
        
        public static Spacer Wide => new Spacer(SpacerOrientation.Horizontal, SpacerColor.Normal);
        public static Spacer Tall => new Spacer(SpacerOrientation.Vertical, SpacerColor.Normal);
    }

    public enum SpacerOrientation
    {
        Horizontal = 0,
        Vertical = 1
    }

    public enum SpacerColor
    {
        Normal = 0,
        Light = 1,
        Dark = 2,
    }
}
