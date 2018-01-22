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

		public Gtk.Entry FilterEntry {
			get;
			set;
		}

		public ResourceControlBar ()
		{
			Layout = Gtk.ButtonBoxStyle.Start;
			Spacing = 10;

			AddResourceButton = new AddResourceButton ();
			RemoveResourceButton = new RemoveResourceButton ();
			FilterEntry = new Gtk.Entry ();

			AddResourceButton.Clicked += (sender, e) => OnAddResource (this, e);
			RemoveResourceButton.Clicked += (sender, e) => OnRemoveResource (this, e);

			PackStart (AddResourceButton, false, false, 10);
			PackStart (RemoveResourceButton, false, false, 10);
			PackEnd (FilterEntry);
		}
	}
}

