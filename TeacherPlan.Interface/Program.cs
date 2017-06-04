using Ninject;
using System;
using System.Windows.Forms;
using TeacherPlan.Configuration;

namespace TeacherPlan.Interface
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var kernel = new StandardKernel();
            IoC.Instance.Initialize(kernel);
            ConfigureInterfaceServices(kernel);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        /// <summary>
        /// Задать зависимости проекта .Interface.
        /// </summary>
        /// <param name="kernel">Ядро IoC.</param>
        static void ConfigureInterfaceServices(IKernel kernel)
        {
            //kernel.Bind<IImportService>().To<DocxImportService>().InTransientScope();
        }
    }
}
