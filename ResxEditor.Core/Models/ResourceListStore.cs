using Gtk;

//Image img = Image.FromFile("en-AU.jpg");
//ResXResourceWriter rsxw = new ResXResourceWriter("en-AU.resx"); 
//rsxw.AddResource("en-AU.jpg",img);
//rsxw.Close();

namespace ResxEditor.Core.Models
{
	public class ResourceListStore : ListStore {

		public ResourceListStore() : base(typeof(string), typeof(string), typeof(string)) {}

	}

}

