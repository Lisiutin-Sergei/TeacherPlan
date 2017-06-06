using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
	/// <summary>
	/// Интерфейс сервиса для сущности "Работа над диссертацией".
	/// </summary>
	public interface IDissertationWorkService
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="dissertationWorkId">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		DissertationWork GetDissertationWorkById(int dissertationWorkId);

		/// <summary>
		/// Получить сущности по идентификатору плана.
		/// </summary>
		/// <param name="planId">Идентификатор плана преподавателя.</param>
		/// <returns>Сущности по плану преподавателя.</returns>
		List<DissertationWork> LoadDissertationWorksByPlan(int planId);

		/// <summary>
		/// Сохранить сущность.
		/// </summary>
		/// <param name="dissertationWork">Сущность.</param>
		/// <returns>Идентификатор сущности.</returns>
		int SaveDissertationWork(DissertationWork dissertationWork);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="dissertationWorkId">Идентификатор сущности.</param>
		void DeleteDissertationWork(int dissertationWorkId);

	}
}
