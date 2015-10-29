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
		readonly ResourceListStore store;

		public ResourceList () : base()
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

			store = new ResourceListStore ();

			//
			store.AppendValues (new ResourceModel ("test1", "test2"));
			for (int i = 0; i < 100; i++) {
				store.AppendValues (new ResourceModel ("test" + i, null));
			}
			//

			Model = store;
		}

		public TreeSelection GetSelectedResource () {
			Console.WriteLine(Selection.Data.Keys);
			return Selection;
		}

		public bool RemoveResource (int index) {
			return false;
		}

		public bool AddNewRow () {
			return false;
		}
	}
}

