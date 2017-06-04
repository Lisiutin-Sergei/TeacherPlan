using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования учебно-методической работы.
    /// </summary>
    public partial class EditEduMethodWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор учебной работы.
        /// </summary>
        private readonly int _eduMethodWorkId;

        #region Сервисы
        
        /// <summary>
        /// Сервис для работы с учебной работой.
        /// </summary>
        private readonly IEduMethodWorkService _eduMethodWorkService;

        #endregion

        public EditEduMethodWorkForm(
            int eduMethodWorkId,
            int planId,
            IEduMethodWorkService eduMethodWorkService)
        {
            InitializeComponent();

            _eduMethodWorkId = eduMethodWorkId;
            _planId = planId;
            
            _eduMethodWorkService = eduMethodWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _eduMethodWorkId > 0;

                if (isEdit)
                {
                    var educationalWork = _eduMethodWorkService.GetEduMethodWorkById(_eduMethodWorkId);

                    tbEducationalWork.Text = educationalWork.Name;
                    numFirstFact.Value = (decimal?)educationalWork.FirstSemesterFact ?? 0;
                    numFirstPlan.Value = (decimal?)educationalWork.FirstSemesterPlan ?? 0;
                    numSecondFact.Value = (decimal?)educationalWork.SecondSemesterFact ?? 0;
                    numSecondPlan.Value = (decimal?)educationalWork.SecondSemesterPlan ?? 0;
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
                var work = new EduMethodWork
                {
                    EduMethodWorkId = _eduMethodWorkId,
                    Name = tbEducationalWork.Text,
                    PlanId = _planId,
                    FirstSemesterFact = numFirstFact.Value == 0 ? null : (double?)numFirstFact.Value,
                    FirstSemesterPlan = numFirstPlan.Value == 0 ? null : (double?)numFirstPlan.Value,
                    SecondSemesterFact = numSecondFact.Value == 0 ? null : (double?)numSecondFact.Value,
                    SecondSemesterPlan = numSecondPlan.Value == 0 ? null : (double?)numSecondPlan.Value
                };
                _eduMethodWorkService.SaveEduMethodWork(work);

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
