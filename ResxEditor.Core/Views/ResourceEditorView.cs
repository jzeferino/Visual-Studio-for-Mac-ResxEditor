using System;
using Gtk;

namespace ResxEditor.Core.Views
{
	public class ResourceEditorView : Gtk.VBox
	{
		public ResourceEditorView () : base()
		{
			ResourceList = new ResourceList ();
			ResourceControlBar = new ResourceControlBar ();

			ResourceControlBar.OnAddResource += (sender, e) => OnAddResource (this, e);
			ResourceControlBar.OnRemoveResource += (sender, e) => OnRemoveResource (this, e);

			ScrolledWindow listContainer = new ScrolledWindow ();
			listContainer.Add (ResourceList);

			PackStart (ResourceControlBar, false, true, 5);
			PackEnd (listContainer);
		}

		public event EventHandler OnAddResource;
		public event EventHandler OnRemoveResource;

		public ResourceList ResourceList {
			get;
			set;
		}

		public ResourceControlBar ResourceControlBar {
			get;
			set;
		}

		public void Load (string filename) {
			return;
		}
	}
}

