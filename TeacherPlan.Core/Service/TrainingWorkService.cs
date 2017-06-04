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
    /// Сервис для сущности "Воспитательная и внеаудиторная и работа со студентами".
    /// </summary>
    public class TrainingWorkService : ITrainingWorkService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public TrainingWorkService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="trainingWorkId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public TrainingWork GetTrainingWorkById(int trainingWorkId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.TrainingWorkRepository
                    .GetByID(trainingWorkId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности по плану преподавателя.</returns>
        public List<TrainingWork> LoadTrainingWorksByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.TrainingWorkRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения.
        /// </summary>
        /// <param name="trainingWork">Сущность.</param>
        private void ValidateSave(TrainingWork trainingWork)
        {
            Argument.NotNull(trainingWork, "Не указана сущность.");
            Argument.Require(trainingWork.PlanId > 0, "Не указан план.");
            Argument.NotNullOrWhiteSpace(trainingWork.Name, "Не указано название.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="trainingWork">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveTrainingWork(TrainingWork trainingWork)
        {
            Argument.NotNull(trainingWork, "Не указана сущность.");
            var isEdit = trainingWork.TrainingWorkId > 0;

            ValidateSave(trainingWork);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.TrainingWorkRepository.Update(trainingWork);
                    return trainingWork.TrainingWorkId;
                }
                else
                {
                    return unitOfWork.TrainingWorkRepository.Insert(trainingWork);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="trainingWorkId">Идентификатор сущности.</param>
        public void DeleteTrainingWork(int trainingWorkId)
        {
            Argument.Require(trainingWorkId > 0, "Не указан идентификатор сущности.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var trainingWork = unitOfWork.TrainingWorkRepository.GetByID(trainingWorkId);

                unitOfWork.TrainingWorkRepository.Delete(trainingWork);
            }
        }
    }
}
