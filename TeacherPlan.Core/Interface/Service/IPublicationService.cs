using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Печатные (научные) труды за год".
    /// </summary>
    public interface IPublicationService
    {
        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="publicationId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        Publication GetPublicationById(int publicationId);

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности.</returns>
        List<Publication> LoadPublicationsByPlan(int planId);

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="publication">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        int SavePublication(Publication publication);

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="publicationId">Идентификатор сущности.</param>
        void DeletePublication(int publicationId);
    }
}
