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
    /// Сервис для сущности "Издано учебников".
    /// </summary>
    public class BooksPublishingService : IBooksPublishingService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public BooksPublishingService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить издание учебников преподавателя по идентификатору.
        /// </summary>
        /// <param name="booksPublishingId">Идентификатор издания учебников преподавателя.</param>
        /// <returns>Издание учебников преподавателя.</returns>
        public BooksPublishing GetBooksPublishingById(int booksPublishingId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.BooksPublishingRepository
                    .GetByID(booksPublishingId);
            }
        }

        /// <summary>
        /// Получить издания учебников преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Издания учебников по плану преподавателя.</returns>
        public List<BooksPublishing> LoadBooksPublishingsByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.BooksPublishingRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения издания учебников преподавателя.
        /// </summary>
        /// <param name="bookWriting">Издание учебников преподавателя.</param>
        private void ValidateSave(BooksPublishing bookWriting)
        {
            Argument.NotNull(bookWriting, "Не указано издание учебников преподавателя.");
            Argument.Require(bookWriting.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(bookWriting.Name, "Не указано название учебника.");
        }

        /// <summary>
        /// Сохранить издание учебников.
        /// </summary>
        /// <param name="booksPublishing">Издание учебников.</param>
        /// <returns>Идентификатор издания учебников.</returns>
        public int SaveBooksPublishing(BooksPublishing booksPublishing)
        {
            Argument.NotNull(booksPublishing, "Не указано издание учебников преподавателя.");
            var isEdit = booksPublishing.BooksPublishingId > 0;

            ValidateSave(booksPublishing);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.BooksPublishingRepository.Update(booksPublishing);
                    return booksPublishing.BooksPublishingId;
                }
                else
                {
                    return unitOfWork.BooksPublishingRepository.Insert(booksPublishing);
                }
            }
        }

        /// <summary>
        /// Удалить издание учебников.
        /// </summary>
        /// <param name="booksPublishingId">Идентификатор издания учебников.</param>
        public void DeleteBooksPublishing(int booksPublishingId)
        {
            Argument.Require(booksPublishingId > 0, "Не указан идентификатор издания учебников преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var bookWriting = unitOfWork.BooksPublishingRepository.GetByID(booksPublishingId);

                unitOfWork.BooksPublishingRepository.Delete(bookWriting);
            }
        }
    }
}
