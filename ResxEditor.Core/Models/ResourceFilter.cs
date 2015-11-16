using System;
using Gtk;

namespace ResxEditor.Core.Models
{
	public class ResourceFilter : TreeModelFilter
	{
		public ResourceFilter(Entry filterEntry, TreeModel childModel, TreePath root) : base(childModel, root) {
			VisibleFunc = new TreeModelFilterVisibleFunc ((model, iter) => {
				var key = model.GetValue(iter, 0) as string;
				var value = model.GetValue(iter, 1);
				var comment = model.GetValue(iter, 2);
				if (
					string.IsNullOrEmpty(filterEntry.Text) ||
					string.IsNullOrEmpty(key) ||
					key.Contains(filterEntry.Text) ||
					value != null && value.ToString().Contains(filterEntry.Text) ||
					comment != null && comment.ToString().Contains(filterEntry.Text)
				) {
					return true;
				}
				return false;
			});
		}
	}
}
