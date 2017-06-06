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
	/// Сервис для сущности "Хоздоговорная работа".
	/// </summary>
	public class ContractWorkService : IContractWorkService
	{
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public ContractWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="contractWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public ContractWork GetContractWorkById(int contractWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ContractWorkRepository
                    .GetByID(contractWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<ContractWork> LoadContractWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ContractWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="contractWork">Сущность.</param>
        private void ValidateSave(ContractWork contractWork)
        {
            Argument.NotNull(contractWork, "Не указана сущность.");
            Argument.Require(contractWork.PlanId > 0, "Не указан план.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="contractWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveContractWork(ContractWork contractWork)
        {
            Argument.NotNull(contractWork, "Не указана сущность.");
            var isEdit = contractWork.ContractWorkId > 0;

            ValidateSave(contractWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.ContractWorkRepository.Update(contractWork);
                    return contractWork.ContractWorkId;
                }
                else
                {
                    return unitOfWork.ContractWorkRepository.Insert(contractWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="contractWorkId">Идентификатор сущности.</param>
        public void DeleteContractWork(int contractWorkId)
        {
            Argument.Require(contractWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var contractWork = unitOfWork.ContractWorkRepository.GetByID(contractWorkId);

                unitOfWork.ContractWorkRepository.Delete(contractWork);
            }
        }
    }
}
