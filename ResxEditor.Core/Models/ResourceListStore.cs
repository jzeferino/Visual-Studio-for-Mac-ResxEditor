using Gtk;

namespace ResxEditor.Core.Models
{
	public class ResourceListStore : ListStore {
		public ResourceListStore() : base(typeof(string), typeof(string)) {
		}

		public void AppendValues(ResourceModel item) {
			this.AppendValues (item.Name, item.Value);
		}

		public bool SetName(string path, string nextName) {
			TreeIter iter;
			if (! this.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				this.SetValue (iter, 0, nextName);
				return true;
			}
		}

		public bool SetValue(string path, string nextValue) {
			TreeIter iter;
			if (! this.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				this.SetValue (iter, 1, nextValue);
				return true;
			}
		}
	}

}

