using System;

namespace ResxEditor.Core.Interfaces
{
	public interface IListViewColumn
	{
		Gtk.CellRenderer Renderer { get; }
		Type ColumnType { get; }
	}
}

