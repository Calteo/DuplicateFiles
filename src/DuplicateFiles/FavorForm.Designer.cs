namespace DuplicateFiles
{
	partial class FavorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			layoutPanel = new TableLayoutPanel();
			buttonOk = new Button();
			buttonCancel = new Button();
			labelFavor = new Label();
			labelOver = new Label();
			textBoxFovor = new TextBox();
			textBoxOver = new TextBox();
			buttonSelectFavor = new Button();
			buttonSelectOver = new Button();
			folderBrowserDialog = new FolderBrowserDialog();
			layoutPanel.SuspendLayout();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.BackColor = Color.Transparent;
			layoutPanel.ColumnCount = 4;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
			layoutPanel.Controls.Add(buttonOk, 2, 3);
			layoutPanel.Controls.Add(buttonCancel, 3, 3);
			layoutPanel.Controls.Add(labelFavor, 0, 0);
			layoutPanel.Controls.Add(labelOver, 0, 1);
			layoutPanel.Controls.Add(textBoxFovor, 1, 0);
			layoutPanel.Controls.Add(textBoxOver, 1, 1);
			layoutPanel.Controls.Add(buttonSelectFavor, 3, 0);
			layoutPanel.Controls.Add(buttonSelectOver, 3, 1);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 4;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
			layoutPanel.Size = new Size(924, 130);
			layoutPanel.TabIndex = 0;
			// 
			// buttonOk
			// 
			buttonOk.DialogResult = DialogResult.OK;
			buttonOk.Dock = DockStyle.Fill;
			buttonOk.Enabled = false;
			buttonOk.Location = new Point(687, 91);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new Size(114, 36);
			buttonOk.TabIndex = 5;
			buttonOk.Text = "&Ok";
			buttonOk.UseVisualStyleBackColor = true;
			// 
			// buttonCancel
			// 
			buttonCancel.DialogResult = DialogResult.Cancel;
			buttonCancel.Dock = DockStyle.Fill;
			buttonCancel.Location = new Point(807, 91);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new Size(114, 36);
			buttonCancel.TabIndex = 6;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			// 
			// labelFavor
			// 
			labelFavor.Dock = DockStyle.Fill;
			labelFavor.Location = new Point(3, 0);
			labelFavor.Name = "labelFavor";
			labelFavor.Size = new Size(114, 42);
			labelFavor.TabIndex = 2;
			labelFavor.Text = "Favor";
			labelFavor.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// labelOver
			// 
			labelOver.Dock = DockStyle.Fill;
			labelOver.Location = new Point(3, 42);
			labelOver.Name = "labelOver";
			labelOver.Size = new Size(114, 42);
			labelOver.TabIndex = 3;
			labelOver.Text = "Over";
			labelOver.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// textBoxFovor
			// 
			layoutPanel.SetColumnSpan(textBoxFovor, 2);
			textBoxFovor.Dock = DockStyle.Fill;
			textBoxFovor.Location = new Point(123, 3);
			textBoxFovor.Name = "textBoxFovor";
			textBoxFovor.Size = new Size(678, 27);
			textBoxFovor.TabIndex = 1;
			textBoxFovor.TextChanged += TextBoxTextChanged;
			// 
			// textBoxOver
			// 
			layoutPanel.SetColumnSpan(textBoxOver, 2);
			textBoxOver.Dock = DockStyle.Fill;
			textBoxOver.Location = new Point(123, 45);
			textBoxOver.Name = "textBoxOver";
			textBoxOver.Size = new Size(678, 27);
			textBoxOver.TabIndex = 3;
			textBoxOver.TextChanged += TextBoxTextChanged;
			// 
			// buttonSelectFavor
			// 
			buttonSelectFavor.Dock = DockStyle.Fill;
			buttonSelectFavor.Location = new Point(807, 3);
			buttonSelectFavor.Name = "buttonSelectFavor";
			buttonSelectFavor.Size = new Size(114, 36);
			buttonSelectFavor.TabIndex = 2;
			buttonSelectFavor.Text = "Select";
			buttonSelectFavor.UseVisualStyleBackColor = true;
			buttonSelectFavor.Click += ButtonSelectFavorClick;
			// 
			// buttonSelectOver
			// 
			buttonSelectOver.Dock = DockStyle.Fill;
			buttonSelectOver.Location = new Point(807, 45);
			buttonSelectOver.Name = "buttonSelectOver";
			buttonSelectOver.Size = new Size(114, 36);
			buttonSelectOver.TabIndex = 4;
			buttonSelectOver.Text = "Select";
			buttonSelectOver.UseVisualStyleBackColor = true;
			buttonSelectOver.Click += ButtonSelectOverClick;
			// 
			// FavorForm
			// 
			AcceptButton = buttonOk;
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			CancelButton = buttonCancel;
			ClientSize = new Size(924, 130);
			Controls.Add(layoutPanel);
			FormBorderStyle = FormBorderStyle.FixedDialog;
			Name = "FavorForm";
			Text = "Favor Folder";
			layoutPanel.ResumeLayout(false);
			layoutPanel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel layoutPanel;
		private Button buttonOk;
		private Button buttonCancel;
		private Label labelFavor;
		private Label labelOver;
		private TextBox textBoxFovor;
		private TextBox textBoxOver;
		private Button buttonSelectFavor;
		private Button buttonSelectOver;
		private FolderBrowserDialog folderBrowserDialog;
	}
}