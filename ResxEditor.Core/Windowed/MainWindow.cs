using System;
using Gtk;
using ResxEditor.Core.Views;

public partial class MainWindow: Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		this.SetSizeRequest (500, 500);
		this.Add (new ResourceEditorView());
		//"Windowed/Test.resx"
		this.ShowAll ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
