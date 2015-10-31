using System;
using Gtk;
using ResxEditor.Core.Models;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Controllers;

namespace ResxEditor.Core.Views
{
	public class ResourceEditorView : Gtk.VBox
	{
		public ResourceEditorView () : base()
		{
			IResourceListStore store = new ResourceListStore ();
			ResourceList = new ResourceList (store);
			ResourceControlBar = new ResourceControlBar ();

			ScrolledWindow listContainer = new ScrolledWindow ();
			listContainer.Add (ResourceList);

			ResourceControlBar.AddResourceButton.Clicked += (_, __) => Controller.RemoveCurrentResource();

			ResourceControlBar.RemoveResourceButton.Clicked += (object sender, EventArgs e) => {
				TreeIter iter;
				TreeSelection selection = ResourceList.GetSelectedResource();
				selection.GetSelected(out iter);
				(ResourceList.Model as ListStore).Remove(ref iter);
			};

			this.PackStart (ResourceControlBar, false, true, 5);
			this.PackEnd (listContainer);
		}

		public event EventHandler OnRemoveResource;

		public IResourceController Controller {
			get;
			private set;
		}

		public ResourceList ResourceList {
			get;
			private set;
		}

		public ResourceControlBar ResourceControlBar {
			get;
			private set;
		}

		public void Load (string filename) {
			return;
		}
	}
}

