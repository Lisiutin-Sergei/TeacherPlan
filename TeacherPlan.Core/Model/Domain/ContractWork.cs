namespace TeacherPlan.Core.Model.Domain
{
	/// <summary>
	/// Доменная модель сущности "Хоздоговорная работа".
	/// </summary>
	public class ContractWork
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int ContractWorkId { get; set; }

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        public int PlanId { get; set; }

        /// <summary>
        /// Наименование темы.
        /// </summary>
        public string Name { get; set; }

		/// <summary>
		/// Вид рабо-ты (ХД, грант, про-граммы и др.).
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Годовой объем финансирова-ния, в руб.
		/// </summary>
		public double? Volume { get; set; }

		/// <summary>
		/// Должностные обязанности.
		/// </summary>
		public string Duty { get; set; }
		
        /// <summary>
        /// Отметки о выполнении.
        /// </summary>
        public string Execution { get; set; }

		/// <summary>
		/// Примечания.
		/// </summary>
		public string Comment { get; set; }

	}
}
