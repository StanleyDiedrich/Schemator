namespace Schemator
{
    partial class Uontrol
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
            this.openfilebtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // openfilebtn
            // 
            this.openfilebtn.BackColor = System.Drawing.Color.Coral;
            this.openfilebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.openfilebtn.Location = new System.Drawing.Point(38, 32);
            this.openfilebtn.Name = "openfilebtn";
            this.openfilebtn.Size = new System.Drawing.Size(217, 23);
            this.openfilebtn.TabIndex = 0;
            this.openfilebtn.Text = "Select CSV";
            this.openfilebtn.UseVisualStyleBackColor = false;
            this.openfilebtn.Click += new System.EventHandler(this.openfilebtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Coral;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(38, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(217, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "S.C.H.E.M.A.T.O.R";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Uontrol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(289, 181);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.openfilebtn);
            this.Name = "Uontrol";
            this.Text = "UControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button openfilebtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}