using System;
using ResxEditor.Core.Views;

namespace ResxEditor.Core.Interfaces
{

	public interface IResourceController
	{
		event EventHandler OnFileSaved;

		string Filename {
			get;
			set;
		}

		IResourceListStore Store {
			get;
			set;
		}

		IResourceFileHandler FileHandler {
			get;
			set;
		}

		ResourceEditorView ResourceEditorView {
			get;
			set;
		}

		void AddNewResource();
		bool RemoveCurrentResource();
		void Load(string fileName);
	}

}

