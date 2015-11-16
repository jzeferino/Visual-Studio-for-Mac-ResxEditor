using System;
using Gtk;
using ResxEditor.Core.Interfaces;
using ResxEditor.Core.Models;
using ResxEditor.Core.Views;
using System.ComponentModel.Design;

namespace ResxEditor.Core.Models
{
	public class ResourceFilter : TreeModelFilter
	{
		public ResourceFilter(Entry filterEntry, TreeModel childModel, TreePath root) : base(childModel, root) {
			VisibleFunc = new TreeModelFilterVisibleFunc ((model, iter) => {
				var key = model.GetValue(iter, 0).ToString();
				var value = model.GetValue(iter, 1).ToString();
				var comment = model.GetValue(iter, 2).ToString();
				if (
					string.IsNullOrEmpty(filterEntry.Text) ||
					key.Contains(filterEntry.Text) ||
					value.Contains(filterEntry.Text) ||
					comment.Contains(filterEntry.Text)
				) {
					return true;
				}
				return false;
			});
		}
	}
}
