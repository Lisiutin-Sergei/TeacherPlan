using System;
using TeacherPlan.Core.Interface.Repository;

namespace TeacherPlan.Core.Interface
{
    /// <summary>
	/// Единица работы (Паттерн Unit of Work).
	/// </summary>
	public interface IUnitOfWork : IDisposable
    {
        #region Repositories

        /// <summary>
        /// Репозиторий пользователей.
        /// </summary>
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Репозиторий плана преподавателя.
        /// </summary>
        IPlanRepository PlanRepository { get; }

        /// <summary>
        /// Репозиторий типа учебных работ преподавателя.
        /// </summary>
        IEducationalWorkTypeRepository EducationalWorkTypeRepository { get; }

        /// <summary>
        /// Репозиторий учебных работ преподавателя.
        /// </summary>
        IEducationalWorkRepository EducationalWorkRepository { get; }

        /// <summary>
        /// Репозиторий Учебно-методических работ преподавателя.
        /// </summary>
        IEduMethodWorkRepository EduMethodWorkRepository { get; }

        /// <summary>
        /// Репозиторий учебников преподавателя.
        /// </summary>
        IBooksWritingRepository BooksWritingRepository { get; }

        /// <summary>
        /// Репозиторий изданий учебников преподавателя.
        /// </summary>
        IBooksPublishingRepository BooksPublishingRepository { get; }

        /// <summary>
        /// Репозиторий Госбюджетных работ преподавателя.
        /// </summary>
        IStateBudgetWorkRepository StateBudgetWorkRepository { get; }

        /// <summary>
        /// Репозиторий Научных кружков преподавателя.
        /// </summary>
        IScienceGroupRepository ScienceGroupRepository { get; }

        /// <summary>
        /// Репозиторий Руководств научной исследовательской работой студентов преподавателя.
        /// </summary>
        IStudentResearchRepository StudentResearchRepository { get; }

        /// <summary>
        /// Репозиторий Печатных трудов за год преподавателя.
        /// </summary>
        IPublicationRepository PublicationRepository { get; }

        /// <summary>
        /// Репозиторий сущности "Воспитательная и внеаудиторная и работа со студентами".
        /// </summary>
        ITrainingWorkRepository TrainingWorkRepository { get; }

        /// <summary>
        /// Репозиторий сущности "Профориентационная работа".
        /// </summary>
        IProfessionalWorkRepository ProfessionalWorkRepository { get; }

        #endregion

        /// <summary>
        /// Открыть транзакцию.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Подтвердить транзакцию, если она открыта.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Откатить транзакцию, если она открыта.
        /// </summary>
        void RollbackTransaction();
    }
}
