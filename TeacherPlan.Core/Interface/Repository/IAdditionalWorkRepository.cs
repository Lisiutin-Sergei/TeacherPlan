using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Repository
{
	/// <summary>
	/// Интерфейс репозитория сущности "Дополнительная образовательная деятельность".
	/// </summary>
	public interface IAdditionalWorkRepository
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		AdditionalWork GetByID(int id);

        /// <summary>
        /// Получить список сущностей.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        IEnumerable<AdditionalWork> GetAll();

        /// <summary>
        /// Получить список сущностей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список сущностей.</returns>
        IEnumerable<AdditionalWork> GetByFilter(Func<AdditionalWork, bool> filter);

        /// <summary>
        /// Создать новую сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        int Insert(AdditionalWork entity);

        /// <summary>
        /// Обновить сущность.
        /// </summary>
        /// <param name="entity">Сущность для сохранения.</param>
        void Update(AdditionalWork entity);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        void Delete(AdditionalWork entity);
    }
}
