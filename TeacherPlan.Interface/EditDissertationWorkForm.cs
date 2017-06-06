using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
	/// <summary>
	/// Форма редактирования работы над дисером.
	/// </summary>
	public partial class EditDissertationWorkForm : Form
	{
		/// <summary>
		/// Идентификатор плана пользователя.
		/// </summary>
		private readonly int _planId;

		/// <summary>
		/// Идентификатор работы над дисером.
		/// </summary>
		private readonly int _dissertationWorkId;

		#region Сервисы

		private readonly IDissertationWorkService _dissertationWorkService;

		#endregion

		public EditDissertationWorkForm(
			int dissertationWorkId,
			int planId,
			IDissertationWorkService dissertationWorkService)
		{
			InitializeComponent();

			_dissertationWorkId = dissertationWorkId;
			_planId = planId;

			_dissertationWorkService = dissertationWorkService;
		}

		/// <summary>
		/// При загрузке формы.
		/// </summary>
		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				var isEdit = _dissertationWorkId > 0;

				if (isEdit)
				{
					var entity = _dissertationWorkService.GetDissertationWorkById(_dissertationWorkId);

					tbName.Text = entity.Name;
					tbDate.Text = entity.Date;
					tbExecution.Text = entity.Execution;
					numHours.Value = (decimal?)entity.Hours ?? 0;
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
				var entity = new DissertationWork
				{
					DissertationWorkId = _dissertationWorkId,
					PlanId = _planId,
					Name = tbName.Text,
					Date = tbDate.Text,
					Execution = tbExecution.Text,
					Hours = numHours.Value == 0 ? null : (double?)numHours.Value
				};
				_dissertationWorkService.SaveDissertationWork(entity);

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
