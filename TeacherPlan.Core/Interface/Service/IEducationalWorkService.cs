using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    public interface IEducationalWorkService
    {
        /// <summary>
        /// Получить учебную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="educationalWorkId">Идентификатор учебной работы преподавателя.</param>
        /// <returns>Учебная работа преподавателя.</returns>
        EducationalWork GetEducationalWorkById(int educationalWorkId);

        /// <summary>
        /// Получить учебные работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Учебные работы по плану преподавателя.</returns>
        List<EducationalWork> LoadEducationalWorksByPlan(int planId);

        /// <summary>
        /// Сохранить учебную работу.
        /// </summary>
        /// <param name="educationalWork">Учебная работа.</param>
        /// <returns>Идентификатор учебной работы.</returns>
        int SaveEducationalWork(EducationalWork educationalWork);

        /// <summary>
        /// Удалить учебную работу.
        /// </summary>
        /// <param name="educationalWorkId">Идентификатор учебной работы.</param>
        void DeleteEducationalWork(int educationalWorkId);

        /// <summary>
        /// Получить тип учебной работы преподавателя по идентификатору.
        /// </summary>
        /// <param name="educationalWorkTypeId">Идентификатор типа учебной работы преподавателя.</param>
        /// <returns>Тип учебной работы преподавателя.</returns>
        EducationalWorkType GetEducationalWorkTypeById(int educationalWorkTypeId);

        /// <summary>
        /// Получить типы учебной работы преподавателя.
        /// </summary>
        /// <returns>Типы учебной работы преподавателя.</returns>
        List<EducationalWorkType> GetAllEducationalWorkTypes();
    }
}
