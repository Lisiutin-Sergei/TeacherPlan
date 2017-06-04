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
    /// Форма редактирования госбюджетной работы.
    /// </summary>
    public partial class EditStateBudgetWorkForm : Form
    {
        /// <summary>
        /// Идентификатор плана пользователя.
        /// </summary>
        private readonly int _planId;

        /// <summary>
        /// Идентификатор госбюджетной работы.
        /// </summary>
        private readonly int _stateBudgetWorkId;

        #region Сервисы
        
        private readonly IStateBudgetWorkService _stateBudgetWorkService;

        #endregion

        public EditStateBudgetWorkForm(
            int stateBudgetWorkId,
            int planId,
            IStateBudgetWorkService stateBudgetWorkService)
        {
            InitializeComponent();

            _stateBudgetWorkId = stateBudgetWorkId;
            _planId = planId;

            _stateBudgetWorkService = stateBudgetWorkService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _stateBudgetWorkId > 0;

                if (isEdit)
                {
                    var stateBudgetWork = _stateBudgetWorkService.GetStateBudgetWorkById(_stateBudgetWorkId);

                    tbStateBudgetlWork.Text = stateBudgetWork.Name;
                    tbExecution.Text = stateBudgetWork.Execution;
                    numFirstFact.Value = (decimal?)stateBudgetWork.FirstSemesterFact ?? 0;
                    numFirstPlan.Value = (decimal?)stateBudgetWork.FirstSemesterPlan ?? 0;
                    numSecondFact.Value = (decimal?)stateBudgetWork.SecondSemesterFact ?? 0;
                    numSecondPlan.Value = (decimal?)stateBudgetWork.SecondSemesterPlan ?? 0;
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
                var entity = new StateBudgetWork
                {
                    StateBudgetWorkId = _stateBudgetWorkId,
                    Name = tbStateBudgetlWork.Text,
                    Execution = tbExecution.Text,
                    PlanId = _planId,
                    FirstSemesterFact = numFirstFact.Value == 0 ? null : (double?)numFirstFact.Value,
                    FirstSemesterPlan = numFirstPlan.Value == 0 ? null : (double?)numFirstPlan.Value,
                    SecondSemesterFact = numSecondFact.Value == 0 ? null : (double?)numSecondFact.Value,
                    SecondSemesterPlan = numSecondPlan.Value == 0 ? null : (double?)numSecondPlan.Value
                };
                _stateBudgetWorkService.SaveStateBudgetWork(entity);

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
