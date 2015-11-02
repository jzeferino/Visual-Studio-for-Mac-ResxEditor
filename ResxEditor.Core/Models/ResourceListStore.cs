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
//			resourceWriter = new ResXResourceWriter (filename);
////			resourceWriter.Close ();
//			ResXResourceReader resourceReader = new ResXResourceReader (filename);
//			IDictionaryEnumerator dict = resourceReader.GetEnumerator();
//			while (dict.MoveNext ())
//				this.AppendValues (new ResourceModel(dict.Key as string, dict.Value as string));
		}

		public void AppendValues(IResourceModel item) {
			this.AppendValues (item.Name, item.Value);
		}

		public bool SetColumnValue(string path, int column, string value) {
			TreeIter iter;
			if (! this.GetIter(out iter, new TreePath(path))) {
				return false;
			} else {
				this.SetValue (iter, column, value);
				return true;
			}
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

		public string GetName(TreeIter iter) {
			return this.GetValue (iter, 0) as string;
		}
	}

}

