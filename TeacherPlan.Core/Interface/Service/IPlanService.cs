using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса планов преподавателей.
    /// </summary>
    public interface IPlanService
    {
        /// <summary>
        /// Получить план преподавателя по идентификатору.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>План преподавателя.</returns>
        Plan GetPlanById(int planId);

        /// <summary>
        /// Загрузить список планов преподавателя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список планов преподавателя.</returns>
        List<Plan> LoadUserPlans(int userId);

        /// <summary>
        /// Получить список планов преподавателя по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список планов преподавателя.</returns>
        List<Plan> LoadPlansByFilter(Func<Plan, bool> filter);

        /// <summary>
        /// Сохранить план.
        /// </summary>
        /// <param name="plan">План.</param>
        /// <returns>Идентификатор плана.</returns>
        int SavePlan(Plan plan);

        /// <summary>
        /// Удалить план.
        /// </summary>
        /// <param name="planId">Идентификатор плана.</param>
        void DeletePlan(int planId);
    }
}
