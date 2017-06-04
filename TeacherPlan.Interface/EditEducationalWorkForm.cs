using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования учебной работы.
    /// </summary>
    public partial class EditEducationalWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор вида учебной работы.
        /// </summary>
        private readonly int _educationalWorkTypeId;

        /// <summary>
        /// Идентификатор учебной работы.
        /// </summary>
        private readonly int _educationalWorkId;

        #region Сервисы

        /// <summary>
        /// Сервис для работы с планом.
        /// </summary>
        private readonly IPlanService _planService;

        /// <summary>
        /// Сервис для работы с учебной работой.
        /// </summary>
        private readonly IEducationalWorkService _educationalWorkService;

        #endregion

        public EditEducationalWorkForm(
            int educationalWorkTypeId,
            int educationalWorkId,
            int planId,
            IPlanService planService,
            IEducationalWorkService educationalWorkService)
        {
            InitializeComponent();

            _educationalWorkTypeId = educationalWorkTypeId;
            _educationalWorkId = educationalWorkId;
            _planId = planId;

            _planService = planService;
            _educationalWorkService = educationalWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _educationalWorkId > 0;

                if (isEdit)
                {
                    var educationalWork = _educationalWorkService.GetEducationalWorkById(_educationalWorkId);

                    tbEducationalWork.Text = educationalWork.Name;
                    numFirstFact.Value = (decimal?)educationalWork.FirstSemesterFact ?? 0;
                    numFirstPlan.Value = (decimal?)educationalWork.FirstSemesterPlan ?? 0;
                    numSecondFact.Value = (decimal?)educationalWork.SecondSemesterFact ?? 0;
                    numSecondPlan.Value = (decimal?)educationalWork.SecondSemesterPlan ?? 0;

                    RefreshWorkType(educationalWork.EducationalWorkTypeId);
                }
                else
                {
                    RefreshWorkType(_educationalWorkTypeId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var educationalWork = new EducationalWork
                {
                    EducationalWorkId = _educationalWorkId,
                    EducationalWorkTypeId = _educationalWorkTypeId,
                    Name = tbEducationalWork.Text,
                    PlanId = _planId,
                    FirstSemesterFact = numFirstFact.Value == 0 ? null : (double?)numFirstFact.Value,
                    FirstSemesterPlan = numFirstPlan.Value == 0 ? null : (double?)numFirstPlan.Value,
                    SecondSemesterFact = numSecondFact.Value == 0 ? null : (double?)numSecondFact.Value,
                    SecondSemesterPlan = numSecondPlan.Value == 0 ? null : (double?)numSecondPlan.Value
                };
                _educationalWorkService.SaveEducationalWork(educationalWork);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить вид учебной работы.
        /// </summary>
        /// <param name="workTypeId"></param>
        private void RefreshWorkType(int workTypeId)
        {
            var workType = _educationalWorkService.GetEducationalWorkTypeById(workTypeId);
            tbEducationalWorkType.Text = workType.Name;
        }

        /// <summary>
        /// Закрыть форму
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
