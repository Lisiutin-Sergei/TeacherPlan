using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
	/// <summary>
	/// Форма редактирования сущности "Повышение квалификации".
	/// </summary>
	public partial class EditQualificationWorkForm : Form
	{

		/// <summary>
		/// Идентификатор плана пользователя.
		/// </summary>
		private readonly int _planId;

		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		private readonly int _qualificationWorkId;

		#region Сервисы

		private readonly IQualificationWorkService _qualificationWorkService;

		#endregion

		public EditQualificationWorkForm(
			int qualificationWorkId,
			int planId,
			IQualificationWorkService qualificationWorkService)
		{
			InitializeComponent();

			_qualificationWorkId = qualificationWorkId;
			_planId = planId;

			_qualificationWorkService = qualificationWorkService;
		}

		/// <summary>
		/// При загрузке формы.
		/// </summary>
		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				var isEdit = _qualificationWorkId > 0;

				if (isEdit)
				{
					var entity = _qualificationWorkService.GetQualificationWorkById(_qualificationWorkId);

					tbCourseName.Text = entity.CourseName;
					tbCourseType.Text = entity.CourseType;
					numCourseVolume.Value = (decimal?)entity.CourseVolume ?? 0;
					tbPlace.Text = entity.Place;
					tbDate.Text = entity.Date;
					tbExecution.Text = entity.Execution;
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
				var entity = new QualificationWork
				{
					QualificationWorkId = _qualificationWorkId,
					PlanId = _planId,
					CourseName = tbCourseName.Text,
					CourseType = tbCourseType.Text,
					CourseVolume = numCourseVolume.Value == 0 ? null : (double?)numCourseVolume.Value,
					Place = tbPlace.Text,
					Date = tbDate.Text,
					Execution = tbExecution.Text,
					Hours = null
				};
				_qualificationWorkService.SaveQualificationWork(entity);

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
