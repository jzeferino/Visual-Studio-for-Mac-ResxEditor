using System;

namespace ResxEditor.Core.Views
{
	public class AddResourceButton : Gtk.Button
	{
		static readonly Gtk.Label label = new Gtk.Label("Add Resource");

		public AddResourceButton(): base(label)
		{
		}
	}

	public class RemoveResourceButton : Gtk.Button
	{
		static readonly Gtk.Label label = new Gtk.Label("Remove Resource");

		public RemoveResourceButton(): base(label) {}
	}

	public class ResourceControlBar : Gtk.HButtonBox
	{
		public AddResourceButton AddResourceButton {
			get;
			private set;
		}

		public RemoveResourceButton RemoveResourceButton {
			get;
			private set;
		}

		public ResourceControlBar ()
		{
			AddResourceButton = new AddResourceButton ();
			RemoveResourceButton = new RemoveResourceButton ();

			PackStart (AddResourceButton, true, true, 5);
			PackEnd (RemoveResourceButton, true, true, 5);
		}
	}
}

