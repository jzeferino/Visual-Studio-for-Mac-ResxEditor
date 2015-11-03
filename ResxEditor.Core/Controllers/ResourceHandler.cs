using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace ResxEditor.Core.Controllers
{
	public class ResourceHandler
	{
		public List<ResXDataNode> Resources {
			get;
			private set;
		}

		public ResourceHandler (string fileName)
		{
			Resources = new List<ResXDataNode> ();
			using (var reader = new ResXResourceReader (fileName) { UseResXDataNodes = true }) {
				LoadResources (reader);
			}
		}

		void LoadResources(ResXResourceReader resxReader) {
			IDictionaryEnumerator enumerator = resxReader.GetEnumerator ();
			while (enumerator.MoveNext ()) {
				Resources.Add (enumerator.Value as ResXDataNode);
			}
		}

		public int RemoveResource(string name) {
			return Resources.RemoveAll (resource => resource.Name == name);
		}

		public void AddResource(string name, string value, string comment = null) {
			Resources.Add (new ResXDataNode (name, value) { Comment = comment });
		}

		public void WriteToFile(string fileName) {
			using (var resxWriter = new ResXResourceWriter (fileName)) {

				Resources.ForEach (resxWriter.AddResource);

				if (Resources.Count == 0) {
					resxWriter.AddMetadata ("", "");
				}
			}
		}
	}
}

