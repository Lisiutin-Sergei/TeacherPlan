using System;
using System.Windows.Forms;
using TeacherPlan.Configuration;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Utilities.Common;

namespace TeacherPlan.Interface
{
    /// <summary>
	/// Форма авторизации пользователя.
	/// </summary>
    public partial class AuthorizationForm : Form
    {
        private readonly IUserService _userServie;

        public AuthorizationForm(IUserService userServie)
        {
            _userServie = userServie;

            InitializeComponent();
        }

        /// <summary>
        /// Валидация регистрации пользователя.
        /// </summary>
        private void ValidateLogin()
        {
            Argument.NotNullOrWhiteSpace(tbLogin.Text, "Не заполнен логин пользователя.");
            Argument.NotNullOrWhiteSpace(tbPassword.Text, "Не заполнен пароль пользователя.");
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateLogin();

                var isPasswordCorrect = _userServie.VerifyUserPassword(tbLogin.Text, tbPassword.Text);
                if (!isPasswordCorrect)
                {
                    throw new Exception("Введенный пароль некорректен.");
                }

                // Переход на форму приложения
                var user = _userServie.GetUserByLogin(tbLogin.Text);
                if (user != null)
                {
                    Close();
                    IoC.Instance.Resolve<PlanListForm>(new IoC.NinjectArgument("userId", user.UserId)).ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Отменить авторизацию.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
