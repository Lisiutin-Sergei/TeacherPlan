using System;

namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель плана преподавателя.
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Идентификатор плана преподавателя.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Название плана.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата начала срока службы преподавателя.
        /// </summary>
        public DateTime DateFrom { get; set; }

        /// <summary>
        /// Дата окончания срока службы преподавателя.
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// Период плана.
        /// </summary>
        public string PlanYear { get; set; }
    }
}
