namespace DuplicateFiles
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			layoutPanel = new TableLayoutPanel();
			textBoxFolder = new TextBox();
			buttonSelectFolder = new Button();
			labelFolder = new Label();
			buttonScan = new Button();
			labelProgess = new Label();
			filesListView = new FilesListView();
			columnHeaderName = new ColumnHeader("(none)");
			columnHeaderCount = new ColumnHeader();
			columnHeaderSize = new ColumnHeader();
			columnHeaderFullname = new ColumnHeader();
			contextMenuStrip = new ContextMenuStrip(components);
			openCommand = new ToolStripMenuItem();
			openFolderCommand = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			ignoreCommand = new ToolStripMenuItem();
			keepCommand = new ToolStripMenuItem();
			deleteCommand = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			ignoreFolderCommand = new ToolStripMenuItem();
			imageList = new ImageList(components);
			folderBrowserDialog = new FolderBrowserDialog();
			workerPool = new Toolbox.Forms.WorkerPool(components);
			timer = new System.Windows.Forms.Timer(components);
			layoutPanel.SuspendLayout();
			contextMenuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// layoutPanel
			// 
			layoutPanel.ColumnCount = 3;
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
			layoutPanel.Controls.Add(textBoxFolder, 1, 0);
			layoutPanel.Controls.Add(buttonSelectFolder, 2, 0);
			layoutPanel.Controls.Add(labelFolder, 0, 0);
			layoutPanel.Controls.Add(buttonScan, 2, 2);
			layoutPanel.Controls.Add(labelProgess, 1, 2);
			layoutPanel.Controls.Add(filesListView, 0, 1);
			layoutPanel.Dock = DockStyle.Fill;
			layoutPanel.Location = new Point(0, 0);
			layoutPanel.Name = "layoutPanel";
			layoutPanel.RowCount = 3;
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			layoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
			layoutPanel.Size = new Size(1344, 511);
			layoutPanel.TabIndex = 0;
			// 
			// textBoxFolder
			// 
			textBoxFolder.Dock = DockStyle.Fill;
			textBoxFolder.Location = new Point(123, 3);
			textBoxFolder.Name = "textBoxFolder";
			textBoxFolder.Size = new Size(1098, 31);
			textBoxFolder.TabIndex = 0;
			textBoxFolder.WordWrap = false;
			textBoxFolder.TextChanged += TextBoxFolderTextChanged;
			// 
			// buttonSelectFolder
			// 
			buttonSelectFolder.Dock = DockStyle.Fill;
			buttonSelectFolder.Location = new Point(1227, 3);
			buttonSelectFolder.Name = "buttonSelectFolder";
			buttonSelectFolder.Size = new Size(114, 34);
			buttonSelectFolder.TabIndex = 1;
			buttonSelectFolder.Text = "Select";
			buttonSelectFolder.UseVisualStyleBackColor = true;
			buttonSelectFolder.Click += ButtonSelectFolderClick;
			// 
			// labelFolder
			// 
			labelFolder.Dock = DockStyle.Fill;
			labelFolder.Location = new Point(3, 0);
			labelFolder.Name = "labelFolder";
			labelFolder.Size = new Size(114, 40);
			labelFolder.TabIndex = 2;
			labelFolder.Text = "Folder";
			labelFolder.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// buttonScan
			// 
			buttonScan.Dock = DockStyle.Fill;
			buttonScan.Enabled = false;
			buttonScan.Location = new Point(1227, 474);
			buttonScan.Name = "buttonScan";
			buttonScan.Size = new Size(114, 34);
			buttonScan.TabIndex = 3;
			buttonScan.Text = "Scan";
			buttonScan.UseVisualStyleBackColor = true;
			buttonScan.Click += ButtonScanClick;
			// 
			// labelProgess
			// 
			labelProgess.Dock = DockStyle.Fill;
			labelProgess.Location = new Point(123, 471);
			labelProgess.Name = "labelProgess";
			labelProgess.Size = new Size(1098, 40);
			labelProgess.TabIndex = 4;
			labelProgess.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// filesListView
			// 
			filesListView.Columns.AddRange(new ColumnHeader[] { columnHeaderName, columnHeaderCount, columnHeaderSize, columnHeaderFullname });
			layoutPanel.SetColumnSpan(filesListView, 3);
			filesListView.ContextMenuStrip = contextMenuStrip;
			filesListView.Dock = DockStyle.Fill;
			filesListView.FullRowSelect = true;
			filesListView.GridLines = true;
			filesListView.Location = new Point(3, 43);
			filesListView.Name = "filesListView";
			filesListView.Size = new Size(1338, 425);
			filesListView.SmallImageList = imageList;
			filesListView.TabIndex = 5;
			filesListView.UseCompatibleStateImageBehavior = false;
			filesListView.View = View.Details;
			filesListView.ColumnClick += FilesListViewColumnClick;
			filesListView.DoubleClick += FilesListViewDoubleClick;
			// 
			// columnHeaderName
			// 
			columnHeaderName.Text = "Name";
			columnHeaderName.Width = 200;
			// 
			// columnHeaderCount
			// 
			columnHeaderCount.Text = "Count";
			columnHeaderCount.TextAlign = HorizontalAlignment.Right;
			// 
			// columnHeaderSize
			// 
			columnHeaderSize.Text = "Size";
			columnHeaderSize.TextAlign = HorizontalAlignment.Right;
			columnHeaderSize.Width = 150;
			// 
			// columnHeaderFullname
			// 
			columnHeaderFullname.Text = "Full";
			columnHeaderFullname.Width = 300;
			// 
			// contextMenuStrip
			// 
			contextMenuStrip.ImageScalingSize = new Size(20, 20);
			contextMenuStrip.Items.AddRange(new ToolStripItem[] { openCommand, openFolderCommand, toolStripSeparator1, ignoreCommand, keepCommand, deleteCommand, toolStripSeparator2, ignoreFolderCommand });
			contextMenuStrip.Name = "contextMenuStrip1";
			contextMenuStrip.Size = new Size(166, 160);
			contextMenuStrip.Opening += ContextMenuStripOpening;
			// 
			// openCommand
			// 
			openCommand.Name = "openCommand";
			openCommand.Size = new Size(165, 24);
			openCommand.Text = "Open";
			openCommand.Click += OpenCommandClick;
			// 
			// openFolderCommand
			// 
			openFolderCommand.Name = "openFolderCommand";
			openFolderCommand.Size = new Size(165, 24);
			openFolderCommand.Text = "Open Folder";
			openFolderCommand.Click += OpenFolderCommandClick;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(162, 6);
			// 
			// ignoreCommand
			// 
			ignoreCommand.Name = "ignoreCommand";
			ignoreCommand.Size = new Size(165, 24);
			ignoreCommand.Text = "Ignore";
			ignoreCommand.Click += IgnoreCommandClick;
			// 
			// keepCommand
			// 
			keepCommand.Name = "keepCommand";
			keepCommand.Size = new Size(165, 24);
			keepCommand.Text = "Keep";
			keepCommand.Click += KeepCommandClick;
			// 
			// deleteCommand
			// 
			deleteCommand.Name = "deleteCommand";
			deleteCommand.Size = new Size(165, 24);
			deleteCommand.Text = "Delete";
			deleteCommand.Click += DeleteCommandClick;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(162, 6);
			// 
			// ignoreFolderCommand
			// 
			ignoreFolderCommand.Name = "ignoreFolderCommand";
			ignoreFolderCommand.Size = new Size(165, 24);
			ignoreFolderCommand.Text = "Ignore folder";
			ignoreFolderCommand.Click += IgnoreFolderCommandClick;
			// 
			// imageList
			// 
			imageList.ColorDepth = ColorDepth.Depth32Bit;
			imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = Color.Transparent;
			imageList.Images.SetKeyName(0, "file");
			imageList.Images.SetKeyName(1, "folder");
			// 
			// folderBrowserDialog
			// 
			folderBrowserDialog.Description = "Select folder to scan";
			folderBrowserDialog.UseDescriptionForTitle = true;
			// 
			// workerPool
			// 
			workerPool.Owner = this;
			workerPool.Started += WorkerPoolStarted;
			workerPool.Stopped += WorkerPoolStopped;
			// 
			// timer
			// 
			timer.Interval = 1000;
			timer.Tick += TimerTick;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1344, 511);
			Controls.Add(layoutPanel);
			Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			Margin = new Padding(4);
			Name = "MainForm";
			Text = "DuplciateFiles";
			Shown += MainFormShown;
			layoutPanel.ResumeLayout(false);
			layoutPanel.PerformLayout();
			contextMenuStrip.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel layoutPanel;
		private TextBox textBoxFolder;
		private Button buttonSelectFolder;
		private Label labelFolder;
		private FolderBrowserDialog folderBrowserDialog;
		private Toolbox.Forms.WorkerPool workerPool;
		private Button buttonScan;
		private Label labelProgess;
		private FilesListView filesListView;
		private ColumnHeader columnHeaderName;
		private System.Windows.Forms.Timer timer;
		private ColumnHeader columnHeaderFullname;
		private ImageList imageList;
		private ColumnHeader columnHeaderSize;
		private ContextMenuStrip contextMenuStrip;
		private ToolStripMenuItem ignoreCommand;
		internal ColumnHeader columnHeaderCount;
		private ToolStripMenuItem deleteCommand;
		private ToolStripMenuItem openCommand;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem openFolderCommand;
		private ToolStripMenuItem keepCommand;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem ignoreFolderCommand;
	}
}
