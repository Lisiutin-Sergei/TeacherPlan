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
	/// Сервис для сущности "Повышение квалификации".
	/// </summary>
	public class QualificationWorkService : IQualificationWorkService
	{
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public QualificationWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="qualificationWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public QualificationWork GetQualificationWorkById(int qualificationWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.QualificationWorkRepository
                    .GetByID(qualificationWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<QualificationWork> LoadQualificationWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.QualificationWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="qualificationWork">Сущность.</param>
        private void ValidateSave(QualificationWork qualificationWork)
        {
            Argument.NotNull(qualificationWork, "Не указана сущность.");
            Argument.Require(qualificationWork.PlanId > 0, "Не указан план.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="qualificationWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveQualificationWork(QualificationWork qualificationWork)
        {
            Argument.NotNull(qualificationWork, "Не указана сущность.");
            var isEdit = qualificationWork.QualificationWorkId > 0;

            ValidateSave(qualificationWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.QualificationWorkRepository.Update(qualificationWork);
                    return qualificationWork.QualificationWorkId;
                }
                else
                {
                    return unitOfWork.QualificationWorkRepository.Insert(qualificationWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="qualificationWorkId">Идентификатор сущности.</param>
        public void DeleteQualificationWork(int qualificationWorkId)
        {
            Argument.Require(qualificationWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var qualificationWork = unitOfWork.QualificationWorkRepository.GetByID(qualificationWorkId);

                unitOfWork.QualificationWorkRepository.Delete(qualificationWork);
            }
        }
    }
}
