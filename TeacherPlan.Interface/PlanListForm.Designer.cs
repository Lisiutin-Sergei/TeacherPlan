namespace TeacherPlan.Interface
{
    partial class PlanListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlanListForm));
            this.dgvPlans = new System.Windows.Forms.DataGridView();
            this.PlanId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrintPlan = new System.Windows.Forms.Button();
            this.btnDeletePlan = new System.Windows.Forms.Button();
            this.btnEditPlan = new System.Windows.Forms.Button();
            this.btnAddPlan = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlans)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPlans
            // 
            this.dgvPlans.AllowUserToAddRows = false;
            this.dgvPlans.AllowUserToDeleteRows = false;
            this.dgvPlans.AllowUserToResizeColumns = false;
            this.dgvPlans.AllowUserToResizeRows = false;
            this.dgvPlans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlanId,
            this.PlanName,
            this.PlanDate});
            this.dgvPlans.Location = new System.Drawing.Point(12, 12);
            this.dgvPlans.Name = "dgvPlans";
            this.dgvPlans.ReadOnly = true;
            this.dgvPlans.RowHeadersVisible = false;
            this.dgvPlans.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPlans.Size = new System.Drawing.Size(504, 337);
            this.dgvPlans.TabIndex = 3;
            // 
            // PlanId
            // 
            this.PlanId.HeaderText = "Id";
            this.PlanId.Name = "PlanId";
            this.PlanId.ReadOnly = true;
            this.PlanId.Visible = false;
            // 
            // PlanName
            // 
            this.PlanName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PlanName.HeaderText = "Название";
            this.PlanName.Name = "PlanName";
            this.PlanName.ReadOnly = true;
            // 
            // PlanDate
            // 
            this.PlanDate.HeaderText = "Дата";
            this.PlanDate.Name = "PlanDate";
            this.PlanDate.ReadOnly = true;
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(532, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 4;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // btnPrintPlan
            // 
            this.btnPrintPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintPlan.Image")));
            this.btnPrintPlan.Location = new System.Drawing.Point(532, 309);
            this.btnPrintPlan.Name = "btnPrintPlan";
            this.btnPrintPlan.Size = new System.Drawing.Size(40, 40);
            this.btnPrintPlan.TabIndex = 5;
            this.btnPrintPlan.UseVisualStyleBackColor = true;
            this.btnPrintPlan.Click += new System.EventHandler(this.Btn_PrintPlan_Click);
            // 
            // btnDeletePlan
            // 
            this.btnDeletePlan.Image = ((System.Drawing.Image)(resources.GetObject("btnDeletePlan.Image")));
            this.btnDeletePlan.Location = new System.Drawing.Point(532, 225);
            this.btnDeletePlan.Name = "btnDeletePlan";
            this.btnDeletePlan.Size = new System.Drawing.Size(40, 40);
            this.btnDeletePlan.TabIndex = 6;
            this.btnDeletePlan.UseVisualStyleBackColor = true;
            this.btnDeletePlan.Click += new System.EventHandler(this.Btn_DeletePlan_Click);
            // 
            // btnEditPlan
            // 
            this.btnEditPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnEditPlan.Image")));
            this.btnEditPlan.Location = new System.Drawing.Point(532, 179);
            this.btnEditPlan.Name = "btnEditPlan";
            this.btnEditPlan.Size = new System.Drawing.Size(40, 40);
            this.btnEditPlan.TabIndex = 7;
            this.btnEditPlan.UseVisualStyleBackColor = true;
            this.btnEditPlan.Click += new System.EventHandler(this.Btn_EditPlan_Click);
            // 
            // btnAddPlan
            // 
            this.btnAddPlan.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPlan.Image")));
            this.btnAddPlan.Location = new System.Drawing.Point(532, 133);
            this.btnAddPlan.Name = "btnAddPlan";
            this.btnAddPlan.Size = new System.Drawing.Size(40, 40);
            this.btnAddPlan.TabIndex = 8;
            this.btnAddPlan.UseVisualStyleBackColor = true;
            this.btnAddPlan.Click += new System.EventHandler(this.Btn_AddPlan_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Image = ((System.Drawing.Image)(resources.GetObject("btnEditUser.Image")));
            this.btnEditUser.Location = new System.Drawing.Point(532, 58);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(40, 40);
            this.btnEditUser.TabIndex = 4;
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.Btn_EditUser_Click);
            // 
            // PlanListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.dgvPlans);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrintPlan);
            this.Controls.Add(this.btnDeletePlan);
            this.Controls.Add(this.btnEditPlan);
            this.Controls.Add(this.btnAddPlan);
            this.Name = "PlanListForm";
            this.Text = "Список планов";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPlans;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrintPlan;
        private System.Windows.Forms.Button btnDeletePlan;
        private System.Windows.Forms.Button btnEditPlan;
        private System.Windows.Forms.Button btnAddPlan;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanId;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanDate;
    }
}