using System;
using System.Windows.Forms;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования пользователя.
    /// </summary>
    public partial class EditUserForm : Form
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        private readonly int _userId;

        /// <summary>
        /// Сервис для работы с пользователем.
        /// </summary>
        private readonly IUserService _userService;

        public EditUserForm(
            int userId,
            IUserService userService)
        {
            InitializeComponent();

            _userId = userId;
            _userService = userService;
        }

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var user = _userService.GetUserById(_userId);

                tbLogin.Text = user.Login;
                tbUserName.Text = user.Name;
                tbUserNameGenitive.Text = user.NameGenitive;
                tbPosition.Text = user.Position;
                tbAcademicDegree.Text = user.AcademicDegree;
                tbAcademicRank.Text = user.AcademicRank;
                tbPositionType.Text = user.PositionType;
                tbPositionVolume.Text = user.PositionVolume;
                tbDepartment.Text = user.Department;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить профиль пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var user = new User
                {
                    UserId = _userId,
                    Position = tbPosition.Text,
                    Login = tbLogin.Text,
                    Name = tbUserName.Text,
                    NameGenitive = tbUserNameGenitive.Text,
                    AcademicDegree = tbAcademicDegree.Text,
                    AcademicRank = tbAcademicRank.Text,
                    PositionType = tbPositionType.Text,
                    PositionVolume = tbPositionVolume.Text,
                    Department = tbDepartment.Text
                };
                _userService.UpdateUser(user);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

    }
}
