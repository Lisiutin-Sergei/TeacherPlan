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
    /// Сервис для сущности "Руководство научной исследовательской работой студентов".
    /// </summary>
    public class StudentResearchService : IStudentResearchService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public StudentResearchService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Получить сущность по идентификатору.
        /// </summary>
        /// <param name="studentResearchId">Идентификатор сущности.</param>
        /// <returns>Сущность.</returns>
        public StudentResearch GetStudentResearchById(int studentResearchId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.StudentResearchRepository
                    .GetByID(studentResearchId);
            }
        }

        /// <summary>
        /// Получить сущности по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>Сущности.</returns>
        public List<StudentResearch> LoadStudentResearchsByPlan(int planId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.StudentResearchRepository
                    .GetByFilter(e => e.PlanId == planId)
                    .ToList();
            }
        }

        /// <summary>
        /// Валидация сохранения сущности.
        /// </summary>
        /// <param name="studentResearch">Сущность.</param>
        private void ValidateSave(StudentResearch studentResearch)
        {
            Argument.NotNull(studentResearch, "Не указано руководство научной исследовательской работой студентов преподавателя.");
            Argument.Require(studentResearch.PlanId > 0, "Не указан план учебной работы.");
        }

        /// <summary>
        /// Сохранить сущность.
        /// </summary>
        /// <param name="studentResearch">Сущность.</param>
        /// <returns>Идентификатор сущности.</returns>
        public int SaveStudentResearch(StudentResearch studentResearch)
        {
            Argument.NotNull(studentResearch, "Не указано руководство научной исследовательской работой студентов преподавателя.");
            var isEdit = studentResearch.StudentResearchId > 0;

            ValidateSave(studentResearch);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                if (isEdit)
                {
                    unitOfWork.StudentResearchRepository.Update(studentResearch);
                    return studentResearch.StudentResearchId;
                }
                else
                {
                    return unitOfWork.StudentResearchRepository.Insert(studentResearch);
                }
            }
        }

        /// <summary>
        /// Удалить сущность.
        /// </summary>
        /// <param name="studentResearchId">Идентификатор сущности.</param>
        public void DeleteStudentResearch(int studentResearchId)
        {
            Argument.Require(studentResearchId > 0, "Не указан идентификатор руководства научной исследовательской работой студентов преподавателя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var studentResearch = unitOfWork.StudentResearchRepository.GetByID(studentResearchId);

                unitOfWork.StudentResearchRepository.Delete(studentResearch);
            }
        }
    }
}
