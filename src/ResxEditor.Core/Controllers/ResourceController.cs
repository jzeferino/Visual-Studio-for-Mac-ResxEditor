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
			ResourceEditorView.ResourceList.RightClicked += (sender, e) => {
				var selectedRows = ResourceEditorView.ResourceList.GetSelectedResource().GetSelectedRows();
				if (selectedRows.Length > 0) {
					var contextMenu = new CellContextMenu (this, StoreController, selectedRows);
					contextMenu.Popup ();
				} else {
					var contextMenu = new NoCellContextMenu(this);
					contextMenu.Popup ();
				}
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
				string oldName = StoreController.GetName(new TreePath(e.Path));

				m_resxHandler.RemoveResource(oldName);
				m_resxHandler.AddResource(e.NextText, string.Empty);

				StoreController.SetName (e.Path, e.NextText);
				OnDirtyChanged(this, true);
			};
			ResourceEditorView.ResourceList.OnValueEdited += (_, e) => {
				string name = StoreController.GetName(new TreePath(e.Path));

				m_resxHandler.RemoveResource(name);
				m_resxHandler.AddResource(name, e.NextText);

				StoreController.SetValue (e.Path, e.NextText);
				OnDirtyChanged (this, true);
			};
			ResourceEditorView.ResourceList.OnCommentEdited += (_, e) => {
				TreePath path = new TreePath(e.Path);
				string name = StoreController.GetName(path);
				string value = StoreController.GetValue(path);

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
			TreePath[] selectedPaths = ResourceEditorView.ResourceList.GetSelectedResource ().GetSelectedRows ();

			foreach (var selectedPath in selectedPaths) {
				string name = StoreController.GetName (selectedPath);
				StoreController.Remove (selectedPath);
				if (m_resxHandler.RemoveResource (name) > 0) {
					OnDirtyChanged (this, true);
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

