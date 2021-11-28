namespace OC
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.act1 = new System.Windows.Forms.Button();
            this.act2 = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.Label();
            this.check = new System.Windows.Forms.Button();
            this.actF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // act1
            // 
            this.act1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.act1.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.act1.Location = new System.Drawing.Point(0, 12);
            this.act1.Name = "act1";
            this.act1.Size = new System.Drawing.Size(448, 52);
            this.act1.TabIndex = 0;
            this.act1.Text = "Step1";
            this.act1.UseVisualStyleBackColor = true;
            this.act1.Click += new System.EventHandler(this.act1_Click);
            // 
            // act2
            // 
            this.act2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.act2.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.act2.Location = new System.Drawing.Point(0, 128);
            this.act2.Name = "act2";
            this.act2.Size = new System.Drawing.Size(448, 58);
            this.act2.TabIndex = 1;
            this.act2.Text = "Step2";
            this.act2.UseVisualStyleBackColor = true;
            this.act2.Click += new System.EventHandler(this.act2_Click);
            // 
            // progress
            // 
            this.progress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progress.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.progress.Location = new System.Drawing.Point(0, 269);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(448, 52);
            this.progress.TabIndex = 2;
            this.progress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // check
            // 
            this.check.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.check.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.check.Location = new System.Drawing.Point(0, 70);
            this.check.Name = "check";
            this.check.Size = new System.Drawing.Size(448, 52);
            this.check.TabIndex = 3;
            this.check.Text = "Check";
            this.check.UseVisualStyleBackColor = true;
            this.check.Click += new System.EventHandler(this.check_Click);
            // 
            // actF
            // 
            this.actF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.actF.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.actF.Location = new System.Drawing.Point(0, 192);
            this.actF.Name = "actF";
            this.actF.Size = new System.Drawing.Size(448, 58);
            this.actF.TabIndex = 4;
            this.actF.Text = "Check DB";
            this.actF.UseVisualStyleBackColor = true;
            this.actF.Click += new System.EventHandler(this.actF_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 330);
            this.Controls.Add(this.actF);
            this.Controls.Add(this.check);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.act2);
            this.Controls.Add(this.act1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "OC-20160914";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button act1;
        private System.Windows.Forms.Button act2;
        private System.Windows.Forms.Label progress;
        private System.Windows.Forms.Button check;
        private System.Windows.Forms.Button actF;
    }
}

