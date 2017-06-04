using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования печатного труда.
    /// </summary>
    public partial class EditPublicationForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор печатного труда.
        /// </summary>
        private readonly int _publicationId;

        #region Сервисы

        private readonly IPublicationService _publicationService;

        #endregion

        public EditPublicationForm(
            int publicationId,
            int planId,
            IPublicationService publicationService)
        {
            InitializeComponent();

            _publicationId = publicationId;
            _planId = planId;

            _publicationService = publicationService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _publicationId > 0;

                if (isEdit)
                {
                    var entity = _publicationService.GetPublicationById(_publicationId);

                    tbName.Text = entity.Name;
                    tbCoauthors.Text = entity.Coauthors;
                    numVolume.Value = entity.Volume ?? 0;
                    cbIsPublished.Checked = entity.IsPublished;
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
                var entity = new Publication
                {
                    PublicationId = _publicationId,
                    PlanId = _planId,
                    Name = tbName.Text,
                    Coauthors = tbCoauthors.Text,
                    Volume = numVolume.Value == 0 ? null : (int?)numVolume.Value,
                    IsPublished = cbIsPublished.Checked
                };
                _publicationService.SavePublication(entity);

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
