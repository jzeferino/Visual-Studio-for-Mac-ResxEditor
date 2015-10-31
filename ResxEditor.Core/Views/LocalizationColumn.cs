using System;
using Gtk;
using ResxEditor.Core.Interfaces;

namespace ResxEditor.Core.Views
{
	public class LocalizationColumn : TreeViewColumn, IListViewColumn
	{
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
}

