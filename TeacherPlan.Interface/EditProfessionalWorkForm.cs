using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования профориентационной работы.
    /// </summary>
    public partial class EditProfessionalWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор профориентационной работы со студентами.
        /// </summary>
        private readonly int _professionalWorkId;

        #region Сервисы

        private readonly IProfessionalWorkService _professionalWorkService;

        #endregion

        public EditProfessionalWorkForm(
            int professionalWorkId,
            int planId,
            IProfessionalWorkService professionalWorkService)
        {
            InitializeComponent();

            _professionalWorkId = professionalWorkId;
            _planId = planId;

            _professionalWorkService = professionalWorkService;
        }
        
        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _professionalWorkId > 0;

                if (isEdit)
                {
                    var entity = _professionalWorkService.GetProfessionalWorkById(_professionalWorkId);

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
                var entity = new ProfessionalWork
                {
                    ProfessionalWorkId = _professionalWorkId,
                    PlanId = _planId,
                    Name = tbName.Text,
                    Date = tbDate.Text,
                    Execution = tbExecution.Text,
                    Hours = numHours.Value == 0 ? null : (double?)numHours.Value
                };
                _professionalWorkService.SaveProfessionalWork(entity);

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
