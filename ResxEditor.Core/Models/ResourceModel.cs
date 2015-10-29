using System;

namespace ResxEditor.Core.Models
{
	public class ResourceModel {

		public ResourceModel(string name, string value) {
			Name = name;
			Value = value;
		}

		public string Name {
			get;
			set;
		}

		public string Value {
			get;
			set;
		}
	}
}

