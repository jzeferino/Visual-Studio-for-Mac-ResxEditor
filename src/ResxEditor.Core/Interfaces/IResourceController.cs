using System;
using ResxEditor.Core.Views;

namespace ResxEditor.Core.Interfaces
{

	public interface IResourceController
	{
		event EventHandler<bool> OnDirtyChanged;

		string Filename {
			get;
			set;
		}

		IResourceListStore StoreController {
			get;
			set;
		}

		ResourceEditorView ResourceEditorView {
			get;
			set;
		}

		void AddNewResource();
		void RemoveCurrentResource();

		void Load(string fileName);
		void Save(string fileName);
	}

}

