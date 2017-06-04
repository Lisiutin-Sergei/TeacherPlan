using Novacode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherPlan.Core.Interface.Service;

namespace TeacherPlan.Interface.Import
{
    /// <summary>
    /// Утилитка для импорта данных в word.
    /// </summary>
    public class DocxImportService : IImportService
    {
        /// <summary>
        /// Папка с шаблонами word.
        /// </summary>
        private readonly string _resourcesDirectory = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "UserDocuments");

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        private readonly Guid _userId;

        #region Сервисы

        private readonly IRegisterService _registerService;
        private readonly IPlanService _userService;

        #endregion

        public DocxImportService(
            Guid userId,
            IPlanService planService,
            IPlanService userService)
        {
            _registerService = planService;
            _userService = userService;

            _userId = userId;
        }

        /// <summary>
        /// Импортировать реестр в word документ.
        /// </summary>
        /// <param name="registerId">Идентификатор реестра.</param>
        /// <param name="openAfterSave">Открыть ли после сохранения.</param>
        public void ImportPlan(int userId, int registerId, bool openAfterSave = true)
        {
            var register = _registerService.GetRegisterById(registerId);
            var user = _userService.GetUserById(register.UserId);

            var wordFileName = user.PositionId == PositionEnum.ZK
                ? ConfigurationManager.AppSettings["ZkTemplateFile"]
                : ConfigurationManager.AppSettings["TemplateFile"];

            var wordFilePath = Path.Combine(_resourcesDirectory, wordFileName);

            using (DocX document = DocX.Load(wordFilePath))
            {
                // Обработать документ
                ProcessRegister(document, registerId);

                // Сформировать директорию сохранения и сохранить
                var resultFileDirectory = new DirectoryInfo(Path.Combine(_resourcesDirectory, user.UserId.ToString()));
                if (!resultFileDirectory.Exists)
                {
                    resultFileDirectory.Create();
                }
                var resultFilePath = Path.Combine(resultFileDirectory.ToString(), register.Name + ".docx");

                document.SaveAs(resultFilePath);

                // Открыть файл на просмотр
                if (openAfterSave)
                {
                    Process.Start(resultFilePath);
                }
            }
        }

        /// <summary>
        /// Обработка данных таблицы word'а.
        /// </summary>
        /// <param name="document">Документ word.</param>
        /// <param name="registerId">Идентификатор реестра.</param>
        private void ProcessRegister(DocX document, Guid registerId)
        {
            // Список показателей
            var register = _registerService.GetRegisterById(registerId);
            var user = _userService.GetUserById(register.UserId);
            var indicatorTypesList = _registerService.GetImportIndicatorTypes(registerId, _userId);

            var table = document.Tables.FirstOrDefault();

            // Делаем замены в файле
            document.ReplaceText("{user_name_upper}", user.Name.ToUpper());
            document.ReplaceText("{user_name_lower}", user.Name);
            document.ReplaceText("{register_year}", register.RegisterDate.Year.ToString());

            document.ReplaceText("{umr_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Umr).Points.ToString());
            document.ReplaceText("{nir_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Nir).Points.ToString());
            document.ReplaceText("{pvor_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Pvor).Points.ToString());
            document.ReplaceText("{ia_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Ia).Points.ToString());
            document.ReplaceText("{pb_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Pb).Points.ToString());
            document.ReplaceText("{zv_sum}", indicatorTypesList.FirstOrDefault(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Zvk)?.Points.ToString() ?? string.Empty);
            document.ReplaceText("{all_sum}", indicatorTypesList.Where(e => e.IndicatorType.IndicatorTypeId != IndicatorTypeEnum.Zvk).Sum(e => e.Points).ToString());
            document.ReplaceText("{all_zv_sum}", indicatorTypesList.Sum(e => e.Points).ToString());

            // В цикле по всем типам показателей
            foreach (var indicatorTypeId in IndicatorTypeEnum.GetSortedIndicatorTypes())
            {
                var currentIndicatorType = indicatorTypesList.FirstOrDefault(e =>
                    e.IndicatorType.IndicatorTypeId == indicatorTypeId);
                if (currentIndicatorType == null)
                {
                    continue;
                }

                // В цикле по всем показателям типа
                foreach (var indicator in currentIndicatorType.Indicators)
                {
                    var indicatorRowIndex = GetIndicatorRowIndex(table, indicator.Indicator.Name);
                    if (indicatorRowIndex == -1)
                    {
                        continue;
                    }

                    var indicatorRow = table.Rows[indicatorRowIndex];

                    if (indicator.Documents.Any())
                    {
                        // В цикле по всем документам показателя - хитрожопая логика
                        for (int documentIndex = 0; documentIndex < indicator.Documents.Count; documentIndex++)
                        {
                            var currentDocument = indicator.Documents[documentIndex];

                            // Выбираем предыдущую строку
                            var currentRow = indicatorRow;
                            if (documentIndex > 0)
                            {
                                var requiredIndex = indicatorRowIndex + documentIndex;
                                currentRow = table.InsertRow(table.Rows[indicatorRowIndex], requiredIndex);
                                currentRow.MinHeight = table.Rows[indicatorRowIndex].Height;
                            }

                            // Заполняем строку данными. Причем название показателя только у 1 строки.
                            FillDocumentRow(currentRow, currentDocument, documentIndex == 0 ? indicator.Indicator.Name : null);
                        }

                        // Объединить ячейки для показателя
                        if (indicator.Documents.Count > 1)
                        {
                            table.MergeCellsInColumn(0, indicatorRowIndex, indicatorRowIndex + indicator.Documents.Count - 1);
                        }
                    }
                }
            }
        }

        private int GetIndicatorRowIndex(Table table, string indicatorName)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var currentRow = table.Rows[i];
                if (currentRow.Cells.Count > 0 &&
                    currentRow.Cells.First().Paragraphs.Count > 0 &&
                    !string.IsNullOrEmpty(currentRow.Cells.First().Paragraphs.First().Text) &&
                    currentRow.Cells.First().Paragraphs.First().Text.Contains(indicatorName))
                {
                    return i;
                }
            }

            return -1;
            throw new Exception($"Не найдена строка для показателя {indicatorName}");
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="document">Документ реестра.</param>
        /// <param name="indicatorName">Название показателя.</param>
        private void FillDocumentRow(Row row, Document document, string indicatorName)
        {
            for (int i = 0; i <= 4; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }


            row.Cells[0].Paragraphs.First().Append(indicatorName).FontSize(8).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(document.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(document.FileName).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(document.Formula).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(document.Points).FontSize(11).Font(new FontFamily("Times New Roman"));
        }
    }
}
