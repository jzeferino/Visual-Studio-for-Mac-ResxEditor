using ResxEditor.Core.Interfaces;

namespace ResxEditor.Core.Models
{
	public class ResourceModel : IResourceModel
	{

		public ResourceModel(string name, string value, string comment = null) {
			Name = name;
			Value = value;
			Comment = comment;
		}

		public string Name {
			get;
			set;
		}

		public string Value {
			get;
			set;
		}

		public string Comment {
			get;
			set;
		}
	}
}

