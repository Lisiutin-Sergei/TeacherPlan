using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherPlan.Core.Interface;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Utilities.Common;
using TeacherPlan.Utilities.ExtensionMethods;

namespace TeacherPlan.Core.Service
{
    /// <summary>
    /// Сервис для сущности "Написание учебников".
    /// </summary>
    public class BooksWritingService : IBookWritingService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public BooksWritingService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить написание учебников преподавателя по идентификатору.
        /// </summary>
        /// <param name="booksWritingId">Идентификатор написания учебников преподавателя.</param>
        /// <returns>Написание учебников преподавателя.</returns>
        public BookWriting GetBooksWritingById(int booksWritingId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.BooksWritingRepository
                    .GetByID(booksWritingId);
            }
        }

        /// <summary>
        /// Получить написания учебников преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Написания учебников по плану преподавателя.</returns>
        public List<BookWriting> LoadBooksWritingsByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.BooksWritingRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения написания учебников преподавателя.
        /// </summary>
        /// <param name="bookWriting">Написание учебников преподавателя.</param>
        private void ValidateSave(BookWriting bookWriting)
        {
            Argument.NotNull(bookWriting, "Не указано написание учебников преподавателя.");
            Argument.Require(bookWriting.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(bookWriting.Name, "Не указано название учебника.");
        }

        /// <summary>
        /// Сохранить написание учебников.
        /// </summary>
        /// <param name="bookWriting">Написание учебников.</param>
        /// <returns>Идентификатор написания учебников.</returns>
        public int SaveBooksWriting(BookWriting bookWriting)
        {
            Argument.NotNull(bookWriting, "Не указано написание учебников преподавателя.");
            var isEdit = bookWriting.BookWritingId > 0;

            ValidateSave(bookWriting);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.BooksWritingRepository.Update(bookWriting);
                    return bookWriting.BookWritingId;
                }
                else
                {
                    return unitOfWork.BooksWritingRepository.Insert(bookWriting);
                }
            }
        }

        /// <summary>
        /// Удалить написание учебников.
        /// </summary>
        /// <param name="booksWritingId">Идентификатор написания учебников.</param>
        public void DeleteBooksWriting(int booksWritingId)
        {
            Argument.Require(booksWritingId > 0, "Не указан идентификатор написания учебников преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var bookWriting = unitOfWork.BooksWritingRepository.GetByID(booksWritingId);

                unitOfWork.BooksWritingRepository.Delete(bookWriting);
            }
        }
    }
}
