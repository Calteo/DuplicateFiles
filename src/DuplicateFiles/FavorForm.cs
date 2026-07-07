using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Toolbox;

namespace DuplicateFiles
{
	public partial class FavorForm : Form
	{
		public FavorForm()
		{
			InitializeComponent();
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string RootPath { get; set; } = "";

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FavorPath
		{
			get => textBoxFovor.Text;
			set => textBoxFovor.Text = value;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string OverPath
		{
			get => textBoxOver.Text;
			set => textBoxOver.Text = value;
		}

		private void TextBoxTextChanged(object sender, EventArgs e)
		{
			buttonOk.Enabled =
				textBoxFovor.Text.NotEmpty() && Directory.Exists(textBoxFovor.Text)
				&& textBoxOver.Text.NotEmpty() && Directory.Exists(textBoxOver.Text);
		}

		private void ButtonSelectFavorClick(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = FavorPath.NotEmpty() ? FavorPath : RootPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				FavorPath = folderBrowserDialog.SelectedPath;
			}
		}

		private void ButtonSelectOverClick(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = OverPath.NotEmpty() ? OverPath : RootPath;
			if (!folderBrowserDialog.SelectedPath.EndsWith('\\'))
			{
				folderBrowserDialog.SelectedPath += '\\';
			}
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				OverPath = folderBrowserDialog.SelectedPath;
			}
		}
	}
}
