namespace TeacherPlan.Interface
{
    partial class EditScienceGroupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditScienceGroupForm));
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbPlace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numPublications = new System.Windows.Forms.NumericUpDown();
            this.numConferences = new System.Windows.Forms.NumericUpDown();
            this.numDiplomas = new System.Windows.Forms.NumericUpDown();
            this.numStudents = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numPublications)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConferences)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiplomas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Место кружк. работы";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(418, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 34;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(418, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 35;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbPlace
            // 
            this.tbPlace.Location = new System.Drawing.Point(140, 11);
            this.tbPlace.Name = "tbPlace";
            this.tbPlace.Size = new System.Drawing.Size(270, 20);
            this.tbPlace.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Кол-во студен-тов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Наименование кружка";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(140, 63);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(270, 20);
            this.tbName.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "Кол-во публика-ций";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Участие в научн.конф";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Дипломы конкурсов";
            // 
            // numPublications
            // 
            this.numPublications.Location = new System.Drawing.Point(140, 89);
            this.numPublications.Name = "numPublications";
            this.numPublications.Size = new System.Drawing.Size(120, 20);
            this.numPublications.TabIndex = 45;
            // 
            // numConferences
            // 
            this.numConferences.Location = new System.Drawing.Point(140, 115);
            this.numConferences.Name = "numConferences";
            this.numConferences.Size = new System.Drawing.Size(120, 20);
            this.numConferences.TabIndex = 46;
            // 
            // numDiplomas
            // 
            this.numDiplomas.Location = new System.Drawing.Point(140, 141);
            this.numDiplomas.Name = "numDiplomas";
            this.numDiplomas.Size = new System.Drawing.Size(120, 20);
            this.numDiplomas.TabIndex = 47;
            // 
            // numStudents
            // 
            this.numStudents.Location = new System.Drawing.Point(140, 37);
            this.numStudents.Name = "numStudents";
            this.numStudents.Size = new System.Drawing.Size(120, 20);
            this.numStudents.TabIndex = 48;
            // 
            // EditScienceGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 169);
            this.Controls.Add(this.numStudents);
            this.Controls.Add(this.numDiplomas);
            this.Controls.Add(this.numConferences);
            this.Controls.Add(this.numPublications);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbPlace);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(486, 208);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(486, 208);
            this.Name = "EditScienceGroupForm";
            this.Text = "Научный кружок";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPublications)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numConferences)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiplomas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbPlace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numPublications;
        private System.Windows.Forms.NumericUpDown numConferences;
        private System.Windows.Forms.NumericUpDown numDiplomas;
        private System.Windows.Forms.NumericUpDown numStudents;
    }
}