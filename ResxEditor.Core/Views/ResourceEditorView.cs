using System;
using Gtk;
using ResxEditor.Core.Models;
using ResxEditor.Core.Interfaces;

namespace ResxEditor.Core.Views
{
	public class ResourceEditorView : Gtk.VBox
	{
		public ResourceEditorView () : base()
		{
			ResourceList = new ResourceList ();
			ResourceControlBar = new ResourceControlBar ();

			ResourceControlBar.OnAddResource += (sender, e) => {
				if (this.OnAddResource != null)
					this.OnAddResource (this, e);
			};
			ResourceControlBar.OnRemoveResource += (sender, e) => {
				if (OnRemoveResource != null)
					OnRemoveResource (this, e);
			};

			ScrolledWindow listContainer = new ScrolledWindow ();
			listContainer.Add (ResourceList);

			this.PackStart (ResourceControlBar, false, true, 5);
			this.PackEnd (listContainer);
		}

		public event EventHandler OnAddResource;
		public event EventHandler OnRemoveResource;

		public ResourceList ResourceList {
			get;
			set;
		}

		ResourceControlBar ResourceControlBar {
			get;
			set;
		}

		public void Load (string filename) {
			return;
		}
	}
}

