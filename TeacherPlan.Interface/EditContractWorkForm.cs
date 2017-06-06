using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования хоздоговорной работы.
    /// </summary>
    public partial class EditContractWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор хоздоговорной работы.
        /// </summary>
        private readonly int _contractWorkId;

		#region Сервисы

		private readonly IContractWorkService _contractWorkService;

		#endregion

		public EditContractWorkForm(
            int contractWorkId,
            int planId,
			IContractWorkService contractWorkService)
        {
            InitializeComponent();

            _contractWorkId = contractWorkId;
            _planId = planId;

			_contractWorkService = contractWorkService;
		}
        
        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _contractWorkId > 0;

                if (isEdit)
                {
					var entity = _contractWorkService.GetContractWorkById(_contractWorkId);

					tbName.Text = entity.Name;
					tbType.Text = entity.Type;
					tbExecution.Text = entity.Execution;
					tbDuty.Text = entity.Duty;
					tbComment.Text = entity.Comment;
					numVolume.Value = (decimal?)entity.Volume ?? 0;
				}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
				var entity = new ContractWork
				{
					ContractWorkId = _contractWorkId,
					PlanId = _planId,
					Name = tbName.Text,
					Type = tbType.Text,
					Duty = tbDuty.Text,
					Comment = tbComment.Text,
					Execution = tbExecution.Text,
					Volume = numVolume.Value == 0 ? null : (double?)numVolume.Value
				};
				_contractWorkService.SaveContractWork(entity);

				DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрыть форму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
