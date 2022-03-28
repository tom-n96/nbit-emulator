
namespace Emulator
{
    partial class Splash
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
            this.Title = new System.Windows.Forms.Label();
            this.subtext = new System.Windows.Forms.Label();
            this.disclaimer = new System.Windows.Forms.Label();
            this.disclaimer2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 50.25F);
            this.Title.Location = new System.Drawing.Point(12, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(614, 76);
            this.Title.TabIndex = 0;
            this.Title.Text = "n-Bit CPU Emulator";
            this.Title.Click += new System.EventHandler(this.label1_Click);
            // 
            // subtext
            // 
            this.subtext.AutoSize = true;
            this.subtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F);
            this.subtext.Location = new System.Drawing.Point(17, 85);
            this.subtext.Name = "subtext";
            this.subtext.Size = new System.Drawing.Size(570, 47);
            this.subtext.TabIndex = 1;
            this.subtext.Text = "C# Edition - v0.1 (01/30/2022)";
            // 
            // disclaimer
            // 
            this.disclaimer.AutoSize = true;
            this.disclaimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.disclaimer.Location = new System.Drawing.Point(20, 157);
            this.disclaimer.Name = "disclaimer";
            this.disclaimer.Size = new System.Drawing.Size(380, 22);
            this.disclaimer.TabIndex = 2;
            this.disclaimer.Text = "This software has not been thoroughly tested. ";
            this.disclaimer.Click += new System.EventHandler(this.disclaimer_Click);
            // 
            // disclaimer2
            // 
            this.disclaimer2.AutoSize = true;
            this.disclaimer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.disclaimer2.Location = new System.Drawing.Point(21, 182);
            this.disclaimer2.Name = "disclaimer2";
            this.disclaimer2.Size = new System.Drawing.Size(649, 22);
            this.disclaimer2.TabIndex = 3;
            this.disclaimer2.Text = "This is a proof of concept and should not be used for any production application." +
    "";
            this.disclaimer2.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(786, 442);
            this.Controls.Add(this.disclaimer2);
            this.Controls.Add(this.disclaimer);
            this.Controls.Add(this.subtext);
            this.Controls.Add(this.Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label subtext;
        private System.Windows.Forms.Label disclaimer;
        private System.Windows.Forms.Label disclaimer2;
    }
}

