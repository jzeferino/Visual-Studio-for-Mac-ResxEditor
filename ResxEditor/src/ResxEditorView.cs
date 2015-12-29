using System;
using Gtk;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Ide;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Controllers;

namespace ResxEditor
{
	public class ResxEditorView : AbstractViewContent, IAttachableViewContent
	{
		IResourceController Controller {
			get;
			set;
		}

		Widget Container {
			get;
			set;
		}

		Document Document {
			get;
			set;
		}

		public ResxEditorView(Document document = null) {
			Document = document;
			Controller = new ResourceController ();
			HPaned container = new HPaned ();
			container.Add (Controller.ResourceEditorView);

			AttachListeners ();

			Container = container;
			Container.ShowAll ();
		}

		void AttachListeners() {
			Controller.OnDirtyChanged += (_, isDirty) => {
				IsDirty = isDirty;
			};
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

		public override void Save (string fileName)
		{
			Controller.Save (fileName);
		}

		#endregion

		#region IAttachableViewContent
		public override string TabPageLabel {
			get { return "ResxEditor"; }
		}

		void IAttachableViewContent.Selected ()
		{
			if (Document == null)
				return;

			var buffer = Document.GetContent< MonoDevelop.Ide.Editor.TextEditor >();

//			info.Start ();
//			ComparisonWidget.UpdateLocalText ();
//			var buffer = info.Document.GetContent<MonoDevelop.Ide.Editor.TextEditor> ();
//			if (buffer != null) {
//				var loc = buffer.CaretLocation;
//				int line = loc.Line < 1 ? 1 : loc.Line;
//				int column = loc.Column < 1 ? 1 : loc.Column;
//				ComparisonWidget.OriginalEditor.SetCaretTo (line, column);
//			}
//
//			if (ComparisonWidget.Allocation.Height == 1 && ComparisonWidget.Allocation.Width == 1) {
//				ComparisonWidget.SizeAllocated += HandleComparisonWidgetSizeAllocated;
//			} else {
//				HandleComparisonWidgetSizeAllocated (null, new Gtk.SizeAllocatedArgs ());
//			}
//
//			widget.UpdatePatchView ();
		}

		void IAttachableViewContent.Deselected ()
		{
//			throw new NotImplementedException ();
		}

		void IAttachableViewContent.BeforeSave ()
		{
//			throw new NotImplementedException ();
		}

		void IAttachableViewContent.BaseContentChanged ()
		{
//			throw new NotImplementedException ();
		}

		#endregion
	}
}

