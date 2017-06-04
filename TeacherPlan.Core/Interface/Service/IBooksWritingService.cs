using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Написание учебников".
    /// </summary>
    public interface IBookWritingService
    {
        /// <summary>
        /// Получить написание учебников преподавателя по идентификатору.
        /// </summary>
        /// <param name="booksWritingId">Идентификатор написания учебников преподавателя.</param>
        /// <returns>Написание учебников преподавателя.</returns>
        BookWriting GetBooksWritingById(int booksWritingId);

        /// <summary>
        /// Получить написания учебников преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Написания учебников по плану преподавателя.</returns>
        List<BookWriting> LoadBooksWritingsByPlan(int planId);

        /// <summary>
        /// Сохранить написание учебников.
        /// </summary>
        /// <param name="bookWriting">Написание учебников.</param>
        /// <returns>Идентификатор написания учебников.</returns>
        int SaveBooksWriting(BookWriting bookWriting);

        /// <summary>
        /// Удалить написание учебников.
        /// </summary>
        /// <param name="booksWritingId">Идентификатор написания учебников.</param>
        void DeleteBooksWriting(int booksWritingId);
    }
}
