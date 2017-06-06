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
	/// Сервис для сущности "Дополнительная образовательная деятельность".
	/// </summary>
	public class AdditionalWorkService : IAdditionalWorkService
	{
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public AdditionalWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="additionalWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public AdditionalWork GetAdditionalWorkById(int additionalWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.AdditionalWorkRepository
                    .GetByID(additionalWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<AdditionalWork> LoadAdditionalWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.AdditionalWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="additionalWork">Сущность.</param>
        private void ValidateSave(AdditionalWork additionalWork)
        {
            Argument.NotNull(additionalWork, "Не указана сущность.");
            Argument.Require(additionalWork.PlanId > 0, "Не указан план.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="additionalWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveAdditionalWork(AdditionalWork additionalWork)
        {
            Argument.NotNull(additionalWork, "Не указана сущность.");
            var isEdit = additionalWork.AdditionalWorkId > 0;

            ValidateSave(additionalWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.AdditionalWorkRepository.Update(additionalWork);
                    return additionalWork.AdditionalWorkId;
                }
                else
                {
                    return unitOfWork.AdditionalWorkRepository.Insert(additionalWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="additionalWorkId">Идентификатор сущности.</param>
        public void DeleteAdditionalWork(int additionalWorkId)
        {
            Argument.Require(additionalWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var additionalWork = unitOfWork.AdditionalWorkRepository.GetByID(additionalWorkId);

                unitOfWork.AdditionalWorkRepository.Delete(additionalWork);
            }
        }
    }
}
