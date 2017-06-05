using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования запланированной работы.
    /// </summary>
    public partial class EditPlannedWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор запланированной работы.
        /// </summary>
        private readonly int _plannedWorkId;

        #region Сервисы

        /// <summary>
        /// Сервис для работы с учебной работой.
        /// </summary>
        private readonly IPlannedWorkService _plannedWorkService;

        #endregion

        public EditPlannedWorkForm(
            int plannedWorkId,
            int planId,
            IPlannedWorkService plannedWorkService)
        {
            InitializeComponent();

            _plannedWorkId = plannedWorkId;
            _planId = planId;

            _plannedWorkService = plannedWorkService;
        }
        
        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _plannedWorkId > 0;

                if (isEdit)
                {
                    var work = _plannedWorkService.GetPlannedWorkById(_plannedWorkId);

                    tbEducationalWork.Text = work.Name;
                    numFirstFact.Value = (decimal?)work.FirstSemesterFact ?? 0;
                    numFirstPlan.Value = (decimal?)work.FirstSemesterPlan ?? 0;
                    numSecondFact.Value = (decimal?)work.SecondSemesterFact ?? 0;
                    numSecondPlan.Value = (decimal?)work.SecondSemesterPlan ?? 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var work = new PlannedWork
                {
                    PlannedWorkId = _plannedWorkId,
                    Name = tbEducationalWork.Text,
                    PlanId = _planId,
                    FirstSemesterFact = numFirstFact.Value == 0 ? null : (double?)numFirstFact.Value,
                    FirstSemesterPlan = numFirstPlan.Value == 0 ? null : (double?)numFirstPlan.Value,
                    SecondSemesterFact = numSecondFact.Value == 0 ? null : (double?)numSecondFact.Value,
                    SecondSemesterPlan = numSecondPlan.Value == 0 ? null : (double?)numSecondPlan.Value
                };
                _plannedWorkService.SavePlannedWork(work);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
