using Fsi.Ui.Entries;
using UnityEngine;

namespace Fsi.Ui.Widgets
{
    public abstract class WidgetUi<T> : MonoBehaviour where T : IWidget
    {
        protected T Value { get; private set; }

        protected void OnEnable()
        {
            if (Value != null)
            {
                Value.Updated += Refresh;
            }
        }

        public void Set(T value)
        {
            Value = value;
            value.Updated += Refresh;
            Refresh();
        }

        public abstract void Refresh();
    }
}
