namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Печатные (научные) труды за год.
    /// </summary>
    public class Publication
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int PublicationId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Наименование работы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Вышло ли из печати.
        /// </summary>
        public bool IsPublished { get; set; }

        /// <summary>
        /// Соавторы.
        /// </summary>
        public string Coauthors { get; set; }

        /// <summary>
        /// Объем в стр.
        /// </summary>
        public int? Volume { get; set; }
    }
}
