using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования дополнительных образовательных деятельностей
    /// </summary>
    public partial class EditAdditionalWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        private readonly int _additionalWorkId;

        #region Сервисы

        //private readonly IAdditionalWorkService _additionalWorkService;

        #endregion

        public EditAdditionalWorkForm(
            int additionalWorkId,
            int planId,
            ITrainingWorkService additionalWorkService)
        {
            InitializeComponent();

            _additionalWorkId = additionalWorkId;
            _planId = planId;

            //_additionalWorkService = additionalWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _additionalWorkId > 0;

                //if (isEdit)
                //{
                //    var entity = _additionalWorkService.GetTrainingWorkById(_additionalWorkId);

                //    tbName.Text = entity.Name;
                //    tbStudents.Text = entity.Students;
                //    tbPlace.Text = entity.Place;
                //    tbProgram.Text = entity.Program;
                //    tbEducationType.Text = entity.EducationType;
                //    numVolume.Value = (decimal?)entity.Volume ?? 0;
                //}
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
                //var entity = new AdditionalWork
                //{
                //    AdditionalWorkId = _additionalWorkId,
                //    PlanId = _planId,
                //    Name = tbName.Text,
                //    Students = tbStudents.Text,
                //    Place = tbPlace.Text,
                //    Program = tbProgram.Text,
                //    EducationType = tbEducationType.Text,
                //    Volume = numVolume.Value == 0 ? null : (double?)numVolume.Value
                //};
                //_additionalWorkService.SaveTrainingWork(entity);

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
