using System;
using Gtk;

namespace ResxEditor.Core
{
	public class ResourceEditorView : Gtk.VBox
	{
		public ResourceEditorView () : base()
		{
			ScrolledWindow listContainer = new ScrolledWindow ();
			listContainer.Add (new ResourceList ());

			this.PackStart (new ResourceControlBar (), false, true, 5);
			this.PackEnd (listContainer);
		}
	}
}

