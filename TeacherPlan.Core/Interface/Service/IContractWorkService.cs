using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
	/// <summary>
	/// Интерфейс сервиса для сущности "Хоздоговорная работа".
	/// </summary>
	public interface IContractWorkService
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="contractWorkId">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		ContractWork GetContractWorkById(int contractWorkId);

		/// <summary>
		/// Получить сущности по идентификатору плана.
		/// </summary>
		/// <param name="planId">Идентификатор плана преподавателя.</param>
		/// <returns>Сущности по плану преподавателя.</returns>
		List<ContractWork> LoadContractWorksByPlan(int planId);

		/// <summary>
		/// Сохранить сущность.
		/// </summary>
		/// <param name="contractWork">Сущность.</param>
		/// <returns>Идентификатор сущности.</returns>
		int SaveContractWork(ContractWork contractWork);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="contractWorkId">Идентификатор сущности.</param>
		void DeleteContractWork(int contractWorkId);

	}
}
