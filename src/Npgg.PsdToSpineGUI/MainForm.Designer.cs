
namespace Npgg.PsdToSpineGUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.PsdFileLabel = new System.Windows.Forms.Label();
            this.FileSelectButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BoneMapLabel = new System.Windows.Forms.Label();
            this.BoneMapSelectButton = new System.Windows.Forms.Button();
            this.ConvertButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PsdFileLabel);
            this.panel1.Controls.Add(this.FileSelectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 79);
            this.panel1.TabIndex = 1;
            // 
            // FileNameLabel
            // 
            this.PsdFileLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PsdFileLabel.Location = new System.Drawing.Point(0, 0);
            this.PsdFileLabel.Name = "FileNameLabel";
            this.PsdFileLabel.Size = new System.Drawing.Size(684, 79);
            this.PsdFileLabel.TabIndex = 2;
            this.PsdFileLabel.Text = "label1";
            // 
            // FileSelectButton
            // 
            this.FileSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.FileSelectButton.Location = new System.Drawing.Point(684, 0);
            this.FileSelectButton.Name = "FileSelectButton";
            this.FileSelectButton.Size = new System.Drawing.Size(116, 79);
            this.FileSelectButton.TabIndex = 1;
            this.FileSelectButton.Text = "PSD선택";
            this.FileSelectButton.UseVisualStyleBackColor = true;
            this.FileSelectButton.Click += new System.EventHandler(this.FileSelectButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BoneMapLabel);
            this.panel2.Controls.Add(this.BoneMapSelectButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 79);
            this.panel2.TabIndex = 2;
            // 
            // BoneMapLabel
            // 
            this.BoneMapLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BoneMapLabel.Location = new System.Drawing.Point(0, 0);
            this.BoneMapLabel.Name = "BoneMapLabel";
            this.BoneMapLabel.Size = new System.Drawing.Size(684, 79);
            this.BoneMapLabel.TabIndex = 2;
            this.BoneMapLabel.Text = "label1";
            // 
            // BoneMapSelectButton
            // 
            this.BoneMapSelectButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.BoneMapSelectButton.Location = new System.Drawing.Point(684, 0);
            this.BoneMapSelectButton.Name = "BoneMapSelectButton";
            this.BoneMapSelectButton.Size = new System.Drawing.Size(116, 79);
            this.BoneMapSelectButton.TabIndex = 1;
            this.BoneMapSelectButton.Text = "본맵선택";
            this.BoneMapSelectButton.UseVisualStyleBackColor = true;
            this.BoneMapSelectButton.Click += new System.EventHandler(this.BoneMapSelectButton_Click);
            // 
            // ConvertButton
            // 
            this.ConvertButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.ConvertButton.Location = new System.Drawing.Point(684, 158);
            this.ConvertButton.Name = "ConvertButton";
            this.ConvertButton.Size = new System.Drawing.Size(116, 292);
            this.ConvertButton.TabIndex = 3;
            this.ConvertButton.Text = "Convert";
            this.ConvertButton.UseVisualStyleBackColor = true;
            this.ConvertButton.Click += new System.EventHandler(this.ConvertButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConvertButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button FileSelectButton;
        private System.Windows.Forms.Label PsdFileLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label BoneMapLabel;
        private System.Windows.Forms.Button BoneMapSelectButton;
        private System.Windows.Forms.Button ConvertButton;
    }
}

