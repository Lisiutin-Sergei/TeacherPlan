namespace TeacherPlan.Interface
{
    partial class EditContractWorkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditContractWorkForm));
            this.numVolume = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDuty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbExecution = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbComment = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).BeginInit();
            this.SuspendLayout();
            // 
            // numVolume
            // 
            this.numVolume.Location = new System.Drawing.Point(166, 65);
            this.numVolume.Name = "numVolume";
            this.numVolume.Size = new System.Drawing.Size(120, 20);
            this.numVolume.TabIndex = 111;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 110;
            this.label1.Text = "Должностные обязанности";
            // 
            // tbDuty
            // 
            this.tbDuty.Location = new System.Drawing.Point(166, 89);
            this.tbDuty.Name = "tbDuty";
            this.tbDuty.Size = new System.Drawing.Size(279, 20);
            this.tbDuty.TabIndex = 109;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "Вид работы";
            // 
            // tbType
            // 
            this.tbType.Location = new System.Drawing.Point(166, 38);
            this.tbType.Name = "tbType";
            this.tbType.Size = new System.Drawing.Size(279, 20);
            this.tbType.TabIndex = 107;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 106;
            this.label4.Text = "Финансирование";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 105;
            this.label2.Text = "Наименование темы";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(451, 57);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 103;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(451, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 104;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(166, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(279, 20);
            this.tbName.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 113;
            this.label3.Text = "Отметки о выполнении";
            // 
            // tbExecution
            // 
            this.tbExecution.Location = new System.Drawing.Point(166, 115);
            this.tbExecution.Name = "tbExecution";
            this.tbExecution.Size = new System.Drawing.Size(279, 20);
            this.tbExecution.TabIndex = 112;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 115;
            this.label6.Text = "Примечания";
            // 
            // tbComment
            // 
            this.tbComment.Location = new System.Drawing.Point(166, 141);
            this.tbComment.Name = "tbComment";
            this.tbComment.Size = new System.Drawing.Size(279, 20);
            this.tbComment.TabIndex = 114;
            // 
            // EditContractWorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 174);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbComment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbExecution);
            this.Controls.Add(this.numVolume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbDuty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbType);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbName);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(515, 213);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(515, 213);
            this.Name = "EditContractWorkForm";
            this.Text = "Хоздоговорная работа";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numVolume)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDuty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbExecution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbComment;
    }
}