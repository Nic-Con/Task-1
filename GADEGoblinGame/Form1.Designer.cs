namespace GADEGoblinGame
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.MinWid = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.MaxWid = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MaxHeight = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.MinHeight = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.NumEnemies = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MinWid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEnemies)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(-1, 1);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(383, 353);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // MinWid
            // 
            this.MinWid.Location = new System.Drawing.Point(477, 2);
            this.MinWid.Name = "MinWid";
            this.MinWid.Size = new System.Drawing.Size(120, 20);
            this.MinWid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max width";
            // 
            // MaxWid
            // 
            this.MaxWid.Location = new System.Drawing.Point(668, 2);
            this.MaxWid.Name = "MaxWid";
            this.MaxWid.Size = new System.Drawing.Size(120, 20);
            this.MaxWid.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(412, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Min Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(412, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Min Height";
            // 
            // MaxHeight
            // 
            this.MaxHeight.Location = new System.Drawing.Point(668, 28);
            this.MaxHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MaxHeight.Name = "MaxHeight";
            this.MaxHeight.Size = new System.Drawing.Size(120, 20);
            this.MaxHeight.TabIndex = 7;
            this.MaxHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(603, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max Height";
            // 
            // MinHeight
            // 
            this.MinHeight.Location = new System.Drawing.Point(477, 28);
            this.MinHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MinHeight.Name = "MinHeight";
            this.MinHeight.Size = new System.Drawing.Size(120, 20);
            this.MinHeight.TabIndex = 5;
            this.MinHeight.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(415, 80);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(373, 23);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(412, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Num enemies";
            // 
            // NumEnemies
            // 
            this.NumEnemies.Location = new System.Drawing.Point(489, 54);
            this.NumEnemies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumEnemies.Name = "NumEnemies";
            this.NumEnemies.Size = new System.Drawing.Size(299, 20);
            this.NumEnemies.TabIndex = 10;
            this.NumEnemies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.NumEnemies);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MaxHeight);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MinHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MaxWid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinWid);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MinWid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxWid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumEnemies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.NumericUpDown MinWid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MaxWid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown MaxHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown MinHeight;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown NumEnemies;
    }
}

