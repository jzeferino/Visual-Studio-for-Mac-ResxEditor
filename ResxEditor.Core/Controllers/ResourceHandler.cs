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
			m_resxReader = new ResXResourceReader (fileName);

			LoadResources ();

			m_resxReader.Close ();
		}

		void LoadResources() {
			IDictionaryEnumerator enumerator = m_resxReader.GetEnumerator ();
			while (enumerator.MoveNext ()) {
				Resources.Add (new ResXDataNode (enumerator.Key as string, enumerator.Value));
			}
		}

		public int RemoveResource(string name) {
			return Resources.RemoveAll (resource => resource.Name == name);
		}

		public void AddResource(string name, string value) {
			Resources.Add (new ResXDataNode (name, value));
		}

		public void WriteToFile(string fileName) {
			m_resxWriter = new ResXResourceWriter (fileName);

			Resources.ForEach (m_resxWriter.AddResource);

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

