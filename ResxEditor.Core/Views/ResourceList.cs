using System;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;

namespace ResxEditor.Core.Views
{
	public class ResourceEditedEventArgs : EventArgs {
		public string Path { get; set; }
		public string NextText { get; set; }
	}

	public class ResourceList : TreeView
	{
		public LocalizationColumn NameColumn {
			get;
			private set;
		}
		public LocalizationColumn ValueColumn {
			get;
			private set;
		}

		public ResourceList (IResourceListStore store) : base()
		{
			NameColumn = new LocalizationColumn (true) { Title = "Name" };
			ValueColumn = new LocalizationColumn (true) { Title = "Value" };

			this.AppendColumn (NameColumn);
			this.AppendColumn (ValueColumn);

			NameColumn.AddAttribute ("text", 0);
			ValueColumn.AddAttribute ("text", 1);

			NameColumn.Edited += (_, e) => {
				store.SetName (e.Path, e.NextText);
				SetCursor (new TreePath (e.Path), ValueColumn, true);
			};
			ValueColumn.Edited += (_, e) => store.SetValue (e.Path, e.NextText);

			Model = (TreeModel)store;
		}

		public TreeSelection GetSelectedResource () {
			return Selection;
		}
	}
}

