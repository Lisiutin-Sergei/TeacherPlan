using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Запланированные работы".
    /// </summary>
    public interface IPlannedWorkService
    {
        // <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="plannedWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        PlannedWork GetPlannedWorkById(int plannedWorkId);

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        List<PlannedWork> LoadPlannedWorksByPlan(int planId);

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="plannedWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        int SavePlannedWork(PlannedWork plannedWork);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="plannedWorkId">Идентификатор сущности.</param>
        void DeletePlannedWork(int plannedWorkId);
    }
}
