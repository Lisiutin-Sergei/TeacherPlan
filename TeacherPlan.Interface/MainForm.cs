using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherPlan.Configuration;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Первая форма приложения.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Login_Click(object sender, EventArgs e)
        {
            var form = IoC.Instance.Resolve<AuthorizationForm>();
            form.ShowDialog();
        }

        /// <summary>
        /// Регистрация нового пользователя.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Register_Click(object sender, EventArgs e)
        {
            var registrationForm = IoC.Instance.Resolve<RegistrationForm>();
            registrationForm.ShowDialog();
        }

        /// <summary>
        /// О проекте.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_About_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }
    }
}
