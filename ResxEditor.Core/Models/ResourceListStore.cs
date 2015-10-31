using System.Collections;
using System.Resources;
using Gtk;
using ResxEditor.Core.Interfaces;

//Image img = Image.FromFile("en-AU.jpg");
//ResXResourceWriter rsxw = new ResXResourceWriter("en-AU.resx"); 
//rsxw.AddResource("en-AU.jpg",img);
//rsxw.Close();

namespace ResxEditor.Core.Models
{
	public class ResourceListStore : ListStore, IResourceListStore {
		ResXResourceWriter resourceWriter;

		public ResourceListStore() : base(typeof(string), typeof(string))
		{

		}

		public void AppendValues(IResourceModel item) {
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

