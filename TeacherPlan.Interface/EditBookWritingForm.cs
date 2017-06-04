using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования учебника.
    /// </summary>
    public partial class EditBookWritingForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор учебника.
        /// </summary>
        private readonly int _bookWritingId;

        #region Сервисы
        
        private readonly IBookWritingService _bookWritingService;

        #endregion

        public EditBookWritingForm(
            int bookWritingId,
            int planId,
            IBookWritingService bookWritingService)
        {
            InitializeComponent();

            _bookWritingId = bookWritingId;
            _planId = planId;

            _bookWritingService = bookWritingService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _bookWritingId > 0;

                if (isEdit)
                {
                    var bookWriting = _bookWritingService.GetBooksWritingById(_bookWritingId);

                    tbBookWriting.Text = bookWriting.Name;
                    numFirstFact.Value = (decimal?)bookWriting.FirstSemesterFact ?? 0;
                    numFirstPlan.Value = (decimal?)bookWriting.FirstSemesterPlan ?? 0;
                    numSecondFact.Value = (decimal?)bookWriting.SecondSemesterFact ?? 0;
                    numSecondPlan.Value = (decimal?)bookWriting.SecondSemesterPlan ?? 0;
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
                var bookWriting = new BookWriting
                {
                    BookWritingId = _bookWritingId,
                    Name = tbBookWriting.Text,
                    PlanId = _planId,
                    FirstSemesterFact = numFirstFact.Value == 0 ? null : (double?)numFirstFact.Value,
                    FirstSemesterPlan = numFirstPlan.Value == 0 ? null : (double?)numFirstPlan.Value,
                    SecondSemesterFact = numSecondFact.Value == 0 ? null : (double?)numSecondFact.Value,
                    SecondSemesterPlan = numSecondPlan.Value == 0 ? null : (double?)numSecondPlan.Value
                };
                _bookWritingService.SaveBooksWriting(bookWriting);

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
