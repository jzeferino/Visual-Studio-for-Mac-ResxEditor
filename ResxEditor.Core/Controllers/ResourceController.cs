using System;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;
using ResxEditor.Core.Views;
using System.ComponentModel.Design;

namespace ResxEditor.Core.Controllers
{
	public class ResourceController : IResourceController
	{
		public event EventHandler<bool> OnDirtyChanged;
		public event EventHandler RemoveFailed;

		ResourceHandler m_resxHandler;

		public ResourceController ()
		{
			ResourceEditorView = new ResourceEditorView ();
			StoreController = new ResourceStoreController(() => ResourceEditorView.ResourceControlBar.FilterEntry.Text);
			ResourceEditorView.ResourceControlBar.FilterEntry.Changed += (_, e) => StoreController.Refilter ();
			ResourceEditorView.ResourceList.OnResourceAdded += (_, e) => {
				ResourceEditorView.ResourceControlBar.FilterEntry.Text = "";
				StoreController.Refilter();
			};
			ResourceEditorView.ResourceList.Model = StoreController.Model;

			AttachListeners ();
		}

		void AttachListeners () {
			ResourceEditorView.OnAddResource += (_, __) => AddNewResource ();
			ResourceEditorView.OnRemoveResource += (_, __) => RemoveCurrentResource ();

			ResourceEditorView.ResourceList.OnNameEdited += (_, e) => {
				TreeIter iter;
				StoreController.GetIter(out iter, new TreePath(e.Path));
				string oldName = StoreController.GetName(iter);

				m_resxHandler.RemoveResource(oldName);
				m_resxHandler.AddResource(e.NextText, string.Empty);

				StoreController.SetName (e.Path, e.NextText);
				OnDirtyChanged(this, true);
			};
			ResourceEditorView.ResourceList.OnValueEdited += (_, e) => {
				TreeIter iter;
				StoreController.GetIter(out iter, new TreePath(e.Path));
				string name = StoreController.GetName(iter);

				m_resxHandler.RemoveResource(name);
				m_resxHandler.AddResource(name, e.NextText);

				StoreController.SetValue (e.Path, e.NextText);
				OnDirtyChanged (this, true);
			};
			ResourceEditorView.ResourceList.OnCommentEdited += (_, e) => {
				TreeIter iter;
				StoreController.GetIter(out iter, new TreePath(e.Path));
				string name = StoreController.GetName(iter);
				string value = StoreController.GetValue(iter);

				m_resxHandler.RemoveResource(name);
				m_resxHandler.AddResource(name, value, e.NextText);

				StoreController.SetComment (e.Path, e.NextText);
				OnDirtyChanged (this, true);
			};
		}

		public void AddNewResource() {
			TreeIter iter = StoreController.Prepend();
			TreePath path = StoreController.GetPath(iter);
			ResourceEditorView.ResourceList.SetCursor(path, ResourceEditorView.ResourceList.NameColumn, true);
			OnDirtyChanged(this, true);
		}

		public void RemoveCurrentResource() {
			TreeIter iter;
			TreePath[] selectedPaths = ResourceEditorView.ResourceList.GetSelectedResource ().GetSelectedRows ();

			foreach (var selectedPath in selectedPaths) {
				if (StoreController.Remove (selectedPath) && ResourceEditorView.ResourceList.Model.GetIter(out iter, selectedPath)) {
					string name = StoreController.GetName (iter);
					if (m_resxHandler.RemoveResource (name) > 0) {
						OnDirtyChanged (this, true);
					}
				} else {
					if (RemoveFailed != null)
						RemoveFailed (this, null);
				}
			}
		}

		public string Filename {
			get;
			set;
		}

		public IResourceListStore StoreController {
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
					StoreController.AppendValues (new ResourceModel (resource.Name, str, resource.Comment));
				} else {
					throw new NotImplementedException();
				}
			});
		}

		public void Save(string fileName) {
			m_resxHandler.WriteToFile (fileName);
			OnDirtyChanged(this, false);
		}
	}
}

