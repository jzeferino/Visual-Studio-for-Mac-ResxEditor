using System;
using Gtk;
using MonoDevelop.Ide.Gui;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Controllers;
using System.Threading.Tasks;

namespace ResxEditor
{
    public class ResxEditorView : ViewContent
    {
        IResourceController Controller
        {
            get;
            set;
        }

        Widget Container
        {
            get;
            set;
        }

        public ResxEditorView()
        {
            Controller = new ResourceController();
            HPaned container = new HPaned();
            container.Add(Controller.ResourceEditorView);

            AttachListeners();

            Container = container;
            Container.ShowAll();
        }

        void AttachListeners()
        {
            Controller.OnDirtyChanged += (_, isDirty) =>
            {
                this.IsDirty = isDirty;
            };
        }

        MonoDevelop.Core.FilePath CurrentFile
        {
            get;
            set;
        }

        #region implemented abstract members of AbstractBaseViewContent

        public override MonoDevelop.Components.Control Control
        {
            get
            {
                return Container;
            }
        }

        #endregion

        public override Task Load(FileOpenInformation fileOpenInformation)
        {
            //this has to be the fullpath otherwise when we save it thinks we're saving a new file
            //this.Save() isn't called the base.Save(string filename) is :s.  
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
            Task toRet = null;
            if (CurrentFile != null)
                toRet = Task.Run(() => Controller.Save(CurrentFile.FullPath));
            else
                toRet = base.Save();
            return toRet;
        }
    }
}

