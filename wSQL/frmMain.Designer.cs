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
         this.statusStrip1 = new System.Windows.Forms.StatusStrip();
         this.toolStrip1 = new System.Windows.Forms.ToolStrip();
         this.panel3 = new System.Windows.Forms.Panel();
         this.tabContainer = new System.Windows.Forms.TabControl();
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
         this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
         this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
         this.panel3.SuspendLayout();
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
         this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
         this.tabContainer.Location = new System.Drawing.Point(0, 0);
         this.tabContainer.Name = "tabContainer";
         this.tabContainer.SelectedIndex = 0;
         this.tabContainer.Size = new System.Drawing.Size(942, 421);
         this.tabContainer.TabIndex = 0;
         this.tabContainer.SelectedIndexChanged += new System.EventHandler(this.tabContainer_SelectedIndexChanged);
         // 
         // mainMenu
         // 
         this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.scriptToolStripMenuItem});
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
         // openFileDialog1
         // 
         this.openFileDialog1.FileName = "openFileDialog1";
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
   }
}

