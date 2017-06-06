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
	/// Сервис для сущности "Прочие виды работ".
	/// </summary>
	public class OtherWorkService : IOtherWorkService
	{
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public OtherWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="otherWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public OtherWork GetOtherWorkById(int otherWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.OtherWorkRepository
                    .GetByID(otherWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<OtherWork> LoadOtherWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.OtherWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="otherWork">Сущность.</param>
        private void ValidateSave(OtherWork otherWork)
        {
            Argument.NotNull(otherWork, "Не указана сущность.");
            Argument.Require(otherWork.PlanId > 0, "Не указан план.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="otherWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveOtherWork(OtherWork otherWork)
        {
            Argument.NotNull(otherWork, "Не указана сущность.");
            var isEdit = otherWork.OtherWorkId > 0;

            ValidateSave(otherWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.OtherWorkRepository.Update(otherWork);
                    return otherWork.OtherWorkId;
                }
                else
                {
                    return unitOfWork.OtherWorkRepository.Insert(otherWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="otherWorkId">Идентификатор сущности.</param>
        public void DeleteOtherWork(int otherWorkId)
        {
            Argument.Require(otherWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var otherWork = unitOfWork.OtherWorkRepository.GetByID(otherWorkId);

                unitOfWork.OtherWorkRepository.Delete(otherWork);
            }
        }
    }
}
