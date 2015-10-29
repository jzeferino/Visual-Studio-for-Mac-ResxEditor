using System;
using MonoDevelop.Ide.Gui;
using Xwt;
using ResxEditor.Core;
using ResxEditor.Core.Views;

namespace ResxEditor
{
	public class ResxEditorView : AbstractXwtViewContent
	{
		ScrollView window;

		public ResxEditorView() {
			//
//			Table t = new Table ();
//			t.Add (new Label ("One:"), 0, 1, 0, 1);
//			t.Add (new TextEntry (), 1, 2, 0, 1);
//			t.Add (new Label ("Two:"), 0, 1, 1, 2);
//			t.Add (new TextEntry (), 1, 2, 1, 2);
//			t.Add (new Label ("Three:"), 0, 1, 2, 3);
//			t.Add (new TextEntry (), 1, 2, 2, 3);
			//
			window = new ScrollView (Xwt.Toolkit.CurrentEngine.WrapWidget(new ResourceList ()));
		}

		#region implemented abstract members of AbstractViewContent

		public override void Load (string fileName)
		{
//			throw new NotImplementedException (); // TODO
		}

		#endregion

		#region implemented abstract members of AbstractXwtViewContent

		public override Xwt.Widget Widget {
			get { return window; }
		}

		#endregion
		
	}
}

