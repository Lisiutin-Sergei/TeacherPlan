using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherPlan.Interface.Import
{
    /// <summary>
    /// Интерфейс для утилит импорта.
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Импортировать реестр.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="planId">Идентификатор реестра.</param>
        /// <param name="openAfterSave">Открыть ли после сохранения.</param>
        void ImportPlan(int userId, int planId, bool openAfterSave = true);
    }
}
