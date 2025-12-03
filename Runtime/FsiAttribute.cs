using System;

namespace Fsi.Ui
{
    /// <summary>
    /// Marks a MonoBehaviour as using the FSI custom inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class FsiAttribute : Attribute
    {
    }
}