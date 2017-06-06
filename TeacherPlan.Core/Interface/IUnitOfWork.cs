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

        /// <summary>
        /// Репозиторий сущности "Запланированные работы".
        /// </summary>
        IPlannedWorkRepository PlannedWorkRepository { get; }

		/// <summary>
		/// Репозиторий сущности "Работа над диссертацией".
		/// </summary>
		IDissertationWorkRepository DissertationWorkRepository { get; }

		/// <summary>
		/// Репозиторий сущности "Повышение квалификации".
		/// </summary>
		IQualificationWorkRepository QualificationWorkRepository { get; }

		/// <summary>
		/// Репозиторий сущности "Дополнительная образовательная деятельность".
		/// </summary>
		IAdditionalWorkRepository AdditionalWorkRepository { get; }

		/// <summary>
		/// Репозиторий сущности "Прочие виды работ".
		/// </summary>
		IOtherWorkRepository OtherWorkRepository { get; }

		/// <summary>
		/// Репозиторий сущности "Хоздоговорная работа".
		/// </summary>
		IContractWorkRepository ContractWorkRepository { get; }

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
