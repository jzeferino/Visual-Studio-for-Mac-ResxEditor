using System;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;

namespace ResxEditor.Core.Controllers
{
	public class ResourceStoreController : IResourceListStore
	{
		public ResourceStoreController (Func<string> GetFilterText)
		{
			BaseModel = new ResourceListStore ();

			this.GetFilterText = GetFilterText;

			ResourceFilter = new ResourceFilter(GetFilterText, BaseModel, null);
		}

		public bool IsFilterable { get { return GetFilterText != null; } }

		Func<string> GetFilterText { get; set; }

		public TreeModel Model {
			get {
				if (IsFilterable) {
					return ResourceFilter;
				}
				return BaseModel;
			}
		}

		ResourceListStore BaseModel { get; set; }

		public void AppendValues(IResourceModel item) {
			BaseModel.AppendValues (item.Name, item.Value, item.Comment);
		}

		public bool SetColumnValue(string path, int column, string value) {
			TreeIter iter;
			if (! BaseModel.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				BaseModel.SetValue (iter, column, value);
				return true;
			}
		}

		public bool SetName(string path, string nextName) {
			TreeIter iter;
			if (! BaseModel.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				BaseModel.SetValue (iter, (int)Enums.ResourceColumns.Name, nextName);
				return true;
			}
		}

		public bool SetValue(string path, string nextValue) {
			TreeIter iter;
			if (! BaseModel.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				BaseModel.SetValue (iter, (int)Enums.ResourceColumns.Value, nextValue);
				return true;
			}
		}

		public bool SetComment(string path, string nextValue) {
			TreeIter iter;
			if (! BaseModel.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				BaseModel.SetValue (iter, (int)Enums.ResourceColumns.Comment, nextValue);
				return true;
			}
		}

		public string GetName(TreeIter iter) {
			return Model.GetValue (iter, (int)Enums.ResourceColumns.Name) as string;
		}

		public string GetValue (TreeIter iter) {
			return BaseModel.GetValue (iter, (int)Enums.ResourceColumns.Value) as string;
		}

		public bool GetIter (out TreeIter iter, TreePath path)
		{
			throw new NotImplementedException ();
		}

		public TreeIter Prepend ()
		{
			throw new NotImplementedException ();
		}

		public TreePath GetPath (TreeIter iter)
		{
			throw new NotImplementedException ();
		}

		public bool Remove (TreePath path) {
			TreeIter iter;
			if (BaseModel.GetIter (out iter, path)) {
				return BaseModel.Remove (ref iter);
			} else {
				return false;
			}
		}

		#region IFilter
		ResourceFilter ResourceFilter { get; set; }

		public void Refilter ()
		{
			ResourceFilter.Refilter ();
		}
		#endregion
	}
}

