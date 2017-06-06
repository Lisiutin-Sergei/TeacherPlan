namespace TeacherPlan.Core.Model.Domain
{
	/// <summary>
	/// Доменная модель сущности "Дополнительная образовательная деятельность".
	/// </summary>
	public class AdditionalWork
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int AdditionalWorkId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

		/// <summary>
		/// Вид деятельности.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Состав обучаемых.
		/// </summary>
		public string Students { get; set; }

		/// <summary>
		/// Подразделение, на базе которого проводится обучение.
		/// </summary>
		public string Place { get; set; }

		/// <summary>
		/// Программа обучения.
		/// </summary>
		public string Program { get; set; }

		/// <summary>
		/// Вид занятий.
		/// </summary>
		public string EducationType { get; set; }

		/// <summary>
		/// Объем занятий, час.
		/// </summary>
		public double? Volume { get; set; }

	}
}
