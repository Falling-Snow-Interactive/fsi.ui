using System;
using UnityEngine;

namespace Fsi.Ui.Headers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public class FsiHeaderAttribute : PropertyAttribute
    {
        public string Text { get; }
        public int Size { get; }

        public FsiHeaderAttribute(string text, int size = 0)
        {
            Text = text;
            Size = size;
        }
    }
}