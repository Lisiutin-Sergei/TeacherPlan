namespace TeacherPlan.Interface.Import
{
    /// <summary>
    /// Интерфейс для утилит импорта.
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Импортировать план.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="planId">Идентификатор плана.</param>
        /// <param name="openAfterSave">Открыть ли после сохранения.</param>
        void ImportPlan(int userId, int planId, bool openAfterSave = true);
    }
}
