using System;
using System.Resources;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;
using ResxEditor.Core.Views;
using System.Collections;

namespace ResxEditor.Core.Controllers
{
	public class ResourceController : IResourceController
	{
		ResXResourceReader ResxReader {
			get;
			set;
		}

		public ResourceController ()
		{
			ResourceEditorView = new ResourceEditorView ();

//			ResourceList = resourceList;
			Store = new ResourceListStore ();
			ResourceEditorView.ResourceList.Model = (TreeModel)Store;
		}

		public bool RemoveCurrentResource() {
			TreeIter iter;
			TreeSelection selection = ResourceEditorView.ResourceList.GetSelectedResource();

			selection.GetSelected(out iter);

			Store.Remove(ref iter);

			return false;
		}

		public event EventHandler OnFileSaved;

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
			ResxReader = new ResXResourceReader (fileName);
			IDictionaryEnumerator enumerator = ResxReader.GetEnumerator ();
			while (enumerator.MoveNext ()) {
				Store.AppendValues (new ResourceModel (enumerator.Key as string, enumerator.Value as string));
			}
		}
	}
}

