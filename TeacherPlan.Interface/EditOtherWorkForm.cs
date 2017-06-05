using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования прочих работ.
    /// </summary>
    public partial class EditOtherWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        private readonly int _otherWorkId;

        #region Сервисы

        //private readonly IOtherWorkService _otherWorkService;

        #endregion

        public EditOtherWorkForm(
            int otherWorkId,
            int planId,
            ITrainingWorkService otherWorkService)
        {
            InitializeComponent();

            _otherWorkId = otherWorkId;
            _planId = planId;

            //_otherWorkService = otherWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _otherWorkId > 0;

                if (isEdit)
                {
                    //    var entity = _otherWorkService.GetTrainingWorkById(_otherWorkId);

                    //    tbName.Text = entity.Name;
                    //    tbDate.Text = entity.Date;
                    //    tbExecution.Text = entity.Execution;
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
                //var entity = new OtherWork
                //{
                //    OtherWorkId = _otherWorkId,
                //    PlanId = _planId,
                //    Name = tbName.Text,
                //    Date = tbDate.Text,
                //    Execution = tbExecution.Text
                //};
                //_otherWorkService.SaveTrainingWork(entity);

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
