using Gtk;

namespace ResxEditor.Core.Interfaces
{
	public interface IResourceListStore : IFilterableResourceStore
	{
		bool IsFilterable { get; }
		TreeModel Model { get; }

		void AppendValues (IResourceModel item);

		bool SetName (string path, string nextName);

		bool SetValue (string path, string nextValue);

		bool SetComment (string path, string nextValue);

		bool GetIter (out TreeIter iter, TreePath path);

		string GetName (TreePath path);

		string GetValue (TreePath path);

		string GetComment (TreePath path);

		#region ListStore
		TreeIter Prepend ();

		TreePath GetPath (TreeIter iter);

		bool Remove (TreePath path);
		#endregion
	}
}

