using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Редактирование научного кружка.
    /// </summary>
    public partial class EditScienceGroupForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;
        
        /// <summary>
        /// Идентификатор кружка.
        /// </summary>
        private readonly int _scienceGroupId;

        #region Сервисы
        
        /// <summary>
        /// Сервис для работы с учебной работой.
        /// </summary>
        private readonly IScienceGroupService _scienceGroupService;

        #endregion

        public EditScienceGroupForm(
            int scienceGroupId,
            int planId,
            IScienceGroupService scienceGroupService)
        {
            InitializeComponent();

            _scienceGroupId = scienceGroupId;
            _planId = planId;

            _scienceGroupService = scienceGroupService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _scienceGroupId > 0;

                if (isEdit)
                {
                    var scienceGroup = _scienceGroupService.GetScienceGroupById(_scienceGroupId);

                    tbName.Text = scienceGroup.Name;
                    tbPlace.Text = scienceGroup.Place;
                    numStudents.Value = (int?)scienceGroup.StudentsCount ?? 0;
                    numPublications.Value = (int?)scienceGroup.PublicationsCount ?? 0;
                    numDiplomas.Value = (int?)scienceGroup.DiplomasCount ?? 0;
                    numConferences.Value = (int?)scienceGroup.ConferencesCount ?? 0;                    
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var entity = new ScienceGroup
                {
                    ScienceGroupId = _scienceGroupId,
                    PlanId = _planId,
                    Name = tbName.Text,
                    Place = tbPlace.Text,
                    StudentsCount = numStudents.Value == 0 ? null : (int?)numStudents.Value,
                    PublicationsCount = numPublications.Value == 0 ? null : (int?)numPublications.Value,
                    DiplomasCount = numDiplomas.Value == 0 ? null : (int?)numDiplomas.Value,
                    ConferencesCount = numConferences.Value == 0 ? null : (int?)numConferences.Value
                };
                _scienceGroupService.SaveScienceGroup(entity);

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
