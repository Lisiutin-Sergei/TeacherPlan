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
    /// Сервис учебных работ.
    /// </summary>
    public class EducationalWorkService : IEducationalWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public EducationalWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить учебную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="educationalWorkId">Идентификатор учебной работы преподавателя.</param>
        /// <returns>Учебная работа преподавателя.</returns>
        public EducationalWork GetEducationalWorkById(int educationalWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EducationalWorkRepository
                    .GetByID(educationalWorkId);
            }
        }

        /// <summary>
        /// Получить учебные работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Учебные работы по плану преподавателя.</returns>
        public List<EducationalWork> LoadEducationalWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EducationalWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения учебной работы преподавателя.
        /// </summary>
        /// <param name="educationalWork">Учебная работа преподавателя.</param>
        private void ValidateSave(EducationalWork educationalWork)
        {
            Argument.NotNull(educationalWork, "Не указана учебная работа преподавателя.");
            Argument.Require(educationalWork.EducationalWorkTypeId > 0, "Не указан тип учебной работы.");
            Argument.Require(educationalWork.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(educationalWork.Name, "Не указано название учебной работы.");
        }

        /// <summary>
        /// Сохранить учебную работу.
        /// </summary>
        /// <param name="educationalWork">Учебная работа.</param>
        /// <returns>Идентификатор учебной работы.</returns>
        public int SaveEducationalWork(EducationalWork educationalWork)
        {
            Argument.NotNull(educationalWork, "Не указана учебная работа преподавателя.");
            var isEdit = educationalWork.EducationalWorkId > 0;

            ValidateSave(educationalWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.EducationalWorkRepository.Update(educationalWork);
                    return educationalWork.EducationalWorkId;
                }
                else
                {
                    return unitOfWork.EducationalWorkRepository.Insert(educationalWork);
                }
            }
        }

        /// <summary>
        /// Удалить учебную работу.
        /// </summary>
        /// <param name="educationalWorkId">Идентификатор учебной работы.</param>
        public void DeleteEducationalWork(int educationalWorkId)
        {
            Argument.Require(educationalWorkId > 0, "Не указан идентификатор учебной работы преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var work = unitOfWork.EducationalWorkRepository.GetByID(educationalWorkId);

                unitOfWork.EducationalWorkRepository.Delete(work);
            }
        }

        /// <summary>
        /// Получить тип учебной работы преподавателя по идентификатору.
        /// </summary>
        /// <param name="educationalWorkTypeId">Идентификатор типа учебной работы преподавателя.</param>
        /// <returns>Тип учебной работы преподавателя.</returns>
        public EducationalWorkType GetEducationalWorkTypeById(int educationalWorkTypeId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EducationalWorkTypeRepository
                    .GetByID(educationalWorkTypeId);
            }
        }

        /// <summary>
        /// Получить типы учебной работы преподавателя.
        /// </summary>
        /// <returns>Типы учебной работы преподавателя.</returns>
        public List<EducationalWorkType> GetAllEducationalWorkTypes()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EducationalWorkTypeRepository
                    .GetAll()
                    .ToList();
            }
        }
    }
}
