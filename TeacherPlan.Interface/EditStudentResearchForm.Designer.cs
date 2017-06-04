namespace TeacherPlan.Interface
{
    partial class EditStudentResearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditStudentResearchForm));
            this.label5 = new System.Windows.Forms.Label();
            this.tbOopCode = new System.Windows.Forms.TextBox();
            this.tbResearch = new System.Windows.Forms.TextBox();
            this.tbStudentGroup = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbStudentName = new System.Windows.Forms.TextBox();
            this.tbExecution = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 86;
            this.label5.Text = "Код ООП";
            // 
            // tbOopCode
            // 
            this.tbOopCode.Location = new System.Drawing.Point(142, 64);
            this.tbOopCode.Name = "tbOopCode";
            this.tbOopCode.Size = new System.Drawing.Size(301, 20);
            this.tbOopCode.TabIndex = 85;
            // 
            // tbResearch
            // 
            this.tbResearch.Location = new System.Drawing.Point(142, 90);
            this.tbResearch.Name = "tbResearch";
            this.tbResearch.Size = new System.Drawing.Size(301, 20);
            this.tbResearch.TabIndex = 83;
            // 
            // tbStudentGroup
            // 
            this.tbStudentGroup.Location = new System.Drawing.Point(142, 38);
            this.tbStudentGroup.Name = "tbStudentGroup";
            this.tbStudentGroup.Size = new System.Drawing.Size(301, 20);
            this.tbStudentGroup.TabIndex = 82;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 81;
            this.label4.Text = "Отметки о выполнении";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Тема работы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 79;
            this.label3.Text = "№ ак. группы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Студент";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(449, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 76;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(449, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 77;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbStudentName
            // 
            this.tbStudentName.Location = new System.Drawing.Point(142, 12);
            this.tbStudentName.Name = "tbStudentName";
            this.tbStudentName.Size = new System.Drawing.Size(301, 20);
            this.tbStudentName.TabIndex = 75;
            // 
            // tbExecution
            // 
            this.tbExecution.Location = new System.Drawing.Point(142, 116);
            this.tbExecution.Name = "tbExecution";
            this.tbExecution.Size = new System.Drawing.Size(301, 20);
            this.tbExecution.TabIndex = 87;
            // 
            // EditStudentResearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 150);
            this.Controls.Add(this.tbExecution);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbOopCode);
            this.Controls.Add(this.tbResearch);
            this.Controls.Add(this.tbStudentGroup);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbStudentName);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(513, 189);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(513, 189);
            this.Name = "EditStudentResearchForm";
            this.Text = "Руководство научной исследовательской работой студентов";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbOopCode;
        private System.Windows.Forms.TextBox tbResearch;
        private System.Windows.Forms.TextBox tbStudentGroup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbStudentName;
        private System.Windows.Forms.TextBox tbExecution;
    }
}