using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Госбюджетная работа".
    /// </summary>
    public interface IStateBudgetWorkService
    {
        /// <summary>
        /// Получить госбюджетную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="stateBudgetWorkId">Идентификатор госбюджетной работы преподавателя.</param>
        /// <returns>Госбюджетная работа преподавателя.</returns>
        StateBudgetWork GetStateBudgetWorkById(int stateBudgetWorkId);

        /// <summary>
        /// Получить госбюджетные работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Госбюджетные работы по плану преподавателя.</returns>
        List<StateBudgetWork> LoadStateBudgetWorksByPlan(int planId);

        /// <summary>
        /// Сохранить госбюджетную работу.
        /// </summary>
        /// <param name="stateBudgetWork">Госбюджетная работа.</param>
        /// <returns>Идентификатор госбюджетной работы.</returns>
        int SaveStateBudgetWork(StateBudgetWork stateBudgetWork);

        /// <summary>
        /// Удалить госбюджетную работу.
        /// </summary>
        /// <param name="stateBudgetWorkId">Идентификатор госбюджетной работы.</param>
        void DeleteStateBudgetWork(int stateBudgetWorkId);
    }
}
