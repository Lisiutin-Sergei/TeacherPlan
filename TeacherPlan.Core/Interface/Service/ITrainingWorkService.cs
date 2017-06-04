using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Воспитательная и внеаудиторная и работа со студентами".
    /// </summary>
    public interface ITrainingWorkService
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="trainingWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        TrainingWork GetTrainingWorkById(int trainingWorkId);

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        List<TrainingWork> LoadTrainingWorksByPlan(int planId);

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="trainingWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        int SaveTrainingWork(TrainingWork trainingWork);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="trainingWorkId">Идентификатор сущности.</param>
        void DeleteTrainingWork(int trainingWorkId);
    }
}
