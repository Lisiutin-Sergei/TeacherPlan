using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Repository
{
    /// <summary>
    /// Интерфейс репозитория планов преподавателя.
    /// </summary>
    public interface IPlanRepository
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        Plan GetByID(int id);

        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        IEnumerable<Plan> GetAll();

        /// <summary>
        /// Получить список сущностей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список сущностей.</returns>
        IEnumerable<Plan> GetByFilter(Func<Plan, bool> filter);

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        int Insert(Plan entity);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        void Update(Plan entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        void Delete(Plan entity);
    }
}
