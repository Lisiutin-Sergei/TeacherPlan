namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Учебная работа".
    /// </summary>
    public class EducationalWork
    {
        /// <summary>
        /// Идентификатор учебной работы.
        /// </summary>
        public int EducationalWorkId { get; set; }

        /// <summary>
        /// Идентификатор типа учебной работы.
        /// </summary>
        public int EducationalWorkTypeId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 1 семестр план.
        /// </summary>
        public double? FirstSemesterPlan { get; set; }

        /// <summary>
        /// 1 семестр факт.
        /// </summary>
        public double? FirstSemesterFact { get; set; }

        /// <summary>
        /// 2 семестр план.
        /// </summary>
        public double? SecondSemesterPlan { get; set; }

        /// <summary>
        /// 2 семестр факт.
        /// </summary>
        public double? SecondSemesterFact { get; set; }
    }
}
