namespace Fsi.Ui.Widgets
{
	public interface IWidgetEntry<T> where T : IWidgetEntryData
	{
		public T Value { get; }
		public void Set(T value);
		public void Refresh();
	}
}