namespace wSQL
{
   partial class frmMain
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
         this.components = new System.ComponentModel.Container();
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.panel3 = new System.Windows.Forms.Panel();
         this.tabContainer = new System.Windows.Forms.TabControl();
         this.tabPage1 = new System.Windows.Forms.TabPage();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
         this.mainMenu = new System.Windows.Forms.MenuStrip();
         this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
         this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
         this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.scriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.runScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
         this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.autosaveOnRunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.highlightCurrentLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.panel3.SuspendLayout();
         this.tabContainer.SuspendLayout();
         this.tabPage1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
         this.mainMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // statusStrip1
         // 
         this.statusStrip1.Location = new System.Drawing.Point(0, 470);
         this.statusStrip1.Name = "statusStrip1";
         this.statusStrip1.Size = new System.Drawing.Size(942, 22);
         this.statusStrip1.TabIndex = 2;
         this.statusStrip1.Text = "statusStrip1";
         // 
         // toolStrip1
         // 
         this.toolStrip1.Location = new System.Drawing.Point(0, 24);
         this.toolStrip1.Name = "toolStrip1";
         this.toolStrip1.Size = new System.Drawing.Size(942, 25);
         this.toolStrip1.TabIndex = 4;
         this.toolStrip1.Text = "toolStrip1";
         // 
         // panel3
         // 
         this.panel3.Controls.Add(this.tabContainer);
         this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel3.Location = new System.Drawing.Point(0, 49);
         this.panel3.Name = "panel3";
         this.panel3.Size = new System.Drawing.Size(942, 421);
         this.panel3.TabIndex = 6;
         // 
         // tabContainer
         // 
         this.tabContainer.Controls.Add(this.tabPage1);
         this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabContainer.Location = new System.Drawing.Point(0, 0);
         this.tabContainer.Name = "tabContainer";
         this.tabContainer.SelectedIndex = 0;
         this.tabContainer.Size = new System.Drawing.Size(942, 421);
         this.tabContainer.TabIndex = 0;
         this.tabContainer.SelectedIndexChanged += new System.EventHandler(this.tabContainer_SelectedIndexChanged);
         // 
         // tabPage1
         // 
         this.tabPage1.Controls.Add(this.splitContainer1);
         this.tabPage1.Location = new System.Drawing.Point(4, 22);
         this.tabPage1.Name = "tabPage1";
         this.tabPage1.Size = new System.Drawing.Size(934, 395);
         this.tabPage1.TabIndex = 0;
         this.tabPage1.Text = "tabPage1";
         this.tabPage1.UseVisualStyleBackColor = true;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 0);
         this.splitContainer1.Name = "splitContainer1";
         this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.fastColoredTextBox1);
         this.splitContainer1.Size = new System.Drawing.Size(934, 395);
         this.splitContainer1.SplitterDistance = 311;
         this.splitContainer1.TabIndex = 0;
         // 
         // fastColoredTextBox1
         // 
         this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
         this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(179, 14);
         this.fastColoredTextBox1.BackBrush = null;
         this.fastColoredTextBox1.CharHeight = 14;
         this.fastColoredTextBox1.CharWidth = 8;
         this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
         this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
         this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.fastColoredTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F);
         this.fastColoredTextBox1.IsReplaceMode = false;
         this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
         this.fastColoredTextBox1.Name = "fastColoredTextBox1";
         this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
         this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
         this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
         this.fastColoredTextBox1.Size = new System.Drawing.Size(934, 311);
         this.fastColoredTextBox1.TabIndex = 0;
         this.fastColoredTextBox1.Text = "fastColoredTextBox1";
         this.fastColoredTextBox1.Zoom = 100;
         // 
         // mainMenu
         // 
         this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptToolStripMenuItem,
            this.optionsToolStripMenuItem});
         this.mainMenu.Location = new System.Drawing.Point(0, 0);
         this.mainMenu.Name = "mainMenu";
         this.mainMenu.Size = new System.Drawing.Size(942, 24);
         this.mainMenu.TabIndex = 10;
         this.mainMenu.Text = "menuStrip1";
         // 
         // fileToolStripMenuItem
         // 
         this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripMenuItem3,
            this.closeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.recentFilesToolStripMenuItem,
            this.toolStripMenuItem4,
            this.exitToolStripMenuItem});
         this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
         this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileToolStripMenuItem.Text = "&File";
         // 
         // newToolStripMenuItem
         // 
         this.newToolStripMenuItem.Name = "newToolStripMenuItem";
         this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
         this.newToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.newToolStripMenuItem.Text = "&New";
         this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
         // 
         // openToolStripMenuItem
         // 
         this.openToolStripMenuItem.Name = "openToolStripMenuItem";
         this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.openToolStripMenuItem.Text = "&Open";
         this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
         // 
         // toolStripMenuItem3
         // 
         this.toolStripMenuItem3.Name = "toolStripMenuItem3";
         this.toolStripMenuItem3.Size = new System.Drawing.Size(182, 6);
         // 
         // closeToolStripMenuItem
         // 
         this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
         this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
         this.closeToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.closeToolStripMenuItem.Text = "Close";
         this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
         // 
         // toolStripMenuItem2
         // 
         this.toolStripMenuItem2.Name = "toolStripMenuItem2";
         this.toolStripMenuItem2.Size = new System.Drawing.Size(182, 6);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.saveToolStripMenuItem.Text = "&Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
         // 
         // saveAsToolStripMenuItem
         // 
         this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
         this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.saveAsToolStripMenuItem.Text = "Save &as ...";
         this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
         // 
         // saveAllToolStripMenuItem
         // 
         this.saveAllToolStripMenuItem.Enabled = false;
         this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
         this.saveAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
         this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.saveAllToolStripMenuItem.Text = "Save all";
         this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
         // 
         // toolStripMenuItem1
         // 
         this.toolStripMenuItem1.Name = "toolStripMenuItem1";
         this.toolStripMenuItem1.Size = new System.Drawing.Size(182, 6);
         // 
         // recentFilesToolStripMenuItem
         // 
         this.recentFilesToolStripMenuItem.Enabled = false;
         this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
         this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.recentFilesToolStripMenuItem.Text = "Recent files";
         this.recentFilesToolStripMenuItem.Click += new System.EventHandler(this.recentFilesToolStripMenuItem_Click);
         // 
         // toolStripMenuItem4
         // 
         this.toolStripMenuItem4.Name = "toolStripMenuItem4";
         this.toolStripMenuItem4.Size = new System.Drawing.Size(182, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
         // 
         // scriptToolStripMenuItem
         // 
         this.scriptToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runScriptToolStripMenuItem,
            this.toolStripMenuItem5});
         this.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem";
         this.scriptToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
         this.scriptToolStripMenuItem.Text = "Script";
         // 
         // runScriptToolStripMenuItem
         // 
         this.runScriptToolStripMenuItem.Name = "runScriptToolStripMenuItem";
         this.runScriptToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
         this.runScriptToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
         this.runScriptToolStripMenuItem.Text = "Run script";
         this.runScriptToolStripMenuItem.Click += new System.EventHandler(this.runScriptToolStripMenuItem_Click);
         // 
         // toolStripMenuItem5
         // 
         this.toolStripMenuItem5.Name = "toolStripMenuItem5";
         this.toolStripMenuItem5.Size = new System.Drawing.Size(143, 6);
         // 
         // optionsToolStripMenuItem
         // 
         this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autosaveOnRunToolStripMenuItem,
            this.highlightCurrentLineToolStripMenuItem});
         this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
         this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
         this.optionsToolStripMenuItem.Text = "Options";
         // 
         // autosaveOnRunToolStripMenuItem
         // 
         this.autosaveOnRunToolStripMenuItem.Checked = true;
         this.autosaveOnRunToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this.autosaveOnRunToolStripMenuItem.Name = "autosaveOnRunToolStripMenuItem";
         this.autosaveOnRunToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
         this.autosaveOnRunToolStripMenuItem.Text = "Autosave on run";
         this.autosaveOnRunToolStripMenuItem.Click += new System.EventHandler(this.autosaveOnRunToolStripMenuItem_Click);
         // 
         // openFileDialog1
         // 
         this.openFileDialog1.FileName = "openFileDialog1";
         // 
         // highlightCurrentLineToolStripMenuItem
         // 
         this.highlightCurrentLineToolStripMenuItem.Checked = true;
         this.highlightCurrentLineToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
         this.highlightCurrentLineToolStripMenuItem.Name = "highlightCurrentLineToolStripMenuItem";
         this.highlightCurrentLineToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
         this.highlightCurrentLineToolStripMenuItem.Text = "Highlight current line";
         this.highlightCurrentLineToolStripMenuItem.Click += new System.EventHandler(this.highlightCurrentLineToolStripMenuItem_Click);
         // 
         // frmMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(942, 492);
         this.Controls.Add(this.panel3);
         this.Controls.Add(this.toolStrip1);
         this.Controls.Add(this.statusStrip1);
         this.Controls.Add(this.mainMenu);
         this.MainMenuStrip = this.mainMenu;
         this.Name = "frmMain";
         this.Text = "Form1";
         this.panel3.ResumeLayout(false);
         this.tabContainer.ResumeLayout(false);
         this.tabPage1.ResumeLayout(false);
         this.splitContainer1.Panel1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
         this.mainMenu.ResumeLayout(false);
         this.mainMenu.PerformLayout();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.StatusStrip statusStrip1;
      private System.Windows.Forms.ToolStrip toolStrip1;
      private System.Windows.Forms.Panel panel3;
      private System.Windows.Forms.MenuStrip mainMenu;
      private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
      private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
      private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
      private System.Windows.Forms.TabControl tabContainer;
      private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
      private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
      private System.Windows.Forms.SaveFileDialog saveFileDialog1;
      private System.Windows.Forms.ToolStripMenuItem scriptToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem runScriptToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
      private System.Windows.Forms.OpenFileDialog openFileDialog1;
      private System.Windows.Forms.TabPage tabPage1;
      private System.Windows.Forms.SplitContainer splitContainer1;
      private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
      private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem autosaveOnRunToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem highlightCurrentLineToolStripMenuItem;
   }
}

