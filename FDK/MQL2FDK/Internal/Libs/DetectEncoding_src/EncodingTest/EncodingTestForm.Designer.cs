namespace EncodingTest
{
    partial class EncodingTestForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.streamReader = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.detected = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stream Reader with encoding detection";
            // 
            // streamReader
            // 
            this.streamReader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.streamReader.Location = new System.Drawing.Point(16, 30);
            this.streamReader.Multiline = true;
            this.streamReader.Name = "streamReader";
            this.streamReader.Size = new System.Drawing.Size(544, 108);
            this.streamReader.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "IMultiLanguage2::DetectInputCodepage ";
            // 
            // detected
            // 
            this.detected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.detected.Location = new System.Drawing.Point(16, 165);
            this.detected.Multiline = true;
            this.detected.Name = "detected";
            this.detected.Size = new System.Drawing.Size(544, 108);
            this.detected.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(485, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // EncodingTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 308);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.detected);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.streamReader);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(580, 340);
            this.MinimumSize = new System.Drawing.Size(580, 340);
            this.Name = "EncodingTestForm";
            this.ShowInTaskbar = false;
            this.Text = "DetectInputCodepage Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox streamReader;
        private System.Windows.Forms.TextBox detected;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}