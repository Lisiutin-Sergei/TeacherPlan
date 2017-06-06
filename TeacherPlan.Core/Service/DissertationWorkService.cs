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
	/// Сервис для сущности "Работа над диссертацией".
	/// </summary>
	public class DissertationWorkService : IDissertationWorkService
	{
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public DissertationWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="dissertationWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public DissertationWork GetDissertationWorkById(int dissertationWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.DissertationWorkRepository
                    .GetByID(dissertationWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<DissertationWork> LoadDissertationWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.DissertationWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="dissertationWork">Сущность.</param>
        private void ValidateSave(DissertationWork dissertationWork)
        {
            Argument.NotNull(dissertationWork, "Не указана сущность.");
            Argument.Require(dissertationWork.PlanId > 0, "Не указан план.");
            Argument.NotNullOrWhiteSpace(dissertationWork.Name, "Не указано название.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="dissertationWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveDissertationWork(DissertationWork dissertationWork)
        {
            Argument.NotNull(dissertationWork, "Не указана сущность.");
            var isEdit = dissertationWork.DissertationWorkId > 0;

            ValidateSave(dissertationWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.DissertationWorkRepository.Update(dissertationWork);
                    return dissertationWork.DissertationWorkId;
                }
                else
                {
                    return unitOfWork.DissertationWorkRepository.Insert(dissertationWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="dissertationWorkId">Идентификатор сущности.</param>
        public void DeleteDissertationWork(int dissertationWorkId)
        {
            Argument.Require(dissertationWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var dissertationWork = unitOfWork.DissertationWorkRepository.GetByID(dissertationWorkId);

                unitOfWork.DissertationWorkRepository.Delete(dissertationWork);
            }
        }
    }
}
