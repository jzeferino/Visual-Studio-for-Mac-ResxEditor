using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;

namespace ResxEditor
{
	public class SubviewAttachmentHandler : CommandHandler
	{
		protected override void Run ()
		{
			MonoDevelop.Ide.IdeApp.Workbench.ActiveDocumentChanged += HandleDocumentChanged;
		}

		static void HandleDocumentChanged (object sender, EventArgs e)
		{
			var document = MonoDevelop.Ide.IdeApp.Workbench.ActiveDocument;
			try {
				if (document == null || !document.IsFile) {
					return;
				}

				if (string.Equals(document.FileName.Extension, ".resx", StringComparison.InvariantCultureIgnoreCase)) {
					TryAttachView<ResxEditorView>(document);
				}

				var project = document.Project;
//				if (document == null || !document.IsFile || document.Window.FindView<IDiffView> () >= 0)
//					return;

//				WorkspaceObject project = document.Project;
//				if (project == null) {
//					// Fix for broken .csproj and .sln files not being seen as having a project.
//					foreach (var projItem in MonoDevelop.Ide.IdeApp.Workspace.GetAllItems<UnknownSolutionItem> ()) {
//						if (projItem.FileName == document.FileName) {
//							project = projItem;
//						}
//					}
//
//					if (project == null)
//						return;
//				}

//				var repo = VersionControlService.GetRepository (project);
//				if (repo == null)
//					return;
//
//				var versionInfo = repo.GetVersionInfo (document.FileName, VersionInfoQueryFlags.IgnoreCache);
//				if (!versionInfo.IsVersioned)
//					return;
//
//				var item = new VersionControlItem (repo, project, document.FileName, false, null);
//				var vcInfo = new VersionControlDocumentInfo (document.PrimaryView, item, item.Repository);
//				TryAttachView <IDiffView> (document, vcInfo, DiffCommand.DiffViewHandlers);
//				TryAttachView <IBlameView> (document, vcInfo, BlameCommand.BlameViewHandlers);
//				TryAttachView <ILogView> (document, vcInfo, LogCommand.LogViewHandlers);
//				TryAttachView <IMergeView> (document, vcInfo, MergeCommand.MergeViewHandlers);
			} catch (Exception ex) {
				// If a user is hitting this, it will show a dialog box every time they
				// switch to a document or open a document, so suppress the crash dialog
				// This bug *should* be fixed already, but it's hard to tell.
				LoggingService.LogInternalError (ex);
			}
		}

		static void TryAttachView <T>(Document document) where T : IAttachableViewContent
		{
			document.Window.AttachViewContent (new ResxEditorView (document));
		}
	}
}

