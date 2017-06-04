namespace TeacherPlan.Interface
{
    partial class EditBookPublishingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBookPublishingForm));
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbBookPublishing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.tbCoauthors = new System.Windows.Forms.TextBox();
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.tbBookPurpose = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Выходные данные";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Наименование издания,  вид";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(450, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 59;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(450, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 60;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbBookPublishing
            // 
            this.tbBookPublishing.Location = new System.Drawing.Point(168, 12);
            this.tbBookPublishing.Multiline = true;
            this.tbBookPublishing.Name = "tbBookPublishing";
            this.tbBookPublishing.Size = new System.Drawing.Size(276, 60);
            this.tbBookPublishing.TabIndex = 58;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 66;
            this.label1.Text = "Соавторы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 67;
            this.label4.Text = "Объем (п.л.)";
            // 
            // tbOutput
            // 
            this.tbOutput.Location = new System.Drawing.Point(168, 81);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(276, 20);
            this.tbOutput.TabIndex = 69;
            // 
            // tbCoauthors
            // 
            this.tbCoauthors.Location = new System.Drawing.Point(168, 132);
            this.tbCoauthors.Name = "tbCoauthors";
            this.tbCoauthors.Size = new System.Drawing.Size(276, 20);
            this.tbCoauthors.TabIndex = 71;
            // 
            // numVolume
            // 
            this.numVolume.Location = new System.Drawing.Point(168, 158);
            this.numVolume.Name = "numVolume";
            this.numVolume.Size = new System.Drawing.Size(120, 20);
            this.numVolume.TabIndex = 72;
            // 
            // xdfsdf
            // 
            this.tbBookPurpose.Location = new System.Drawing.Point(168, 106);
            this.tbBookPurpose.Name = "xdfsdf";
            this.tbBookPurpose.Size = new System.Drawing.Size(276, 20);
            this.tbBookPurpose.TabIndex = 73;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Назначение";
            // 
            // EditBookPublishingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 188);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbBookPurpose);
            this.Controls.Add(this.numVolume);
            this.Controls.Add(this.tbCoauthors);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbBookPublishing);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(518, 227);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(518, 227);
            this.Name = "EditBookPublishingForm";
            this.Text = "Издание учебника";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbBookPublishing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.TextBox tbCoauthors;
        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.TextBox tbBookPurpose;
        private System.Windows.Forms.Label label5;
    }
}