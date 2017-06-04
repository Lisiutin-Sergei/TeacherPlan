namespace TeacherPlan.Interface
{
    partial class EditEducationalWorkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditEducationalWorkForm));
            this.tbEducationalWorkType = new System.Windows.Forms.TextBox();
            this.tbEducationalWork = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numFirstPlan = new System.Windows.Forms.NumericUpDown();
            this.numFirstFact = new System.Windows.Forms.NumericUpDown();
            this.numSecondPlan = new System.Windows.Forms.NumericUpDown();
            this.numSecondFact = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstFact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondFact)).BeginInit();
            this.SuspendLayout();
            // 
            // tbEducationalWorkType
            // 
            this.tbEducationalWorkType.Location = new System.Drawing.Point(127, 18);
            this.tbEducationalWorkType.Name = "tbEducationalWorkType";
            this.tbEducationalWorkType.ReadOnly = true;
            this.tbEducationalWorkType.Size = new System.Drawing.Size(284, 20);
            this.tbEducationalWorkType.TabIndex = 0;
            // 
            // tbEducationalWork
            // 
            this.tbEducationalWork.Location = new System.Drawing.Point(127, 44);
            this.tbEducationalWork.Multiline = true;
            this.tbEducationalWork.Name = "tbEducationalWork";
            this.tbEducationalWork.Size = new System.Drawing.Size(284, 60);
            this.tbEducationalWork.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(417, 64);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 20;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(417, 18);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 21;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Вид учебной работы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Учебная работа";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "1 семестр план";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(261, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "факт";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(261, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "факт";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "2 семестр план";
            // 
            // numFirstPlan
            // 
            this.numFirstPlan.Location = new System.Drawing.Point(127, 111);
            this.numFirstPlan.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFirstPlan.Name = "numFirstPlan";
            this.numFirstPlan.Size = new System.Drawing.Size(120, 20);
            this.numFirstPlan.TabIndex = 28;
            // 
            // numFirstFact
            // 
            this.numFirstFact.Location = new System.Drawing.Point(299, 111);
            this.numFirstFact.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFirstFact.Name = "numFirstFact";
            this.numFirstFact.Size = new System.Drawing.Size(112, 20);
            this.numFirstFact.TabIndex = 29;
            // 
            // numSecondPlan
            // 
            this.numSecondPlan.Location = new System.Drawing.Point(127, 137);
            this.numSecondPlan.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSecondPlan.Name = "numSecondPlan";
            this.numSecondPlan.Size = new System.Drawing.Size(120, 20);
            this.numSecondPlan.TabIndex = 30;
            // 
            // numSecondFact
            // 
            this.numSecondFact.Location = new System.Drawing.Point(299, 137);
            this.numSecondFact.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSecondFact.Name = "numSecondFact";
            this.numSecondFact.Size = new System.Drawing.Size(112, 20);
            this.numSecondFact.TabIndex = 31;
            // 
            // EditEducationalWorkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 169);
            this.Controls.Add(this.numSecondFact);
            this.Controls.Add(this.numSecondPlan);
            this.Controls.Add(this.numFirstFact);
            this.Controls.Add(this.numFirstPlan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbEducationalWork);
            this.Controls.Add(this.tbEducationalWorkType);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(485, 208);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(485, 208);
            this.Name = "EditEducationalWorkForm";
            this.Text = "Учебная работа";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstFact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondFact)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbEducationalWorkType;
        private System.Windows.Forms.TextBox tbEducationalWork;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numFirstPlan;
        private System.Windows.Forms.NumericUpDown numFirstFact;
        private System.Windows.Forms.NumericUpDown numSecondPlan;
        private System.Windows.Forms.NumericUpDown numSecondFact;
    }
}