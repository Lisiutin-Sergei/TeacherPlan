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
    /// Сервис для сущности "Профориентационная работа".
    /// </summary>
    public class ProfessionalWorkService : IProfessionalWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public ProfessionalWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="professionalWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public ProfessionalWork GetProfessionalWorkById(int professionalWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ProfessionalWorkRepository
                    .GetByID(professionalWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<ProfessionalWork> LoadProfessionalWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ProfessionalWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="professionalWork">Сущность.</param>
        private void ValidateSave(ProfessionalWork professionalWork)
        {
            Argument.NotNull(professionalWork, "Не указана сущность.");
            Argument.Require(professionalWork.PlanId > 0, "Не указан план.");
            Argument.NotNullOrWhiteSpace(professionalWork.Name, "Не указано название.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="professionalWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveProfessionalWork(ProfessionalWork professionalWork)
        {
            Argument.NotNull(professionalWork, "Не указана сущность.");
            var isEdit = professionalWork.ProfessionalWorkId > 0;

            ValidateSave(professionalWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.ProfessionalWorkRepository.Update(professionalWork);
                    return professionalWork.ProfessionalWorkId;
                }
                else
                {
                    return unitOfWork.ProfessionalWorkRepository.Insert(professionalWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="professionalWorkId">Идентификатор сущности.</param>
        public void DeleteProfessionalWork(int professionalWorkId)
        {
            Argument.Require(professionalWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var professionalWork = unitOfWork.ProfessionalWorkRepository.GetByID(professionalWorkId);

                unitOfWork.ProfessionalWorkRepository.Delete(professionalWork);
            }
        }
    }
}
