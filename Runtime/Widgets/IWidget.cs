using System;

namespace Fsi.Ui.Entries
{
	public interface IWidget
	{
		public event Action Updated;
	}
}