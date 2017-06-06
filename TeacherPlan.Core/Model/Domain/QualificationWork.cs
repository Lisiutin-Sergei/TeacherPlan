namespace TeacherPlan.Core.Model.Domain
{
	/// <summary>
	/// Доменная модель сущности "Повышение квалификации".
	/// </summary>
	public class QualificationWork
	{
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int QualificationWorkId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

		/// <summary>
		/// Наименование про-граммы обучения.
		/// </summary>
		public string CourseName { get; set; }

		/// <summary>
		/// Вид обучения.
		/// </summary>
		public string CourseType { get; set; }

		/// <summary>
		/// Объем курса.
		/// </summary>
		public double? CourseVolume { get; set; }

		/// <summary>
		/// Наименование образовательно-го учреждения.
		/// </summary>
		public string Place { get; set; }

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
