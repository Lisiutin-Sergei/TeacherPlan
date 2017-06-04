using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Руководство научной исследовательской работой студентов".
    /// </summary>
    public interface IStudentResearchService
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="studentResearchId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        StudentResearch GetStudentResearchById(int studentResearchId);

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности.</returns>
        List<StudentResearch> LoadStudentResearchsByPlan(int planId);

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="studentResearch">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        int SaveStudentResearch(StudentResearch studentResearch);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="studentResearchId">Идентификатор сущности.</param>
        void DeleteStudentResearch(int studentResearchId);
    }
}
