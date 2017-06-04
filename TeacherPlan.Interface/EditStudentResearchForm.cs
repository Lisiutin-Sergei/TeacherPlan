using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования руководства научной исследовательской работой студентов.
    /// </summary>
    public partial class EditStudentResearchForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор руководства научной исследовательской работой студентов.
        /// </summary>
        private readonly int _studentResearchId;

        #region Сервисы

        private readonly IStudentResearchService _studentResearchService;

        #endregion

        public EditStudentResearchForm(
            int studentResearchId,
            int planId,
            IStudentResearchService studentResearchService)
        {
            InitializeComponent();

            _studentResearchId = studentResearchId;
            _planId = planId;

            _studentResearchService = studentResearchService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _studentResearchId > 0;

                if (isEdit)
                {
                    var entity = _studentResearchService.GetStudentResearchById(_studentResearchId);

                    tbStudentName.Text = entity.StudentName;
                    tbStudentGroup.Text = entity.StudentGroup;
                    tbResearch.Text = entity.Research;
                    tbOopCode.Text = entity.OopCode;
                    tbExecution.Text = entity.Execution;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить учебник.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = new StudentResearch
                {
                    StudentResearchId = _studentResearchId,
                    PlanId = _planId,
                    StudentName = tbStudentName.Text,
                    StudentGroup = tbStudentGroup.Text,
                    Research = tbResearch.Text,
                    OopCode = tbOopCode.Text,
                    Execution = tbExecution.Text
                };
                _studentResearchService.SaveStudentResearch(entity);

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
