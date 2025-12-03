using System;
using UnityEngine;

namespace Fsi.Ui.Categories
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CategoryAttribute : PropertyAttribute
    {
        public string Category { get; }
        public bool Divider { get; }

        public CategoryAttribute(string category, bool divider = false)
        {
            Category = category;
            Divider = divider;
        }
    }
}