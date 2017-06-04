namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Руководство научной исследовательской работой студентов".
    /// </summary>
    public class StudentResearch
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int StudentResearchId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Студент (Ф.И.О.).
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// № ак. группы.
        /// </summary>
        public string StudentGroup { get; set; }

        /// <summary>
        /// Код ООП.
        /// </summary>
        public string OopCode { get; set; }

        /// <summary>
        /// Тема работы.
        /// </summary>
        public string Research { get; set; }

        /// <summary>
        /// Отметки о выполнении.
        /// </summary>
        public string Execution { get; set; }
    }
}
