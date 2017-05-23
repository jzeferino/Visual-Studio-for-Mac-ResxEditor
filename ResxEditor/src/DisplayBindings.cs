using System;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;

namespace ResxEditor
{
	public class DisplayBindings : IViewDisplayBinding
	{

		#region IViewDisplayBinding implementation

		public ViewContent CreateContent (FilePath fileName, string mimeType, Project ownerProject)
		{
			return new ResxEditorView ();
		}

		public string Name {
			get { return "Resx Editor"; }
		}

		#endregion

		#region IDisplayBinding implementation

		public bool CanHandle (FilePath fileName, string mimeType, Project ownerProject)
		{
			if (mimeType == "text/microsoft-resx" || fileName.Extension == ".resx") {
				return true;
			} else {
				return false;
			}
		}
        public bool CanUseAsDefault {
			get { return true; }
		}

		#endregion
		
	}
}

