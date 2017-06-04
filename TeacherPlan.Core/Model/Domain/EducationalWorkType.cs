namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель сущности "Тип учебной работы".
    /// </summary>
    public class EducationalWorkType
    {
        /// <summary>
        /// Идентификатор типа учебной работы.
        /// </summary>
        public int EducationalWorkTypeId { get; set; }
        
        /// <summary>
        /// Название.
        /// </summary>
        public string Name { get; set; }
    }
}
