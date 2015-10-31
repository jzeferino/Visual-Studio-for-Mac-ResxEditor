using Gtk;
using MonoDevelop.Ide.Gui;
using ResxEditor.Core.Views;

namespace ResxEditor
{
	public class ResxEditorView : AbstractViewContent
	{
		Gtk.Widget Container {
			get;
			set;
		}

		public ResxEditorView() {
			ResourceEditor = new ResourceEditorView ();
			Gtk.HPaned container = new Gtk.HPaned ();
			container.Add (ResourceEditor);

			Container = container;
			Container.ShowAll ();
		}

		public ResourceEditorView ResourceEditor {
			get;
			private set;
		}

		string CurrentFile {
			get;
			set;
		}

		#region implemented abstract members of AbstractBaseViewContent

		public override Widget Control {
			get {
				return Container;
			}
		}

		#endregion

		#region implemented abstract members of AbstractViewContent

		public override void Load (string fileName)
		{
			return;
		}

		#endregion
	}
}

