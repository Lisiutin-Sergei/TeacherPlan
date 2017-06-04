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
    /// Сервис для сущности "Учебно-методическая работа".
    /// </summary>
    public class EduMethodWorkService : IEduMethodWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public EduMethodWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить учебную работу преподавателя по идентификатору.
        /// </summary>
        /// <param name="eduMethodWorkId">Идентификатор учебно-методической работы преподавателя.</param>
        /// <returns>Учебно-методическая работа преподавателя.</returns>
        public EduMethodWork GetEduMethodWorkById(int eduMethodWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EduMethodWorkRepository
                    .GetByID(eduMethodWorkId);
            }
        }

        /// <summary>
        /// Получить учебно-методические работы преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Учебно-методические работы по плану преподавателя.</returns>
        public List<EduMethodWork> LoadEduMethodWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.EduMethodWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения учебно-методической работы преподавателя.
        /// </summary>
        /// <param name="educationalWork">Учебно-методическая работа преподавателя.</param>
        private void ValidateSave(EduMethodWork educationalWork)
        {
            Argument.NotNull(educationalWork, "Не указана учебная работа преподавателя.");
            Argument.Require(educationalWork.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(educationalWork.Name, "Не указано название учебной работы.");
        }

        /// <summary>
        /// Сохранить учебно-методическую работу.
        /// </summary>
        /// <param name="eduMethodWork">Учебно-методическая работа.</param>
        /// <returns>Идентификатор учебно-методической работы.</returns>
        public int SaveEduMethodWork(EduMethodWork eduMethodWork)
        {
            Argument.NotNull(eduMethodWork, "Не указана учебно-методическая работа преподавателя.");
            var isEdit = eduMethodWork.EduMethodWorkId > 0;

            ValidateSave(eduMethodWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.EduMethodWorkRepository.Update(eduMethodWork);
                    return eduMethodWork.EduMethodWorkId;
                }
                else
                {
                    return unitOfWork.EduMethodWorkRepository.Insert(eduMethodWork);
                }
            }
        }

        /// <summary>
        /// Удалить учебно-методическую работу.
        /// </summary>
        /// <param name="eduMethodWorkId">Идентификатор учебно-методической работы.</param>
        public void DeleteEduMethodWork(int eduMethodWorkId)
        {
            Argument.Require(eduMethodWorkId > 0, "Не указан идентификатор учебно-методической работы преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var work = unitOfWork.EduMethodWorkRepository.GetByID(eduMethodWorkId);

                unitOfWork.EduMethodWorkRepository.Delete(work);
            }
        }
    }
}
