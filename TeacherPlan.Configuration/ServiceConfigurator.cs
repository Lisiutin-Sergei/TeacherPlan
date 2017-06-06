using Microsoft.Extensions.Configuration;
using Ninject;
using TeacherPlan.Core.Interface;
using TeacherPlan.Core.Interface.Repository;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Service;
using TeacherPlan.Data;
using TeacherPlan.Data.Repositories;
using TeacherPlan.Data.UnitOfWork;

namespace TeacherPlan.Configuration
{
    /// <summary>
    /// Конфигуратор сервисов для контейнера зависимостей.
    /// </summary>
    public class ServiceConfigurator
    {
        public static void ConfigureServices(IKernel kernel, IConfiguration configuration)
        {
            kernel.Bind<IConfiguration>().ToMethod((serviceProvider) => configuration).InSingletonScope();

            ConfigureCoreServices(kernel);
            ConfigureDataServices(kernel);
        }

        /// <summary>
        /// Задать зависимости проекта .Core.
        /// </summary>
        /// <param name="kernel">Ядро IoC.</param>
        private static void ConfigureCoreServices(IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>().InTransientScope();
            kernel.Bind<IPlanService>().To<PlanService>().InTransientScope();

            kernel.Bind<IEducationalWorkService>().To<EducationalWorkService>().InTransientScope();
            kernel.Bind<IEduMethodWorkService>().To<EduMethodWorkService>().InTransientScope();
            kernel.Bind<IBookWritingService>().To<BooksWritingService>().InTransientScope();
            kernel.Bind<IBooksPublishingService>().To<BooksPublishingService>().InTransientScope();
            kernel.Bind<IStateBudgetWorkService>().To<StateBudgetWorkService>().InTransientScope();
            kernel.Bind<IScienceGroupService>().To<ScienceGroupService>().InTransientScope();
            kernel.Bind<IStudentResearchService>().To<StudentResearchService>().InTransientScope();
            kernel.Bind<IPublicationService>().To<PublicationService>().InTransientScope();
            kernel.Bind<ITrainingWorkService>().To<TrainingWorkService>().InTransientScope();
            kernel.Bind<IProfessionalWorkService>().To<ProfessionalWorkService>().InTransientScope();
            kernel.Bind<IPlannedWorkService>().To<PlannedWorkService>().InTransientScope();
			kernel.Bind<IDissertationWorkService>().To<DissertationWorkService>().InTransientScope();
			kernel.Bind<IQualificationWorkService>().To<QualificationWorkService>().InTransientScope();
			kernel.Bind<IAdditionalWorkService>().To<AdditionalWorkService>().InTransientScope();
			kernel.Bind<IOtherWorkService>().To<OtherWorkService>().InTransientScope();
			kernel.Bind<IContractWorkService>().To<ContractWorkService>().InTransientScope();
		}

        /// <summary>
        /// Задать зависимости проекта .Data.
        /// </summary>
        /// <param name="kernel">Ядро IoC.</param>
        private static void ConfigureDataServices(IKernel kernel)
        {
            FluentMappingConfiguration.ConfigureMapping();
            kernel.Bind<IUnitOfWorkFactory>().To<UnitOfWorkFactory>().InTransientScope();

            kernel.Bind<IUserRepository>().To<UserRepository>().InTransientScope();
            kernel.Bind<IPlanRepository>().To<PlanRepository>().InTransientScope();
            kernel.Bind<IEducationalWorkTypeRepository>().To<EducationalWorkTypeRepository>().InTransientScope();
            kernel.Bind<IEducationalWorkRepository>().To<EducationalWorkRepository>().InTransientScope();
        }
    }
}
