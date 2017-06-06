namespace TeacherPlan.Interface
{
	partial class EditDissertationWorkForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditDissertationWorkForm));
			this.numHours = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.tbExecution = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tbDate = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.tbName = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numHours)).BeginInit();
			this.SuspendLayout();
			// 
			// numHours
			// 
			this.numHours.Location = new System.Drawing.Point(142, 109);
			this.numHours.Name = "numHours";
			this.numHours.Size = new System.Drawing.Size(120, 20);
			this.numHours.TabIndex = 121;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 111);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 120;
			this.label1.Text = "Часы";
			// 
			// tbExecution
			// 
			this.tbExecution.Location = new System.Drawing.Point(142, 83);
			this.tbExecution.Name = "tbExecution";
			this.tbExecution.Size = new System.Drawing.Size(301, 20);
			this.tbExecution.TabIndex = 119;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(11, 60);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 118;
			this.label5.Text = "Сроки";
			// 
			// tbDate
			// 
			this.tbDate.Location = new System.Drawing.Point(142, 57);
			this.tbDate.Name = "tbDate";
			this.tbDate.Size = new System.Drawing.Size(301, 20);
			this.tbDate.TabIndex = 117;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 85);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(125, 13);
			this.label4.TabIndex = 116;
			this.label4.Text = "Отметки о выполнении";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 13);
			this.label2.TabIndex = 115;
			this.label2.Text = "Виды работ";
			// 
			// btnSave
			// 
			this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
			this.btnSave.Location = new System.Drawing.Point(449, 57);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(40, 40);
			this.btnSave.TabIndex = 113;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.Btn_Save_Click);
			// 
			// btnExit
			// 
			this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
			this.btnExit.Location = new System.Drawing.Point(449, 11);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(40, 40);
			this.btnExit.TabIndex = 114;
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(142, 12);
			this.tbName.Multiline = true;
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(301, 39);
			this.tbName.TabIndex = 112;
			// 
			// EditDissertationWorkForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 141);
			this.Controls.Add(this.numHours);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tbExecution);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbDate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.tbName);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(513, 180);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(513, 180);
			this.Name = "EditDissertationWorkForm";
			this.Text = "Работа над диссертацией";
			this.Load += new System.EventHandler(this.Form_Load);
			((System.ComponentModel.ISupportInitialize)(this.numHours)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numHours;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbExecution;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tbDate;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.TextBox tbName;
	}
}