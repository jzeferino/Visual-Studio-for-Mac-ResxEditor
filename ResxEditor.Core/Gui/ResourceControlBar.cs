using System;
using Gtk;

namespace ResxEditor.Core
{
	public class ResourceControlBar : Gtk.HButtonBox
	{
		public ResourceControlBar () : base()
		{
			this.PackStart (new Button (new Label ("test")));
		}
	}
}

