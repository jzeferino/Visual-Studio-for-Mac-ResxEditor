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

		bool RemoveCurrentResource();
		void Load(string fileName);
	}

}

