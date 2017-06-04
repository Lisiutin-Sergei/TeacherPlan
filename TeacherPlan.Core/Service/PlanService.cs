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
    /// Сервис планов преподавателей.
    /// </summary>
    public class PlanService : IPlanService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public PlanService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить план преподавателя по идентификатору.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>План преподавателя.</returns>
        public Plan GetPlanById(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PlanRepository
                    .GetByID(planId);
            }
        }

        /// <summary>
        /// Получить список планов преподавателя по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список планов преподавателя.</returns>
        public List<Plan> LoadPlansByFilter(Func<Plan, bool> filter)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PlanRepository
                    .GetByFilter(filter)
                    .ToList();
            }
        }

        /// <summary>
        /// Загрузить список планов преподавателя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Список планов преподавателя.</returns>
        public List<Plan> LoadUserPlans(int userId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.PlanRepository
                    .GetByFilter(e => e.UserId == userId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения плана преподавателя.
        /// </summary>
        /// <param name="plan">План преподавателя.</param>
        private void ValidateSave(Plan plan)
        {
            Argument.NotNull(plan, "Не указан план преподавателя.");
            Argument.NotNullOrWhiteSpace(plan.Name, "Не указано название плана преподавателя.");
            Argument.NotNullOrWhiteSpace(plan.PlanYear, "Не указан год плана преподавателя.");
            Argument.Require(plan.DateFrom != default(DateTime), "Не указано начало срока работы преподавателя.");
            Argument.Require(plan.DateTo != default(DateTime), "Не указано окончание срока работы преподавателя.");
        }

        /// <summary>
        /// Сохранить план.
        /// </summary>
        /// <param name="plan">План.</param>
        /// <returns>Идентификатор плана.</returns>
        public int SavePlan(Plan plan)
        {
            Argument.NotNull(plan, "Не указан план преподавателя для сохранения.");
            var isEdit = plan.PlanId > 0;

            ValidateSave(plan);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.PlanRepository.Update(plan);
                    return plan.PlanId;
                }
                else
                {
                    return unitOfWork.PlanRepository.Insert(plan);
                }
            }
        }

        /// <summary>
        /// Удалить план.
        /// </summary>
        /// <param name="planId">Идентификатор плана.</param>
        public void DeletePlan(int planId)
        {
            Argument.Require(planId > 0, "Не указан идентификатор плана преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var plan = unitOfWork.PlanRepository.GetByID(planId);

                // Список учебных работ
                var educationWorks = unitOfWork.EducationalWorkRepository
                    .GetByFilter(e => e.PlanId == planId);

                // Удалить в транзакции все последствия плана
                unitOfWork.BeginTransaction();
                try
                {
                    // Удалить учебные работы
                    foreach(var educationWork in educationWorks)
                    {
                        unitOfWork.EducationalWorkRepository.Delete(educationWork);
                    }

                    // Удалить план
                    unitOfWork.PlanRepository.Delete(plan);

                    unitOfWork.CommitTransaction();
                }
                catch
                {
                    unitOfWork.RollbackTransaction();
                    throw;
                }

            }
        }
    }
}
