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

		public string GetName(TreePath path) {
			TreeIter iter;

			return Model.GetIter (out iter, path) ? Model.GetValue (iter, (int)Enums.ResourceColumns.Name) as string : null;
		}

		public string GetValue (TreePath path) {
			TreeIter iter;

			return Model.GetIter (out iter, path) ? Model.GetValue (iter, (int)Enums.ResourceColumns.Value) as string : null;
		}

		public string GetComment (TreePath path)
		{
			TreeIter iter;

			return Model.GetIter (out iter, path) ? Model.GetValue (iter, (int)Enums.ResourceColumns.Comment) as string : null;
		}

		public bool GetIter (out TreeIter iter, TreePath path)
		{
			return Model.GetIter (out iter, path);
		}

		public TreeIter Prepend ()
		{
			return BaseModel.Prepend ();
		}

		public TreePath GetPath (TreeIter iter)
		{
			return BaseModel.GetPath (iter);
		}

		/// <summary>
		/// Remove the specified resource at the path
		/// </summary>
		/// <param name="path">Returns true if the path is a valid path.</param>
		public bool Remove (TreePath path) {
			TreeIter iter;
			if (IsFilterable) {
				ResourceFilter.GetIter (out iter, path);
				iter = ResourceFilter.ConvertIterToChildIter (iter);
			} else {
				BaseModel.GetIter (out iter, path);
			}

			return BaseModel.Remove (ref iter);
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

