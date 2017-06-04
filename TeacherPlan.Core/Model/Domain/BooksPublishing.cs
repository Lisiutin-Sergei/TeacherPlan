namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Издано учебников".
    /// </summary>
    public class BooksPublishing
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int BooksPublishingId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Выход.
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// Назначение.
        /// </summary>
        public string Purpose { get; set; }
        
        /// <summary>
        /// Соавторы.
        /// </summary>
        public string Coauthors { get; set; }

        /// <summary>
        /// Объем.
        /// </summary>
        public int? Volume { get; set; }

    }
}
