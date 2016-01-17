using System;
using System.Linq;
using Gdk;
using Gtk;
using ResxEditor.Core.Interfaces;

namespace ResxEditor.Core.Views
{
	public class CopyCellMenuItem : MenuItem
	{
		Func<TreePath, string> GetValueFromRow { get; }
		TreePath[] SelectedRows { get; }
		EventButton EventButton { get; }

		public CopyCellMenuItem (TreePath[] selectedRows, EventButton eventButton, string label, Func<TreePath, string> getValue) : base (label)
		{
			if (selectedRows.Length == 0) {
				throw new IndexOutOfRangeException ("Missing selected resource rows");
			}

			SelectedRows = selectedRows;
			EventButton = eventButton;
			GetValueFromRow = getValue;

			ButtonReleaseEvent += (o, e) => OnCopy ();
		}

		void OnCopy () {
			if (SelectedRows.Length > 1) {
				Console.WriteLine ("Multiple rows selected. Currently not supported: defaulting to first.");
			}

			var selectedPath = SelectedRows.First ();

			Clipboard clipboard = GetClipboard (Gdk.Selection.Clipboard);
			clipboard.Text = GetValueFromRow.Invoke (selectedPath);
		}
	}

	public class CellContextMenu : Menu
	{
		public CellContextMenu (TreePath[] selectedRows, IResourceListStore resourceController, EventButton eventButton)
		{
			#region ArrangeGUI
			Append (new CopyCellMenuItem (selectedRows, eventButton, "Copy Name", resourceController.GetName));
			Append (new CopyCellMenuItem (selectedRows, eventButton, "Copy Value", resourceController.GetValue));
			Append (new CopyCellMenuItem (selectedRows, eventButton, "Copy Comment", resourceController.GetComment));

			ShowAll ();
			#endregion
		}
	}
}

