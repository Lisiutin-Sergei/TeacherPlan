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
    /// Репозиторий типа учебных работ преподавателя.
    /// </summary>
    public class EducationalWorkTypeRepository : IEducationalWorkTypeRepository
    {
        private DataContext _dataContext;

        /// <summary>
        /// Конструктор репозитория типа учебных работ преподавателя.
        /// </summary>
        /// <param name="dataContext">Контекст данных (подключение к базе и транзакция).</param>
        public EducationalWorkTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        /// <summary>
        /// Получить список всех экземпляров сущности.
        /// </summary>
        /// <returns>Список всех экземпляров сущности.</returns>
        public IEnumerable<EducationalWorkType> GetAll()
        {
            return _dataContext.Connection.GetAll<EducationalWorkType>().ToList();
        }

        /// <summary>
        /// Получить список экземпляров сущности по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список экземпляров сущности.</returns>
        public IEnumerable<EducationalWorkType> GetByFilter(Func<EducationalWorkType, bool> filter)
        {
            var data = _dataContext.Connection.GetAll<EducationalWorkType>();
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
        public EducationalWorkType GetByID(int id)
        {
            return _dataContext.Connection.Get<EducationalWorkType>(id);
        }
    }
}
