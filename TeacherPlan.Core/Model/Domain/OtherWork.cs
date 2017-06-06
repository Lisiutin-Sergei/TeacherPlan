namespace TeacherPlan.Core.Model.Domain
{
	/// <summary>
	/// Доменная модель сущности "Прочие виды работ".
	/// </summary>
	public class OtherWork
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int OtherWorkId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

		/// <summary>
		/// Вид поручений, работ.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Срок исполнения.
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
