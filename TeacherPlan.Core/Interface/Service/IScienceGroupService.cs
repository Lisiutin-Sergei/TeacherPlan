using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса для сущности "Научные кружки".
    /// </summary>
    public interface IScienceGroupService
    {
        /// <summary>
        /// Получить научный кружок преподавателя по идентификатору.
        /// </summary>
        /// <param name="ScienceGroupId">Идентификатор научного кружка преподавателя.</param>
        /// <returns>Научный кружок преподавателя.</returns>
        ScienceGroup GetScienceGroupById(int ScienceGroupId);

        /// <summary>
        /// Получить научные кружки преподавателя по идентификатору плана.
        /// </summary>
        /// <param name="planId">Идентификатор плана преподавателя.</param>
        /// <returns>научные кружки по плану преподавателя.</returns>
        List<ScienceGroup> LoadScienceGroupsByPlan(int planId);

        /// <summary>
        /// Сохранить научный кружок.
        /// </summary>
        /// <param name="scienceGroup">Научный кружок.</param>
        /// <returns>Идентификатор научного кружка.</returns>
        int SaveScienceGroup(ScienceGroup scienceGroup);

        /// <summary>
        /// Удалить научный кружок.
        /// </summary>
        /// <param name="scienceGroupId">Идентификатор научного кружка.</param>
        void DeleteScienceGroup(int scienceGroupId);
    }
}
