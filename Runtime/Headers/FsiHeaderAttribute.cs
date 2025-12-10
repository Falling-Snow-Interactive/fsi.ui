using System;
using UnityEngine;

namespace Fsi.Ui.Headers
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class FsiHeaderAttribute : PropertyAttribute
    {
        public string Text { get; }

        public FsiHeaderAttribute(string text)
        {
            Text = text;
        }
    }
}