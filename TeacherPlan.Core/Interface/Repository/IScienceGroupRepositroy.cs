using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Repository
{
    /// <summary>
    /// Интерфейс репозитория Научных кружков.
    /// </summary>
    public interface IScienceGroupRepository
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        ScienceGroup GetByID(int id);

        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        IEnumerable<ScienceGroup> GetAll();

        /// <summary>
        /// Получить список сущностей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список сущностей.</returns>
        IEnumerable<ScienceGroup> GetByFilter(Func<ScienceGroup, bool> filter);

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        int Insert(ScienceGroup entity);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        void Update(ScienceGroup entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        void Delete(ScienceGroup entity);
    }
}
