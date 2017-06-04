namespace TeacherPlan.Interface
{
    partial class EditEduMethodWorkForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditEduMethodWorkForm));
            this.numSecondFact = new System.Windows.Forms.NumericUpDown();
            this.numSecondPlan = new System.Windows.Forms.NumericUpDown();
            this.numFirstFact = new System.Windows.Forms.NumericUpDown();
            this.numFirstPlan = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.tbEducationalWork = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondFact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondPlan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstFact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPlan)).BeginInit();
            this.SuspendLayout();
            // 
            // numSecondFact
            // 
            this.numSecondFact.Location = new System.Drawing.Point(298, 105);
            this.numSecondFact.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSecondFact.Name = "numSecondFact";
            this.numSecondFact.Size = new System.Drawing.Size(112, 20);
            this.numSecondFact.TabIndex = 45;
            // 
            // numSecondPlan
            // 
            this.numSecondPlan.Location = new System.Drawing.Point(126, 105);
            this.numSecondPlan.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numSecondPlan.Name = "numSecondPlan";
            this.numSecondPlan.Size = new System.Drawing.Size(120, 20);
            this.numSecondPlan.TabIndex = 44;
            // 
            // numFirstFact
            // 
            this.numFirstFact.Location = new System.Drawing.Point(298, 79);
            this.numFirstFact.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFirstFact.Name = "numFirstFact";
            this.numFirstFact.Size = new System.Drawing.Size(112, 20);
            this.numFirstFact.TabIndex = 43;
            // 
            // numFirstPlan
            // 
            this.numFirstPlan.Location = new System.Drawing.Point(126, 79);
            this.numFirstPlan.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFirstPlan.Name = "numFirstPlan";
            this.numFirstPlan.Size = new System.Drawing.Size(120, 20);
            this.numFirstPlan.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "2 семестр план";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "факт";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "факт";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "1 семестр план";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Вид работы";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(416, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 34;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(416, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 35;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // tbEducationalWork
            // 
            this.tbEducationalWork.Location = new System.Drawing.Point(126, 12);
            this.tbEducationalWork.Multiline = true;
            this.tbEducationalWork.Name = "tbEducationalWork";
            this.tbEducationalWork.Size = new System.Drawing.Size(284, 60);
            this.tbEducationalWork.TabIndex = 33;
            // 
            // EditEduMethodWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 138);
            this.Controls.Add(this.numSecondFact);
            this.Controls.Add(this.numSecondPlan);
            this.Controls.Add(this.numFirstFact);
            this.Controls.Add(this.numFirstPlan);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.tbEducationalWork);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(487, 177);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(487, 177);
            this.Name = "EditEduMethodWork";
            this.Text = "Учебно-методическая работа";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSecondFact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSecondPlan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstFact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFirstPlan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numSecondFact;
        private System.Windows.Forms.NumericUpDown numSecondPlan;
        private System.Windows.Forms.NumericUpDown numFirstFact;
        private System.Windows.Forms.NumericUpDown numFirstPlan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox tbEducationalWork;
    }
}