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
    /// Сервис для сущности "Научные кружки".
    /// </summary>
    public class ScienceGroupService : IScienceGroupService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public ScienceGroupService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить научный кружок преподавателя по идентификатору.
        /// </summary>
        /// <param name="ScienceGroupId">Идентификатор научного кружка преподавателя.</param>
        /// <returns>Научный кружок преподавателя.</returns>
        public ScienceGroup GetScienceGroupById(int ScienceGroupId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ScienceGroupRepository
                    .GetByID(ScienceGroupId);
            }
        }

        /// <summary>
        /// Получить научные кружки преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>научные кружки по плану преподавателя.</returns>
        public List<ScienceGroup> LoadScienceGroupsByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.ScienceGroupRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения научного кружка преподавателя.
        /// </summary>
        /// <param name="scienceGroup">Научный кружок преподавателя.</param>
        private void ValidateSave(ScienceGroup scienceGroup)
        {
            Argument.NotNull(scienceGroup, "Не указан научный кружок преподавателя.");
            Argument.Require(scienceGroup.PlanId > 0, "Не указан план учебной работы.");
            Argument.NotNullOrWhiteSpace(scienceGroup.Name, "Не указано название научного кружка.");
        }

        /// <summary>
        /// Сохранить научный кружок.
        /// </summary>
        /// <param name="scienceGroup">Научный кружок.</param>
        /// <returns>Идентификатор научного кружка.</returns>
        public int SaveScienceGroup(ScienceGroup scienceGroup)
        {
            Argument.NotNull(scienceGroup, "Не указан научный кружок преподавателя.");
            var isEdit = scienceGroup.ScienceGroupId > 0;

            ValidateSave(scienceGroup);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.ScienceGroupRepository.Update(scienceGroup);
                    return scienceGroup.ScienceGroupId;
                }
                else
                {
                    return unitOfWork.ScienceGroupRepository.Insert(scienceGroup);
                }
            }
        }

        /// <summary>
        /// Удалить научный кружок.
        /// </summary>
        /// <param name="scienceGroupId">Идентификатор научного кружка.</param>
        public void DeleteScienceGroup(int scienceGroupId)
        {
            Argument.Require(scienceGroupId > 0, "Не указан идентификатор научного кружка преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var scienceGroup = unitOfWork.ScienceGroupRepository.GetByID(scienceGroupId);

                unitOfWork.ScienceGroupRepository.Delete(scienceGroup);
            }
        }
    }
}
