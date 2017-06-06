using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
	/// <summary>
	/// Интерфейс сервиса для сущности "Дополнительная образовательная деятельность".
	/// </summary>
	public interface IAdditionalWorkService
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="additionalWorkId">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		AdditionalWork GetAdditionalWorkById(int additionalWorkId);

		/// <summary>
		/// Получить сущности по идентификатору плана.
		/// </summary>
		/// <param name="planId">Идентификатор плана преподавателя.</param>
		/// <returns>Сущности по плану преподавателя.</returns>
		List<AdditionalWork> LoadAdditionalWorksByPlan(int planId);

		/// <summary>
		/// Сохранить сущность.
		/// </summary>
		/// <param name="additionalWork">Сущность.</param>
		/// <returns>Идентификатор сущности.</returns>
		int SaveAdditionalWork(AdditionalWork additionalWork);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="additionalWorkId">Идентификатор сущности.</param>
		void DeleteAdditionalWork(int additionalWorkId);

	}
}
