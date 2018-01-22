using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Projects;

namespace ResxEditor.UI
{
    public class DisplayBindings : IViewDisplayBinding
    {
        public bool CanUseAsDefault => true;
        public string Name => "Resx Editor";

        public ViewContent CreateContent(FilePath fileName, string mimeType, Project ownerProject)
        {
            return new ResxEditorView();
        }

        public bool CanHandle(FilePath fileName, string mimeType, Project ownerProject)
        {
            return mimeType == "text/microsoft-resx" || fileName.Extension == ".resx";
        }
    }
}

