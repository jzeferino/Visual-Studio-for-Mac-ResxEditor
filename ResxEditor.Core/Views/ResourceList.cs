using System;
using Gtk;

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
		public event EventHandler<ResourceEditedEventArgs> OnCommentEdited;
		public event EventHandler<string> OnResourceAdded;
		public event EventHandler<ButtonReleaseEventArgs> RightClicked;

		public LocalizationColumn NameColumn {
			get;
			private set;
		}
		public LocalizationColumn ValueColumn {
			get;
			private set;
		}
		public LocalizationColumn CommentColumn {
			get;
			private set;
		}

		public ResourceList () : base()
		{
			NameColumn = new LocalizationColumn (true) { Title = "Name" };
			ValueColumn = new LocalizationColumn (true) { Title = "Value" };
			CommentColumn = new LocalizationColumn (true) { Title = "Comment" };

			this.AppendColumn (NameColumn);
			this.AppendColumn (ValueColumn);
			this.AppendColumn (CommentColumn);

			NameColumn.AddAttribute ("text", 0);
			ValueColumn.AddAttribute ("text", 1);
			CommentColumn.AddAttribute ("text", 2);

			NameColumn.Edited += (_, e) => {
				OnNameEdited(this, e);
				OnResourceAdded(this, e.NextText);
				SetCursor (new TreePath (e.Path), ValueColumn, true);
			};
			ValueColumn.Edited += (_, e) => {
				OnValueEdited (this, e);
				SetCursor (new TreePath (e.Path), CommentColumn, true);
			};
			CommentColumn.Edited += (_, e) => OnCommentEdited (this, e);

			this.ButtonReleaseEvent += (object o, ButtonReleaseEventArgs args) => {
				// Right click event
				if (args.Event.Type == Gdk.EventType.ButtonRelease && args.Event.Button == 3) {
					RightClicked(this, args);
				}
			};
		}

		public TreeSelection GetSelectedResource () {
			return Selection;
		}
	}
}

