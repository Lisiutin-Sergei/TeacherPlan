using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Профориентационная работа".
    /// </summary>
    public interface IProfessionalWorkService
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="professionalWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        ProfessionalWork GetProfessionalWorkById(int professionalWorkId);

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        List<ProfessionalWork> LoadProfessionalWorksByPlan(int planId);

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="professionalWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        int SaveProfessionalWork(ProfessionalWork professionalWork);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="professionalWorkId">Идентификатор сущности.</param>
        void DeleteProfessionalWork(int professionalWorkId);
    }
}
