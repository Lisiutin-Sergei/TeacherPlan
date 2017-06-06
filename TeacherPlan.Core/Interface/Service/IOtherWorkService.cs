using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
	/// <summary>
	/// Интерфейс сервиса для сущности "Прочие виды работ".
	/// </summary>
	public interface IOtherWorkService
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="otherWorkId">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		OtherWork GetOtherWorkById(int otherWorkId);

		/// <summary>
		/// Получить сущности по идентификатору плана.
		/// </summary>
		/// <param name="planId">Идентификатор плана преподавателя.</param>
		/// <returns>Сущности по плану преподавателя.</returns>
		List<OtherWork> LoadOtherWorksByPlan(int planId);

		/// <summary>
		/// Сохранить сущность.
		/// </summary>
		/// <param name="otherWork">Сущность.</param>
		/// <returns>Идентификатор сущности.</returns>
		int SaveOtherWork(OtherWork otherWork);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="otherWorkId">Идентификатор сущности.</param>
		void DeleteOtherWork(int otherWorkId);

	}
}
