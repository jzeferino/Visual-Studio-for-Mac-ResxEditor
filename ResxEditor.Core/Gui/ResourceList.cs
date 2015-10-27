using System;
using Gtk;

namespace ResxEditor.Core
{
	public interface IListViewColumn {
		CellRenderer Renderer { get; }
		Type ColumnType { get; }
	}

	public class ResourceEditedEventArgs : EventArgs {
		public string Path { get; set; }
		public string NextText { get; set; }
	}

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

	public class ResourceModel {
		string m_name;
		string m_value;

		public ResourceModel(string name, string value) {
			m_name = name;
			m_value = value;
		}

		public string Name {
			get { return m_name; }
			set { m_name = value; }
		}

		public string Value {
			get { return m_value; }
			set { m_value = value; }
		}
	}

	public class LocalizationColumn : TreeViewColumn, IListViewColumn {
		CellRenderer m_renderer;
		readonly Type m_columnType = typeof(string);

		public event EventHandler<ResourceEditedEventArgs> Edited;

		public LocalizationColumn(bool editable = false) : base()
		{
			CellRendererText textRenderer = new CellRendererText () { Editable = editable };

			textRenderer.Edited += (_, args) => this.Edited (this, new ResourceEditedEventArgs () {
				Path = args.Path,
				NextText = args.NewText
			});

			textRenderer.EditingCanceled += (object sender, EventArgs e) => {
				Console.WriteLine();
			};

			m_renderer = textRenderer;
			m_columnType = typeof(string);

			this.PackStart (m_renderer, true);
		}

		public void AddAttribute(string attribute, int position) {
			this.AddAttribute (this.m_renderer, attribute, position);
		}

		#region IListViewColumn implementation
		public CellRenderer Renderer {
			get { return m_renderer; }
		}
		public Type ColumnType {
			get { return m_columnType; }
		}
		#endregion
	}

	public class ResourceList : TreeView
	{
		readonly LocalizationColumn nameColumn;
		readonly LocalizationColumn valueColumn;
		readonly ResourceListStore store;

		public ResourceList () : base()
		{
			nameColumn = new LocalizationColumn (true) { Title = "Name" };
			valueColumn = new LocalizationColumn (true) { Title = "Value" };

			this.AppendColumn (nameColumn);
			this.AppendColumn (valueColumn);

			nameColumn.AddAttribute ("text", 0);
			valueColumn.AddAttribute ("text", 1);

			nameColumn.Edited += (_, e) => store.SetName (e.Path, e.NextText);
			valueColumn.Edited += (_, e) => store.SetValue (e.Path, e.NextText);

			store = new ResourceListStore ();

			//
			store.AppendValues (new ResourceModel ("test1", "test2"));
			for (int i = 0; i < 100; i++) {
				store.AppendValues (new ResourceModel ("test" + i, null));
			}
			//

			this.Model = store;
		}
	}
}

