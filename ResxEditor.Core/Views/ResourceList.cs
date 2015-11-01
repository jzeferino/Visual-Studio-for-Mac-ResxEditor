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
		public event EventHandler<ResourceEditedEventArgs> OnNameEdited;
		public event EventHandler<ResourceEditedEventArgs> OnValueEdited;

		public LocalizationColumn NameColumn {
			get;
			private set;
		}
		public LocalizationColumn ValueColumn {
			get;
			private set;
		}

		public ResourceList () : base()
		{
			NameColumn = new LocalizationColumn (true) { Title = "Name" };
			ValueColumn = new LocalizationColumn (true) { Title = "Value" };

			this.AppendColumn (NameColumn);
			this.AppendColumn (ValueColumn);

			NameColumn.AddAttribute ("text", 0);
			ValueColumn.AddAttribute ("text", 1);

			NameColumn.Edited += (_, e) => {
				if (OnNameEdited != null)
					OnNameEdited(this, e);
				SetCursor (new TreePath (e.Path), ValueColumn, true);
			};
			ValueColumn.Edited += (_, e) => {
				if (OnValueEdited != null)
					OnValueEdited(this, e);
			};
		}

		public TreeSelection GetSelectedResource () {
			return Selection;
		}
	}
}

