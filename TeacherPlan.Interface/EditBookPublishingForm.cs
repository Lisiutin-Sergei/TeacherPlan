using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования издания учебника.
    /// </summary>
    public partial class EditBookPublishingForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор учебника.
        /// </summary>
        private readonly int _booksPublishingId;

        #region Сервисы
        
        private readonly IBooksPublishingService _booksPublishingService;

        #endregion

        public EditBookPublishingForm(
            int bookPublishingId,
            int planId,
            IBooksPublishingService booksPublishingService)
        {
            InitializeComponent();

            _booksPublishingId = bookPublishingId;
            _planId = planId;

            _booksPublishingService = booksPublishingService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _booksPublishingId > 0;

                if (isEdit)
                {
                    var bookPublishing = _booksPublishingService.GetBooksPublishingById(_booksPublishingId);

                    tbBookPublishing.Text = bookPublishing.Name;
                    tbOutput.Text = bookPublishing.Output;
                    tbCoauthors.Text = bookPublishing.Coauthors;
                    tbBookPurpose.Text = bookPublishing.Purpose;
                    numVolume.Value = bookPublishing.Volume ?? 0;
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
                var bookPublishing = new BooksPublishing
                {
                    BooksPublishingId = _booksPublishingId,
                    Name = tbBookPublishing.Text,
                    PlanId = _planId,
                    Output = tbOutput.Text,
                    Coauthors = tbCoauthors.Text,
                    Purpose = tbBookPurpose.Text,
                    Volume = numVolume.Value == 0 ? null : (int?)numVolume.Value
                };
                _booksPublishingService.SaveBooksPublishing(bookPublishing);

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
