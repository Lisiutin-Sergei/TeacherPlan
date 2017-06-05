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
    /// Сервис для сущности "Запланированные работы".
    /// </summary>
    public class PlannedWorkService : IPlannedWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public PlannedWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="plannedWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public PlannedWork GetPlannedWorkById(int plannedWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PlannedWorkRepository
                    .GetByID(plannedWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<PlannedWork> LoadPlannedWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PlannedWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="plannedWork">Сущность.</param>
        private void ValidateSave(PlannedWork plannedWork)
        {
            Argument.NotNull(plannedWork, "Не указана сущность.");
            Argument.Require(plannedWork.PlanId > 0, "Не указан план.");
            Argument.NotNullOrWhiteSpace(plannedWork.Name, "Не указано название.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="plannedWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SavePlannedWork(PlannedWork plannedWork)
        {
            Argument.NotNull(plannedWork, "Не указана сущность.");
            var isEdit = plannedWork.PlannedWorkId > 0;

            ValidateSave(plannedWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.PlannedWorkRepository.Update(plannedWork);
                    return plannedWork.PlannedWorkId;
                }
                else
                {
                    return unitOfWork.PlannedWorkRepository.Insert(plannedWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="plannedWorkId">Идентификатор сущности.</param>
        public void DeletePlannedWork(int plannedWorkId)
        {
            Argument.Require(plannedWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var plannedWork = unitOfWork.PlannedWorkRepository.GetByID(plannedWorkId);

                unitOfWork.PlannedWorkRepository.Delete(plannedWork);
            }
        }
    }
}
