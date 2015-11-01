using System;

namespace ResxEditor.Core.Views
{
	public class AddResourceButton : Gtk.Button
	{
		static readonly Gtk.Label label = new Gtk.Label("Add Resource");

		public AddResourceButton(): base(label) {}
	}

	public class RemoveResourceButton : Gtk.Button
	{
		static readonly Gtk.Label label = new Gtk.Label("Remove Resource");

		public RemoveResourceButton(): base(label) {}
	}

	public class ResourceControlBar : Gtk.HButtonBox
	{
		public event EventHandler OnAddResource;
		public event EventHandler OnRemoveResource;

		AddResourceButton AddResourceButton {
			get;
			set;
		}

		RemoveResourceButton RemoveResourceButton {
			get;
			set;
		}

		public ResourceControlBar ()
		{
			AddResourceButton = new AddResourceButton ();
			RemoveResourceButton = new RemoveResourceButton ();

			AddResourceButton.Clicked += (sender, e) => {
				if (OnAddResource != null)
					OnAddResource (this, e);
			};
			RemoveResourceButton.Clicked += (sender, e) => {
				if (OnRemoveResource != null)
					OnRemoveResource (this, e);
			};

			PackStart (AddResourceButton, true, true, 5);
			PackEnd (RemoveResourceButton, true, true, 5);
		}
	}
}

