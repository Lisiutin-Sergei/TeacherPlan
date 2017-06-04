namespace TeacherPlan.Interface
{
    partial class EditPublicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPublicationForm));
            this.tbCoauthors = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbIsPublished = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // tbCoauthors
            // 
            this.tbCoauthors.Location = new System.Drawing.Point(145, 60);
            this.tbCoauthors.Name = "tbCoauthors";
            this.tbCoauthors.Size = new System.Drawing.Size(301, 20);
            this.tbCoauthors.TabIndex = 97;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Соавторы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 91;
            this.label2.Text = "Наименование работы";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(452, 60);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 89;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(452, 14);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 90;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(145, 15);
            this.tbName.Multiline = true;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(301, 39);
            this.tbName.TabIndex = 88;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 100;
            this.label6.Text = " и издательства";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 102;
            this.label1.Text = "Объем в стр. (п.л.)";
            // 
            // numVolume
            // 
            this.numVolume.Location = new System.Drawing.Point(145, 86);
            this.numVolume.Name = "numVolume";
            this.numVolume.Size = new System.Drawing.Size(120, 20);
            this.numVolume.TabIndex = 103;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 104;
            this.label4.Text = "Вышло из печати";
            // 
            // cbIsPublished
            // 
            this.cbIsPublished.AutoSize = true;
            this.cbIsPublished.Location = new System.Drawing.Point(145, 113);
            this.cbIsPublished.Name = "cbIsPublished";
            this.cbIsPublished.Size = new System.Drawing.Size(15, 14);
            this.cbIsPublished.TabIndex = 105;
            this.cbIsPublished.UseVisualStyleBackColor = true;
            // 
            // EditPublicationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 145);
            this.Controls.Add(this.cbIsPublished);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numVolume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbCoauthors);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbName);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(523, 184);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(523, 184);
            this.Name = "EditPublicationForm";
            this.Text = "Печатный труд";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbCoauthors;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbIsPublished;
    }
}