
namespace Emulator
{
    partial class MainWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.hexViewer = new System.Windows.Forms.TextBox();
            this.asciiViewer = new System.Windows.Forms.TextBox();
            this.registerViewer = new System.Windows.Forms.TextBox();
            this.ProgramDisplay = new System.Windows.Forms.TabControl();
            this.asmOutput = new System.Windows.Forms.TabPage();
            this.asmOutputBox = new System.Windows.Forms.TextBox();
            this.binOutput = new System.Windows.Forms.TabPage();
            this.binOutputBox = new System.Windows.Forms.TextBox();
            this.consoleDisplayOut = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadvasmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConsoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assemblerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assmbleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emulatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAsm = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.ProgramDisplay.SuspendLayout();
            this.asmOutput.SuspendLayout();
            this.binOutput.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer1.Location = new System.Drawing.Point(-5, 26);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Desktop;
            this.splitContainer1.Panel2.Controls.Add(this.consoleDisplayOut);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(1024, 587);
            this.splitContainer1.SplitterDistance = 417;
            this.splitContainer1.TabIndex = 2;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Black;
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ProgramDisplay);
            this.splitContainer2.Size = new System.Drawing.Size(1024, 417);
            this.splitContainer2.SplitterDistance = 667;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.registerViewer);
            this.splitContainer3.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer3_Panel2_Paint);
            this.splitContainer3.Size = new System.Drawing.Size(667, 417);
            this.splitContainer3.SplitterDistance = 296;
            this.splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.hexViewer);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.asciiViewer);
            this.splitContainer4.Size = new System.Drawing.Size(667, 296);
            this.splitContainer4.SplitterDistance = 477;
            this.splitContainer4.TabIndex = 0;
            // 
            // hexViewer
            // 
            this.hexViewer.AcceptsReturn = true;
            this.hexViewer.AcceptsTab = true;
            this.hexViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexViewer.BackColor = System.Drawing.Color.Black;
            this.hexViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hexViewer.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexViewer.ForeColor = System.Drawing.SystemColors.Window;
            this.hexViewer.Location = new System.Drawing.Point(6, 4);
            this.hexViewer.Multiline = true;
            this.hexViewer.Name = "hexViewer";
            this.hexViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.hexViewer.Size = new System.Drawing.Size(468, 293);
            this.hexViewer.TabIndex = 2;
            this.hexViewer.WordWrap = false;
            this.hexViewer.TextChanged += new System.EventHandler(this.hexViewer_TextChanged);
            // 
            // asciiViewer
            // 
            this.asciiViewer.AcceptsReturn = true;
            this.asciiViewer.AcceptsTab = true;
            this.asciiViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.asciiViewer.BackColor = System.Drawing.Color.Black;
            this.asciiViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.asciiViewer.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asciiViewer.ForeColor = System.Drawing.SystemColors.Window;
            this.asciiViewer.Location = new System.Drawing.Point(3, -1);
            this.asciiViewer.Multiline = true;
            this.asciiViewer.Name = "asciiViewer";
            this.asciiViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.asciiViewer.Size = new System.Drawing.Size(180, 297);
            this.asciiViewer.TabIndex = 1;
            this.asciiViewer.WordWrap = false;
            // 
            // registerViewer
            // 
            this.registerViewer.AcceptsReturn = true;
            this.registerViewer.AcceptsTab = true;
            this.registerViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.registerViewer.BackColor = System.Drawing.Color.Black;
            this.registerViewer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.registerViewer.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerViewer.ForeColor = System.Drawing.SystemColors.Window;
            this.registerViewer.Location = new System.Drawing.Point(5, 3);
            this.registerViewer.Multiline = true;
            this.registerViewer.Name = "registerViewer";
            this.registerViewer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.registerViewer.Size = new System.Drawing.Size(659, 114);
            this.registerViewer.TabIndex = 1;
            this.registerViewer.WordWrap = false;
            this.registerViewer.TextChanged += new System.EventHandler(this.registerViewer_TextChanged);
            // 
            // ProgramDisplay
            // 
            this.ProgramDisplay.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ProgramDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramDisplay.Controls.Add(this.asmOutput);
            this.ProgramDisplay.Controls.Add(this.binOutput);
            this.ProgramDisplay.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgramDisplay.Location = new System.Drawing.Point(3, 3);
            this.ProgramDisplay.Multiline = true;
            this.ProgramDisplay.Name = "ProgramDisplay";
            this.ProgramDisplay.SelectedIndex = 0;
            this.ProgramDisplay.Size = new System.Drawing.Size(347, 411);
            this.ProgramDisplay.TabIndex = 0;
            // 
            // asmOutput
            // 
            this.asmOutput.Controls.Add(this.asmOutputBox);
            this.asmOutput.Location = new System.Drawing.Point(4, 4);
            this.asmOutput.Name = "asmOutput";
            this.asmOutput.Padding = new System.Windows.Forms.Padding(3);
            this.asmOutput.Size = new System.Drawing.Size(339, 383);
            this.asmOutput.TabIndex = 0;
            this.asmOutput.Text = "asm";
            this.asmOutput.UseVisualStyleBackColor = true;
            this.asmOutput.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // asmOutputBox
            // 
            this.asmOutputBox.AcceptsReturn = true;
            this.asmOutputBox.AcceptsTab = true;
            this.asmOutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.asmOutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.asmOutputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.asmOutputBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.asmOutputBox.ForeColor = System.Drawing.SystemColors.Window;
            this.asmOutputBox.Location = new System.Drawing.Point(0, 0);
            this.asmOutputBox.Multiline = true;
            this.asmOutputBox.Name = "asmOutputBox";
            this.asmOutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.asmOutputBox.Size = new System.Drawing.Size(339, 383);
            this.asmOutputBox.TabIndex = 0;
            this.asmOutputBox.WordWrap = false;
            this.asmOutputBox.TextChanged += new System.EventHandler(this.asmOutputBox_TextChanged);
            // 
            // binOutput
            // 
            this.binOutput.Controls.Add(this.binOutputBox);
            this.binOutput.Location = new System.Drawing.Point(4, 4);
            this.binOutput.Name = "binOutput";
            this.binOutput.Padding = new System.Windows.Forms.Padding(3);
            this.binOutput.Size = new System.Drawing.Size(339, 383);
            this.binOutput.TabIndex = 1;
            this.binOutput.Text = "bin";
            this.binOutput.UseVisualStyleBackColor = true;
            // 
            // binOutputBox
            // 
            this.binOutputBox.AcceptsReturn = true;
            this.binOutputBox.AcceptsTab = true;
            this.binOutputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.binOutputBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.binOutputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.binOutputBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.binOutputBox.ForeColor = System.Drawing.SystemColors.Window;
            this.binOutputBox.Location = new System.Drawing.Point(0, 0);
            this.binOutputBox.Multiline = true;
            this.binOutputBox.Name = "binOutputBox";
            this.binOutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.binOutputBox.Size = new System.Drawing.Size(339, 383);
            this.binOutputBox.TabIndex = 1;
            this.binOutputBox.TextChanged += new System.EventHandler(this.binDisplay_TextChanged);
            // 
            // consoleDisplayOut
            // 
            this.consoleDisplayOut.AcceptsReturn = true;
            this.consoleDisplayOut.AcceptsTab = true;
            this.consoleDisplayOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.consoleDisplayOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.consoleDisplayOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.consoleDisplayOut.Font = new System.Drawing.Font("Consolas", 10F);
            this.consoleDisplayOut.ForeColor = System.Drawing.SystemColors.Window;
            this.consoleDisplayOut.Location = new System.Drawing.Point(5, 2);
            this.consoleDisplayOut.Multiline = true;
            this.consoleDisplayOut.Name = "consoleDisplayOut";
            this.consoleDisplayOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.consoleDisplayOut.Size = new System.Drawing.Size(1019, 164);
            this.consoleDisplayOut.TabIndex = 1;
            this.consoleDisplayOut.WordWrap = false;
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.Black;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.assemblerToolStripMenuItem,
            this.emulatorToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1016, 24);
            this.mainMenu.TabIndex = 3;
            this.mainMenu.Text = "Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadvasmToolStripMenuItem,
            this.saveConsoleToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadvasmToolStripMenuItem
            // 
            this.loadvasmToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.loadvasmToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.loadvasmToolStripMenuItem.Name = "loadvasmToolStripMenuItem";
            this.loadvasmToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.loadvasmToolStripMenuItem.Text = "Load .vasm";
            this.loadvasmToolStripMenuItem.Click += new System.EventHandler(this.loadvasmToolStripMenuItem_Click);
            // 
            // saveConsoleToolStripMenuItem
            // 
            this.saveConsoleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.saveConsoleToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.saveConsoleToolStripMenuItem.Name = "saveConsoleToolStripMenuItem";
            this.saveConsoleToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.saveConsoleToolStripMenuItem.Text = "Save Console";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // assemblerToolStripMenuItem
            // 
            this.assemblerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.assmbleToolStripMenuItem});
            this.assemblerToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.assemblerToolStripMenuItem.Name = "assemblerToolStripMenuItem";
            this.assemblerToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.assemblerToolStripMenuItem.Text = "Assembler";
            // 
            // assmbleToolStripMenuItem
            // 
            this.assmbleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.assmbleToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.assmbleToolStripMenuItem.Name = "assmbleToolStripMenuItem";
            this.assmbleToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.assmbleToolStripMenuItem.Text = "Start";
            this.assmbleToolStripMenuItem.Click += new System.EventHandler(this.assmbleToolStripMenuItem_Click);
            // 
            // emulatorToolStripMenuItem
            // 
            this.emulatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem});
            this.emulatorToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Control;
            this.emulatorToolStripMenuItem.Name = "emulatorToolStripMenuItem";
            this.emulatorToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.emulatorToolStripMenuItem.Text = "Emulator";
            this.emulatorToolStripMenuItem.Click += new System.EventHandler(this.emulatorToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // loadAsm
            // 
            this.loadAsm.FileName = "loadAsm";
            this.loadAsm.Filter = "vasm files (*.vasm)|*.vasm|All files (*.*)|*.*";
            this.loadAsm.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1016, 614);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.mainMenu);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainWindow";
            this.Text = "nBit Emulator GUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ProgramDisplay.ResumeLayout(false);
            this.asmOutput.ResumeLayout(false);
            this.asmOutput.PerformLayout();
            this.binOutput.ResumeLayout(false);
            this.binOutput.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl ProgramDisplay;
        private System.Windows.Forms.TabPage asmOutput;
        private System.Windows.Forms.TabPage binOutput;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadvasmToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog loadAsm;
        private System.Windows.Forms.TextBox asmOutputBox;
        private System.Windows.Forms.TextBox consoleDisplayOut;
        private System.Windows.Forms.ToolStripMenuItem assemblerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assmbleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConsoleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox binOutputBox;
        private System.Windows.Forms.ToolStripMenuItem emulatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TextBox hexViewer;
        private System.Windows.Forms.TextBox asciiViewer;
        private System.Windows.Forms.TextBox registerViewer;
    }
}