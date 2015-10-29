using System;
using Gtk;

namespace ResxEditor.Core.Views
{
	public class ResourceEditorView : Gtk.VBox
	{
		public ResourceList ResourceList {
			get;
			private set;
		}

		public ResourceControlBar ResourceControlBar {
			get;
			private set;
		}

		public ResourceEditorView () : base()
		{
			ResourceList = new ResourceList ();
			ResourceControlBar = new ResourceControlBar ();

			ScrolledWindow listContainer = new ScrolledWindow ();
			listContainer.Add (ResourceList);

			ResourceControlBar.AddResourceButton.Clicked += (object sender, EventArgs e) => {
				ListStore resourceList = ResourceList.Model as ListStore;
				TreeIter iter = resourceList.Prepend();
				TreePath path = resourceList.GetPath(iter);
				ResourceList.SetCursor(path, ResourceList.NameColumn, true);
			};

			ResourceControlBar.RemoveResourceButton.Clicked += (object sender, EventArgs e) => {
				TreeIter iter;
				TreeSelection selection = ResourceList.GetSelectedResource();
				selection.GetSelected(out iter);
				(ResourceList.Model as ListStore).Remove(ref iter);
			};

			this.PackStart (ResourceControlBar, false, true, 5);
			this.PackEnd (listContainer);
		}
	}
}

