using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using TeacherPlan.Core.Interface;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Utilities.Common;

namespace TeacherPlan.Core.Service
{
    /// <summary>
    /// Сервис для сущности "Печатные (научные) труды за год".
    /// </summary>
    public class PublicationService : IPublicationService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public PublicationService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="publicationId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public Publication GetPublicationById(int publicationId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PublicationRepository
                    .GetByID(publicationId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности.</returns>
        public List<Publication> LoadPublicationsByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PublicationRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения сущности.
        /// </summary>
        /// <param name="publication">Сущность.</param>
        private void ValidateSave(Publication publication)
        {
            Argument.NotNull(publication, "Не указан печатный труд преподавателя.");
            Argument.Require(publication.PlanId > 0, "Не указан план преподавателя.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="publication">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SavePublication(Publication publication)
        {
            Argument.NotNull(publication, "Не указан печатный труд преподавателя.");
            var isEdit = publication.PublicationId > 0;

            ValidateSave(publication);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.PublicationRepository.Update(publication);
                    return publication.PublicationId;
                }
                else
                {
                    return unitOfWork.PublicationRepository.Insert(publication);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="publicationId">Идентификатор сущности.</param>
        public void DeletePublication(int publicationId)
        {
            Argument.Require(publicationId > 0, "Не указан идентификатор печатного труда преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var publication = unitOfWork.PublicationRepository.GetByID(publicationId);

                unitOfWork.PublicationRepository.Delete(publication);
            }
        }
    }
}
