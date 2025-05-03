using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Fsi.Ui.ColorPalettes
{
    [Serializable]
    public class ColorGroup
    {
        [Header("Images")]
        
        [SerializeField]
        private Color background = Color.white;
        public Color Background => background;

        [FormerlySerializedAs("outline")]
        [SerializeField]
        private Color primaryAccent = Color.black;
        public Color PrimaryAccent => primaryAccent;

        [FormerlySerializedAs("accent")]
        [SerializeField]
        private Color secondaryAccent = Color.grey;
        public Color SecondaryAccent => secondaryAccent;
        
        [SerializeField]
        private Color tertiaryAccent = Color.white;
        public Color TertiaryAccent => tertiaryAccent;
        
        [Header("Text")]
        
        [FormerlySerializedAs("titleText")]
        [SerializeField]
        private Color title = Color.black;
        public Color Title => title;
        
        [FormerlySerializedAs("headingText")]
        [SerializeField]
        private Color heading = Color.black;
        public Color Heading => heading;
        
        [FormerlySerializedAs("bodyText")]
        [SerializeField]
        private Color body = Color.black;
        public Color Body => body;
    }
}