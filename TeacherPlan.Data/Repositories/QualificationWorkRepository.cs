using Dommel;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherPlan.Core.Interface.Repository;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Data.UnitOfWork;

namespace TeacherPlan.Data.Repositories
{
	/// <summary>
	/// Репозиторий сущности "Повышение квалификации".
	/// </summary>
	public class QualificationWorkRepository : IQualificationWorkRepository
	{
        private DataContext _dataContext;

		/// <summary>
		/// Конструктор репозитория сущности "Повышение квалификации".
		/// </summary>
		/// <param name="dataContext">Контекст данных (подключение к базе и транзакция).</param>
		public QualificationWorkRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Вставить новый экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель нового экземпляра сущности.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        public int Insert(QualificationWork item)
        {
            return (int)_dataContext.Connection.Insert(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Обновить существующий экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель экземпляра сущности.</param>
        public void Update(QualificationWork item)
        {
            _dataContext.Connection.Update(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Удалить существующий экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель экземпляра сущности.</param>
        public void Delete(QualificationWork item)
        {
            _dataContext.Connection.Delete(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Получить список всех экземпляров сущности.
        /// </summary>
        /// <returns>Список всех экземпляров сущности.</returns>
        public IEnumerable<QualificationWork> GetAll()
        {
            return _dataContext.Connection.GetAll<QualificationWork>().ToList();
        }

        /// <summary>
        /// Получить список экземпляров сущности по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список экземпляров сущности.</returns>
        public IEnumerable<QualificationWork> GetByFilter(Func<QualificationWork, bool> filter)
        {
            var data = _dataContext.Connection.GetAll<QualificationWork>();
            if (filter != null)
            {
                data = data.Where(filter);
            }
            return data.ToList();
        }

        /// <summary>
        /// Получить экземпляр сущности по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор экземпляра сущности.</param>
        /// <returns>Найденный экземпляр сущности.</returns>
        public QualificationWork GetByID(int id)
        {
            return _dataContext.Connection.Get<QualificationWork>(id);
        }
    }
}
