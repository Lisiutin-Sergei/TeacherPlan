﻿using Dommel;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherPlan.Core.Interface.Repository;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Data.UnitOfWork;

namespace TeacherPlan.Data.Repositories
{
    /// <summary>
    /// Репозиторий Руководств научной исследовательской работой студентов.
    /// </summary>
    public class StudentResearchRepository : IStudentResearchRepository
    {
        private DataContext _dataContext;

        /// <summary>
        /// Конструктор репозитория планов преподавателя.
        /// </summary>
        /// <param name="dataContext">Контекст данных (подключение к базе и транзакция).</param>
        public StudentResearchRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Вставить новый экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель нового экземпляра сущности.</param>
        /// <returns>Идентификатор нового экземпляра сущности.</returns>
        public int Insert(StudentResearch item)
        {
            return (int)_dataContext.Connection.Insert(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Обновить существующий экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель экземпляра сущности.</param>
        public void Update(StudentResearch item)
        {
            _dataContext.Connection.Update(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Удалить существующий экземпляр сущности.
        /// </summary>
        /// <param name="item">Модель экземпляра сущности.</param>
        public void Delete(StudentResearch item)
        {
            _dataContext.Connection.Delete(item, _dataContext.Transaction);
        }

        /// <summary>
        /// Получить список всех экземпляров сущности.
        /// </summary>
        /// <returns>Список всех экземпляров сущности.</returns>
        public IEnumerable<StudentResearch> GetAll()
        {
            return _dataContext.Connection.GetAll<StudentResearch>().ToList();
        }

        /// <summary>
        /// Получить список экземпляров сущности по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список экземпляров сущности.</returns>
        public IEnumerable<StudentResearch> GetByFilter(Func<StudentResearch, bool> filter)
        {
            var data = _dataContext.Connection.GetAll<StudentResearch>();
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
        public StudentResearch GetByID(int id)
        {
            return _dataContext.Connection.Get<StudentResearch>(id);
        }
    }
}
