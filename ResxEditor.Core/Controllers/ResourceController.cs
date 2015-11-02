using System;
using System.Resources;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;
using ResxEditor.Core.Views;
using System.Collections;
using System.ComponentModel.Design;

namespace ResxEditor.Core.Controllers
{
	public class ResourceController : IResourceController, IDisposable
	{
		public event EventHandler<bool> OnDirtyChanged;
		public event EventHandler OnFileSaved;

		ResourceHandler m_resxHandler;

		public ResourceController ()
		{
			ResourceEditorView = new ResourceEditorView ();

//			ResourceList = resourceList;
			Store = new ResourceListStore ();
			ResourceEditorView.ResourceList.Model = (TreeModel)Store;

			AttachListeners ();
		}

		void AttachListeners () {
			ResourceEditorView.OnAddResource += (_, __) => AddNewResource ();
			ResourceEditorView.OnRemoveResource += (_, __) => {
				RemoveCurrentResource ();
			};

			ResourceEditorView.ResourceList.OnNameEdited += (_, e) => {
				TreeIter iter;
				Store.GetIter(out iter, new TreePath(e.Path));
				string oldName = Store.GetName(iter);

				m_resxHandler.RemoveResource(oldName);
				m_resxHandler.AddResource(e.NextText, string.Empty);

				Store.SetName (e.Path, e.NextText);
				OnDirtyChanged(this, true);
			};
			ResourceEditorView.ResourceList.OnValueEdited += (_, e) => {
				TreeIter iter;
				Store.GetIter(out iter, new TreePath(e.Path));
				string name = Store.GetName(iter);

				m_resxHandler.RemoveResource(name);
				m_resxHandler.AddResource(name, e.NextText);

				Store.SetValue (e.Path, e.NextText);
				OnDirtyChanged (this, true);
			};
		}

		public void AddNewResource() {
			TreeIter iter = Store.Prepend();
			TreePath path = Store.GetPath(iter);
			ResourceEditorView.ResourceList.SetCursor(path, ResourceEditorView.ResourceList.NameColumn, true);
			OnDirtyChanged(this, true);
		}

		public void RemoveCurrentResource() {
			TreeIter iter;
			TreeSelection selection = ResourceEditorView.ResourceList.GetSelectedResource();

			if (selection.GetSelected (out iter)) {
				string name = Store.GetName (iter);
				if (m_resxHandler.RemoveResource (name) > 0) {

					Store.Remove (ref iter);
					OnDirtyChanged (this, true);

				}
			}
		}

		public string Filename {
			get;
			set;
		}

		public IResourceListStore Store {
			get;
			set;
		}

		public IResourceFileHandler FileHandler {
			get;
			set;
		}

		public ResourceEditorView ResourceEditorView {
			get;
			set;
		}

		public void Load (string fileName)
		{
			Filename = fileName;
			m_resxHandler = new ResourceHandler (fileName);
			m_resxHandler.Resources.ForEach ((resource) => {
				if (resource.FileRef == null) {
					object value = resource.GetValue((ITypeResolutionService) null);
					var str = value as string;
//					if (str != null) {
						Store.AppendValues (new ResourceModel (resource.Name, str));
//					} else {
//						throw new NotImplementedException();
//					}

				} else {
					throw new NotImplementedException();
				}
			});
		}

		public void Save(string fileName) {
			m_resxHandler.WriteToFile (fileName);
			OnDirtyChanged(this, false);
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			m_resxHandler.Dispose ();
		}

		#endregion
	}
}

