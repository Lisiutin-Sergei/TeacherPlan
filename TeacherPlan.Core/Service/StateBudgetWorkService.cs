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
    /// Сервис для сущности "Госбюджетная работа".
    /// </summary>
    public class StateBudgetWorkService : IStateBudgetWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public StateBudgetWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить госбюджетную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="stateBudgetWorkId">Идентификатор госбюджетной работы преподавателя.</param>
        /// <returns>Госбюджетная работа преподавателя.</returns>
        public StateBudgetWork GetStateBudgetWorkById(int stateBudgetWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.StateBudgetWorkRepository
                    .GetByID(stateBudgetWorkId);
            }
        }

        /// <summary>
        /// Получить госбюджетные работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Госбюджетные работы по плану преподавателя.</returns>
        public List<StateBudgetWork> LoadStateBudgetWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.StateBudgetWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения госбюджетной работы преподавателя.
        /// </summary>
        /// <param name="stateBudgetWork">Госбюджетная работа преподавателя.</param>
        private void ValidateSave(StateBudgetWork stateBudgetWork)
        {
            Argument.NotNull(stateBudgetWork, "Не указано госбюджетная работа преподавателя.");
            Argument.Require(stateBudgetWork.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(stateBudgetWork.Name, "Не указано название учебника.");
        }

        /// <summary>
        /// Сохранить госбюджетную работу.
        /// </summary>
        /// <param name="stateBudgetWork">Госбюджетная работа.</param>
        /// <returns>Идентификатор госбюджетной работы.</returns>
        public int SaveStateBudgetWork(StateBudgetWork stateBudgetWork)
        {
            Argument.NotNull(stateBudgetWork, "Не указано госбюджетную работу преподавателя.");
            var isEdit = stateBudgetWork.StateBudgetWorkId > 0;

            ValidateSave(stateBudgetWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.StateBudgetWorkRepository.Update(stateBudgetWork);
                    return stateBudgetWork.StateBudgetWorkId;
                }
                else
                {
                    return unitOfWork.StateBudgetWorkRepository.Insert(stateBudgetWork);
                }
            }
        }

        /// <summary>
        /// Удалить госбюджетную работу.
        /// </summary>
        /// <param name="stateBudgetWorkId">Идентификатор госбюджетной работы.</param>
        public void DeleteStateBudgetWork(int stateBudgetWorkId)
        {
            Argument.Require(stateBudgetWorkId > 0, "Не указан идентификатор госбюджетной работы преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var stateBudgetWork = unitOfWork.StateBudgetWorkRepository.GetByID(stateBudgetWorkId);

                unitOfWork.StateBudgetWorkRepository.Delete(stateBudgetWork);
            }
        }
    }
}
