using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования сущности "Воспитательная и внеаудиторная и работа со студентами".
    /// </summary>
    public partial class EditTrainingWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор воспитательной работы со студентами.
        /// </summary>
        private readonly int _trainingWorkId;

        #region Сервисы

        private readonly ITrainingWorkService _trainingWorkService;

        #endregion

        public EditTrainingWorkForm(
            int trainingWorkId,
            int planId,
            ITrainingWorkService trainingWorkService)
        {
            InitializeComponent();

            _trainingWorkId = trainingWorkId;
            _planId = planId;

            _trainingWorkService = trainingWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _trainingWorkId > 0;

                if (isEdit)
                {
                    var entity = _trainingWorkService.GetTrainingWorkById(_trainingWorkId);

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
                var entity = new TrainingWork
                {
                    TrainingWorkId = _trainingWorkId,
                    PlanId = _planId,
                    Name = tbName.Text,
                    Date = tbDate.Text,
                    Execution = tbExecution.Text,
                    Hours = numHours.Value == 0 ? null : (double?)numHours.Value
                };
                _trainingWorkService.SaveTrainingWork(entity);

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
