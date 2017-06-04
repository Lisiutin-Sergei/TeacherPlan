using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Издано учебников".
    /// </summary>
    public interface IBooksPublishingService
    {
        /// <summary>
        /// Получить издание учебников преподавателя по идентификатору.
        /// </summary>
        /// <param name="booksPublishingId">Идентификатор издания учебников преподавателя.</param>
        /// <returns>Издание учебников преподавателя.</returns>
        BooksPublishing GetBooksPublishingById(int booksPublishingId);

        /// <summary>
        /// Получить издания учебников преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Издания учебников по плану преподавателя.</returns>
        List<BooksPublishing> LoadBooksPublishingsByPlan(int planId);

        /// <summary>
        /// Сохранить издание учебников.
        /// </summary>
        /// <param name="booksPublishing">Издание учебников.</param>
        /// <returns>Идентификатор издания учебников.</returns>
        int SaveBooksPublishing(BooksPublishing booksPublishing);

        /// <summary>
        /// Удалить издание учебников.
        /// </summary>
        /// <param name="booksPublishingId">Идентификатор издания учебников.</param>
        void DeleteBooksPublishing(int booksPublishingId);
    }
}
