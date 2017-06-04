using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    public interface IEduMethodWorkService
    {
        /// <summary>
        /// Получить учебную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="eduMethodWorkId">Идентификатор учебно-методической работы преподавателя.</param>
        /// <returns>Учебно-методическая работа преподавателя.</returns>
        EduMethodWork GetEduMethodWorkById(int educationalWorkId);

        /// <summary>
        /// Получить учебно-методические работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Учебно-методические работы по плану преподавателя.</returns>
        List<EduMethodWork> LoadEduMethodWorksByPlan(int planId);

        /// <summary>
        /// Сохранить учебно-методическую работу.
        /// </summary>
        /// <param name="eduMethodWork">Учебно-методическая работа.</param>
        /// <returns>Идентификатор учебно-методической работы.</returns>
        int SaveEduMethodWork(EduMethodWork educationalWork);

        /// <summary>
        /// Удалить учебно-методическую работу.
        /// </summary>
        /// <param name="eduMethodWorkId">Идентификатор учебно-методической работы.</param>
        void DeleteEduMethodWork(int educationalWorkId);
    }
}
