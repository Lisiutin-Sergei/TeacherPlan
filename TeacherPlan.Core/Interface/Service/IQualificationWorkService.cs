using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
	/// <summary>
	/// Интерфейс сервиса для сущности "Повышение квалификации".
	/// </summary>
	public interface IQualificationWorkService
	{
		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="qualificationWorkId">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		QualificationWork GetQualificationWorkById(int qualificationWorkId);

		/// <summary>
		/// Получить сущности по идентификатору плана.
		/// </summary>
		/// <param name="planId">Идентификатор плана преподавателя.</param>
		/// <returns>Сущности по плану преподавателя.</returns>
		List<QualificationWork> LoadQualificationWorksByPlan(int planId);

		/// <summary>
		/// Сохранить сущность.
		/// </summary>
		/// <param name="qualificationWork">Сущность.</param>
		/// <returns>Идентификатор сущности.</returns>
		int SaveQualificationWork(QualificationWork qualificationWork);

		/// <summary>
		/// Удалить сущность.
		/// </summary>
		/// <param name="qualificationWorkId">Идентификатор сущности.</param>
		void DeleteQualificationWork(int qualificationWorkId);

	}
}
