using System;
using Gtk;
using MonoDevelop.Ide.Gui;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Controllers;
using System.Threading.Tasks;
using MonoDevelop.Core;
using MonoDevelop.Components;

namespace ResxEditor.UI
{
    public class ResxEditorView : ViewContent
    {
        private IResourceController Controller { get; set; }
        private Widget Container { get; set; }
        private FilePath CurrentFile { get; set; }

        public override Control Control => Container;

        public ResxEditorView()
        {
            Controller = new ResourceController();
            Controller.OnDirtyChanged += ControllerOnDirtyChanged;

            Container = new HPaned
            {
                Controller.ResourceEditorView
            };

            Container.ShowAll();
        }

        private void ControllerOnDirtyChanged(object sender, bool isDirty) => IsDirty = isDirty;

        public override Task Load(FileOpenInformation fileOpenInformation)
        {
            ContentName = fileOpenInformation.FileName.FullPath;
            CurrentFile = fileOpenInformation.FileName;
            return Task.Run(() => Controller.Load(fileOpenInformation.FileName.FullPath));
        }

        public override Task Save(FileSaveInformation fileSaveInformation)
        {
            return Task.Run(() => Controller.Save(fileSaveInformation.FileName.FullPath));
        }

        public override Task Save()
        {
            return CurrentFile != null ? Task.Run(() => Controller.Save(CurrentFile.FullPath)) : base.Save();
        }

        public override void Dispose()
        {
            if (Controller != null)
            {
                Controller.OnDirtyChanged -= ControllerOnDirtyChanged;
            }
            base.Dispose();
        }
    }
}

