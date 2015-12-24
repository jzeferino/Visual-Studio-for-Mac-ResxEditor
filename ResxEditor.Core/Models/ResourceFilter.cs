using System;
using Gtk;

namespace ResxEditor.Core.Models
{
	public class ResourceFilter : TreeModelFilter
	{
		public ResourceFilter(Func<string> GetFilterText, TreeModel childModel, TreePath root) : base(childModel, root) {
			VisibleFunc = new TreeModelFilterVisibleFunc ((model, iter) => {
				var key = model.GetValue(iter, 0) as string;
				var value = model.GetValue(iter, 1);
				var comment = model.GetValue(iter, 2);
				if (
					string.IsNullOrEmpty(GetFilterText ()) ||
					string.IsNullOrEmpty(key) ||
					key.Contains(GetFilterText ()) ||
					value != null && value.ToString().Contains(GetFilterText ()) ||
					comment != null && comment.ToString().Contains(GetFilterText ())
				) {
					return true;
				}
				return false;
			});
		}
	}
}
