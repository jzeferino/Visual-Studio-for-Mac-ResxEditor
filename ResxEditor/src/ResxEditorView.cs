using System;
using Gtk;
using MonoDevelop.Ide.Gui;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Controllers;

namespace ResxEditor
{
	public class ResxEditorView : AbstractViewContent
	{
		IResourceController Controller {
			get;
			set;
		}

		Widget Container {
			get;
			set;
		}

		public ResxEditorView() {
			Controller = new ResourceController ();
			HPaned container = new HPaned ();
			container.Add (Controller.ResourceEditorView);

			Container = container;
			Container.ShowAll ();
		}

		string CurrentFile {
			get;
			set;
		}

		#region AbstractViewContent Overrides
		string m_contentName;
		public override string ContentName {
			get {
				return m_contentName;
			}
			set {
				if (value != m_contentName) {
					m_contentName = value;
					OnContentNameChanged (EventArgs.Empty);
				}
			}
		}
		#endregion

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
			ContentName = fileName;
			Controller.Load (fileName);
		}

		#endregion
	}
}

