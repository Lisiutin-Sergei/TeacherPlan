using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Repository
{
    /// <summary>
    /// Интерфейс репозитория учебных работ преподавателя.
    /// </summary>
    public interface IEducationalWorkRepository
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        EducationalWork GetByID(int id);

        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        IEnumerable<EducationalWork> GetAll();

        /// <summary>
        /// Получить список сущностей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список сущностей.</returns>
        IEnumerable<EducationalWork> GetByFilter(Func<EducationalWork, bool> filter);

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        int Insert(EducationalWork entity);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        void Update(EducationalWork entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        void Delete(EducationalWork entity);
    }
}
