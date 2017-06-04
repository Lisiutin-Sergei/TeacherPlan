namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Научные кружки".
    /// </summary>
    public class ScienceGroup
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ScienceGroupId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Наименование научного круж-ка.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Место кружковой работы.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Кол-во студен-тов.
        /// </summary>
        public int? StudentsCount { get; set; }

        /// <summary>
        /// Кол-во публика-ций.
        /// </summary>
        public int? PublicationsCount { get; set; }

        /// <summary>
        /// Участие в научн.конф.
        /// </summary>
        public int? ConferencesCount { get; set; }

        /// <summary>
        /// Дипломы конкурсов, олимпиад и т.д.
        /// </summary>
        public int? DiplomasCount { get; set; }
    }
}
