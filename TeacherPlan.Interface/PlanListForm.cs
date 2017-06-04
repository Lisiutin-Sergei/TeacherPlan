using System;
using System.Linq;
using System.Windows.Forms;
using TeacherPlan.Configuration;
using TeacherPlan.Core.Interface.Service;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма для списка планов.
    /// </summary>
    public partial class PlanListForm : Form
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        private readonly int _userId;

        /// <summary>
        /// Сервис импорта данных.
        /// </summary>
        //private readonly IImportService _importService;

        #region Сервисы

        private readonly IPlanService _planService;
        private readonly IUserService _userService;

        #endregion

        public PlanListForm(
            int userId,
            IPlanService planService,
            IUserService userService
            //IImportService importService
            )
        {
            InitializeComponent();

            _userId = userId;

            _planService = planService;
            _userService = userService;
            //_importService = importService;
        }

        #region Обработка событий

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshUserName();
                RefreshPlansList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавление нового плана.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddPlan_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditPlanForm>(
                    new IoC.NinjectArgument("userId", _userId),
                    new IoC.NinjectArgument("planId", 0));
                form.FormClosed += RefreshPlansList;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshPlansList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменение плана.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlans.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Выберите план для изменения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var planId = int.Parse(dgvPlans.SelectedRows[0].Cells["PlanId"]?.Value?.ToString());

                var form = IoC.Instance.Resolve<EditPlanForm>(
                    new IoC.NinjectArgument("userId", _userId),
                    new IoC.NinjectArgument("planId", planId));
                form.FormClosed += RefreshPlansList;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RefreshPlansList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаление плана.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeletePlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlans.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Выберите план для удаления", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var planId = int.Parse(dgvPlans.SelectedRows[0].Cells["PlanId"].Value?.ToString());

                var confirmation = MessageBox.Show("Удалить план?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (confirmation == DialogResult.OK)
                {
                    _planService.DeletePlan(planId);
                    RefreshPlansList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Печать плана.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_PrintPlan_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlans.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Выберите план для печати", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var planId = int.Parse(dgvPlans.SelectedRows[0].Cells["PlanId"]?.Value?.ToString());
                //_importService.ImportPlan(_userId, planId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить данные пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditUser_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditUserForm>(
                    new IoC.NinjectArgument("userId", _userId));
                form.ShowDialog();
                form.FormClosed += RefreshUserName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Обновить список планов.
        /// </summary>
        private void RefreshPlansList(object sender = null, EventArgs e = null)
        {
            dgvPlans.Rows.Clear();

            var plans = _planService.LoadUserPlans(_userId);
            if (!(plans?.Any() ?? false))
            {
                return;
            }

            foreach (var plan in plans)
            {
                dgvPlans.Rows.Add(plan.PlanId, plan.Name, plan.PlanYear);
            }
        }

        /// <summary>
        /// Обновить имя пользователя.
        /// </summary>
        private void RefreshUserName(object sender = null, EventArgs e = null)
        {
            var user = _userService.GetUserById(_userId);
            Text = $"Список планов преподавателя {user.NameGenitive ?? user.Name ?? user.Login}";
        }

        /// <summary>
        /// Выйти.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
