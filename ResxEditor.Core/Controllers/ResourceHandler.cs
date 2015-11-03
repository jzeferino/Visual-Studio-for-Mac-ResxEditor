using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace ResxEditor.Core.Controllers
{
	public class ResourceHandler : IDisposable
	{
		ResXResourceReader m_resxReader;
		ResXResourceWriter m_resxWriter;

		public List<ResXDataNode> Resources {
			get;
			private set;
		}

		public ResourceHandler (string fileName)
		{
			Resources = new List<ResXDataNode> ();
			m_resxReader = new ResXResourceReader (fileName) { UseResXDataNodes = true };

			LoadResources ();

			m_resxReader.Close ();
		}

		void LoadResources() {
			IDictionaryEnumerator enumerator = m_resxReader.GetEnumerator ();
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
			m_resxWriter = new ResXResourceWriter (fileName);

			Resources.ForEach (m_resxWriter.AddResource);

			if (Resources.Count == 0) {
				m_resxWriter.AddMetadata ("", "");
			}

			m_resxWriter.Close();
		}

		#region IDisposable implementation

		public void Dispose ()
		{
			m_resxReader.Close ();
			m_resxWriter.Close ();
		}

		#endregion
	}
}

