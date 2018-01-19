using System;
using Gtk;
using Gdk;
using System.Collections.Generic;

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


		public LocalizationColumn NameColumn { get; private set;}

		private void AddCol(LocalizationColumn col, int index){
			this.AppendColumn (col);
			col.Data.Add ("text", index);
			col.AddAttribute("text",index);
		}


		public ResourceList () : base()
		{
			NameColumn = new LocalizationColumn (true) { Title = "Name" };
			var ValueColumn = new LocalizationColumn (true) { Title = "Value" };
			var CommentColumn = new LocalizationColumn (true) { Title = "Comment" };

			AddCol (NameColumn, 0);
			AddCol (ValueColumn, 1);
			AddCol (CommentColumn, 2);


			NameColumn.Edited += (_, e) => {
				OnNameEdited(this, e);
				OnResourceAdded(this, e.NextText);
				SetCursor (new TreePath (e.Path), ValueColumn, true);
			};
			ValueColumn.Edited += (_, e) => {
				OnValueEdited (this, e);
				SetCursor (new TreePath (e.Path), CommentColumn, true);
			};
			CommentColumn.Edited += (_, e) => { 
				OnCommentEdited (this, e);
			};

			this.ButtonReleaseEvent += (object o, ButtonReleaseEventArgs args) => {
				// Right click event
				if (args.Event.Type == Gdk.EventType.ButtonRelease && args.Event.Button == 3) {
					RightClicked(this, args);
				}
			};
			this.KeyPressEvent += OnUserKeyPress;
		}


	
		public TreeSelection GetSelectedResource () {
			return Selection;
		}

		public void OnUserKeyPress(object o, KeyPressEventArgs args){
			if (args.Event.Key == Gdk.Key.Tab) {
				//get a pointer in the tree to the selected item
				TreePath path;
				TreeViewColumn col;
				var resList = o as ResourceList;
				resList.GetCursor (out path,out col);
				if (col != null) {
					//read updated values from the control in focus which currently is always an Gtk.Entry.
					var next = resList.FocusChild as Entry;	
					//sanity check that we're within the expected range of values
					var selectedIndex = (int)col.Data ["text"]; 
					if (selectedIndex < Columns.Length) {
						col.CellRenderers [0].StopEditing (false);
						//Call the save changes handler for the column 
						//this also handles the change in tab focus via SetCursor 
						((LocalizationColumn)col).InvokeEdited(new ResourceEditedEventArgs(){Path = path.ToString(), NextText = next.Text});
						args.RetVal = true; //eat the event
					} 
				}
			}
		}

	}
}

