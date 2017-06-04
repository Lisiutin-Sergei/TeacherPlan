using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherPlan.Core.Interface.Service;

namespace TeacherPlan.Interface.Import
{
    class MicrosoftImportService : IImportService
    {
        /// <summary>
        /// Папка с шаблонами word.
        /// </summary>
        private readonly string _resourcesDirectory = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "UserDocuments");

        #region Сервисы

        //private readonly IRegisterService _registerService;
        private readonly IPlanService _userService;

        #endregion

        public MicrosoftImportService(
           //IRegisterService registerService,
           IPlanService userService)
        {
            //_registerService = registerService;
            _userService = userService;
        }

        /// <summary>
        /// Импортировать реестр в word документ.
        /// </summary>
        /// <param name="registerId">Идентификатор реестра.</param>
        /// <param name="openAfterSave">Открыть ли после сохранения.</param>
        public void ImportRegister(Guid registerId, bool openAfterSave = true)
        {
            var register = _registerService.GetRegisterById(registerId);
            var user = _userService.GetUserById(register.UserId);

            var wordFileName = user.PositionId == PositionEnum.ZK
                ? ConfigurationManager.AppSettings["ZkTemplateFile"]
                : ConfigurationManager.AppSettings["TemplateFile"];
            var wordFilePath = Path.Combine(_resourcesDirectory, wordFileName);

            object saveOption = Word.WdSaveOptions.wdDoNotSaveChanges;
            object originalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;
            object routeDocument = false;
            Word.Application application = new Word.Application();
            Word.Document doc = new Word.Document();

            try
            {
                // Define an object to pass to the API for missing parameters
                object filePath = wordFilePath;
                object missing = System.Type.Missing;
                doc = application.Documents.Open(ref filePath, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing);

                // Обработать документ
                ProcessRegister(doc, registerId);

                // Сформировать директорию сохранения и сохранить
                var resultFileDirectory = new DirectoryInfo(Path.Combine(_resourcesDirectory, user.UserId.ToString()));
                if (!resultFileDirectory.Exists)
                {
                    resultFileDirectory.Create();
                }
                var resultFilePath = Path.Combine(resultFileDirectory.ToString(), register.Name + ".docx");

                doc.SaveAs(resultFilePath);

                // Открыть файл на просмотр
                if (openAfterSave)
                {
                    Process.Start(resultFilePath);
                }
            }
            finally
            {
                // Освободить ресурсы
                doc.Close(ref saveOption, ref originalFormat, ref routeDocument);
                application.Quit();
                ReleaseObject(doc);
                ReleaseObject(application);
            }
        }

        /// <summary>
        /// Обработать документ - заполнить его данные.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="registerId">Идентификатор реестра.</param>
        private void ProcessRegister(Word.Document document, Guid registerId)
        {
            var register = _registerService.GetRegisterById(registerId);
            var user = _userService.GetUserById(register.UserId);

            var indicatorTypesList = _registerService.GetImportIndicatorTypes(registerId, _userId);

            // Основная таблица
            var table = document.Tables[1];
            var dictionary = GetIndicatorRowDictionary(table);

            // Делаем замены в файле
            SearchReplace("{user_name_upper}", user.Name.ToUpper(), document);
            SearchReplace("{user_name_lower}", user.Name, document);
            SearchReplace("{register_year}", register.RegisterDate.Year.ToString(), document);

            SearchReplace("{umr_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Umr).Points.ToString(), document);
            SearchReplace("{nir_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Nir).Points.ToString(), document);
            SearchReplace("{pvor_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Pvor).Points.ToString(), document);
            SearchReplace("{ia_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Ia).Points.ToString(), document);
            SearchReplace("{pb_sum}", indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Pb).Points.ToString(), document);
            SearchReplace("{zv_sum}", indicatorTypesList.FirstOrDefault(e => e.IndicatorType.IndicatorTypeId == IndicatorTypeEnum.Zvk)?.Points.ToString() ?? string.Empty, document);
            SearchReplace("{all_sum}", indicatorTypesList.Where(e => e.IndicatorType.IndicatorTypeId != IndicatorTypeEnum.Zvk).Sum(e => e.Points).ToString(), document);
            SearchReplace("{all_zv_sum}", indicatorTypesList.Sum(e => e.Points).ToString(), document);

            // В цикле по всем типам показателей
            foreach (var indicatorTypeId in IndicatorTypeEnum.GetSortedIndicatorTypes())
            {
                var currentIndicatorType = indicatorTypesList.First(e => e.IndicatorType.IndicatorTypeId == indicatorTypeId);
                if (currentIndicatorType == null)
                {
                    continue;
                }

                // В цикле по всем показателям типа
                foreach (var indicator in currentIndicatorType.Indicators)
                {
                    var indicatorRow = dictionary
                        .FirstOrDefault(e => e.Key.Contains(indicator.Indicator.Name)).Value;

                    if (indicator.Documents.Any())
                    {
                        // В цикле по всем документам показателя - хитрожопая логика
                        for (int documentIndex = 0; documentIndex < indicator.Documents.Count; documentIndex++)
                        {
                            var currentDocument = indicator.Documents[documentIndex];
                            var isLastDocument = documentIndex == indicator.Documents.Count - 1;

                            // Выбираем предыдущую строку
                            var currentRow = indicatorRow;
                            if (documentIndex > 0)
                            {
                                // Если доукмент не первый, добавляем строку
                                object beforeRow = documentIndex == 1 ? indicatorRow : table.Rows[indicatorRow.Index - documentIndex + 1];
                                currentRow = table.Rows.Add(ref beforeRow);
                            }

                            // Заполняем строку данными. Причем название показателя только у 1 строки.
                            FillDocumentRow(currentRow, currentDocument, isLastDocument ? indicator.Indicator.Name : null);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="document">Документ реестра.</param>
        /// <param name="indicatorName">Название показателя.</param>
        private void FillDocumentRow(Word.Row row, Document document, string indicatorName)
        {
            row.Cells[1].Range.Text = indicatorName;
            row.Cells[2].Range.Text = document.Name;
            row.Cells[3].Range.Text = document.FileName;
            row.Cells[4].Range.Text = document.Formula;
            row.Cells[5].Range.Text = document.Points;
        }

        /// <summary>
        /// Получить словарь Название показателя - строка таблицы.
        /// </summary>
        /// <param name="table">Таблица.</param>
        /// <returns>Словарь Название показателя - строка таблицы.</returns>
        private Dictionary<string, Word.Row> GetIndicatorRowDictionary(Word.Table table)
        {
            var dictionary = new Dictionary<string, Word.Row>();

            for (int i = 1; i <= table.Rows.Count; i++)
            {
                var currentRow = table.Rows[i];
                if (currentRow.Cells.Count > 0)
                {
                    var currentIndicatorName = currentRow.Cells[1].Range.Text;
                    if (!string.IsNullOrWhiteSpace(currentIndicatorName) && !dictionary.ContainsKey(currentIndicatorName))
                    {
                        dictionary.Add(currentIndicatorName, currentRow);
                    }
                }
            }

            return dictionary;
        }

        [Obsolete("Так и не использовал", true)]
        private Word.Row CopyDocumentRow(Word.Table templateTable, Word.Table destinationTable, Document document)
        {
            var rowIndex = document.Formula == null ? 3 : 4;

            Word.Range range = templateTable.Range;

            // Выбрать и копировать контент у строки таблицы
            range.Start = templateTable.Rows[rowIndex].Cells[1].Range.Start;
            range.End = templateTable.Rows[rowIndex].Cells[templateTable.Rows[rowIndex].Cells.Count].Range.End;
            range.Copy();

            // Insert a new row after the last row.
            object missObj = System.Reflection.Missing.Value;
            destinationTable.Rows.Add(ref missObj);

            // Moves the cursor to the first cell of target row.
            range.Start = destinationTable.Rows[destinationTable.Rows.Count].Cells[1].Range.Start;
            range.End = destinationTable.Rows[destinationTable.Rows.Count].Cells[destinationTable.Rows[destinationTable.Rows.Count].Cells.Count].Range.End;
            range.Paste();

            return destinationTable.Rows[destinationTable.Rows.Count];
        }

        /// <summary>
        /// Найти и заменить строку в документе.
        /// </summary>
        /// <param name="textToFind">Строка для поиска.</param>
        /// <param name="textToReplace">Строка, на которую заменяем.</param>
        /// <param name="document">Документ.</param>
        private void SearchReplace(string textToFind, string textToReplace, Word.Document document)
        {
            Word.Find findObject = document.Content.Find;
            findObject.ClearFormatting();
            findObject.Text = textToFind;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = textToReplace;

            object replaceAll = Word.WdReplace.wdReplaceAll;
            object missing = System.Type.Missing;
            findObject.Execute(ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing,
                ref replaceAll, ref missing, ref missing, ref missing, ref missing);
        }

        /// <summary>
        /// Освободить ресурсы.
        /// </summary>
        /// <param name="o">Объект, держащий ресурс.</param>
        private void ReleaseObject(object obj)
        {
            if (obj == null)
            {
                return;
            }
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
        }
    }
}
