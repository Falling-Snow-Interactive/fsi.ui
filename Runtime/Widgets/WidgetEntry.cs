
namespace Fsi.Ui.Widgets
{
    public abstract class WidgetEntry<T> : Widget, IWidgetEntry<T> where T : IWidgetEntryData
    {
        public T Value { get; private set; }

        protected void OnEnable()
        {
            if (Value != null)
            {
                Value.Updated += Refresh;
            }
        }

        protected void OnDisable()
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
