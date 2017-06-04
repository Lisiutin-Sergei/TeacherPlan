namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Воспитательная и внеаудиторная и работа со студентами".
    /// </summary>
    public class TrainingWork
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int TrainingWorkId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Сроки.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Отметки о выполнении.
        /// </summary>
        public string Execution { get; set; }

        /// <summary>
        /// Часы.
        /// </summary>
        public double? Hours { get; set; }

    }
}
