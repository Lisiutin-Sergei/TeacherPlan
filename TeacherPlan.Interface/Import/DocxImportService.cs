using Novacode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;

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

        #region Сервисы

        private readonly IUserService _userService;
        private readonly IPlanService _planService;
        private readonly IEducationalWorkService _educationalWorkService;
        private readonly IEduMethodWorkService _eduMethodWorkService;
        private readonly IBookWritingService _booksWritingService;
        private readonly IBooksPublishingService _booksPublishingService;
        private readonly IStateBudgetWorkService _stateBudgetWorkService;
        private readonly IScienceGroupService _scienceGroupService;
        private readonly IStudentResearchService _studentResearchService;
        private readonly IPublicationService _publicationService;
        private readonly ITrainingWorkService _trainingWorkService;
        private readonly IProfessionalWorkService _professionalWorkService;
        private readonly IPlannedWorkService _plannedWorkService;
		private readonly IDissertationWorkService _dissertationWorkService;
		private readonly IQualificationWorkService _qualificationWorkService;
		private readonly IAdditionalWorkService _additionalWorkService;
		private readonly IOtherWorkService _otherWorkService;
		private readonly IContractWorkService _contractWorkService;

		#endregion

		public DocxImportService(
            IUserService userService,
            IPlanService planService,
            IEducationalWorkService educationalWorkService,
            IEduMethodWorkService eduMethodWorkService,
            IBookWritingService booksWritingService,
            IBooksPublishingService booksPublishingService,
            IStateBudgetWorkService stateBudgetWorkService,
            IScienceGroupService scienceGroupService,
            IStudentResearchService studentResearchService,
            IPublicationService publicationService,
            ITrainingWorkService trainingWorkService,
            IProfessionalWorkService professionalWorkService,
			IDissertationWorkService dissertationWorkService,
			IQualificationWorkService qualificationWorkService,
			IAdditionalWorkService additionalWorkService,
			IOtherWorkService otherWorkService,
			IContractWorkService contractWorkService,
			IPlannedWorkService plannedWorkService)
        {
            _userService = userService;
            _planService = planService;
            _educationalWorkService = educationalWorkService;
            _eduMethodWorkService = eduMethodWorkService;
            _booksWritingService = booksWritingService;
            _booksPublishingService = booksPublishingService;
            _stateBudgetWorkService = stateBudgetWorkService;
            _scienceGroupService = scienceGroupService;
            _studentResearchService = studentResearchService;
            _publicationService = publicationService;
            _trainingWorkService = trainingWorkService;
            _professionalWorkService = professionalWorkService;
            _plannedWorkService = plannedWorkService;
			_dissertationWorkService = dissertationWorkService;
			_qualificationWorkService = qualificationWorkService;
			_additionalWorkService = additionalWorkService;
			_otherWorkService = otherWorkService;
			_contractWorkService = contractWorkService;
		}

        /// <summary>
        /// Импортировать план в word документ.
        /// </summary>
        /// <param name="planId">Идентификатор плана.</param>
        /// <param name="openAfterSave">Открыть ли после сохранения.</param>
        public void ImportPlan(int userId, int planId, bool openAfterSave = true)
        {
            var plan = _planService.GetPlanById(planId);
            var user = _userService.GetUserById(plan.UserId);

            var wordFileName = ConfigurationManager.AppSettings["TemplateFile"];
            var wordFilePath = Path.Combine(_resourcesDirectory, wordFileName);

            using (DocX document = DocX.Load(wordFilePath))
            {
                ProcessPlan(document, planId, userId);
                
                ProcessEducationalWork(document, planId);
                ProcessEducationalMethodicWork(document, planId);
                ProcessBookWriting(document, planId);
                ProcessBookPublishing(document, planId);
                ProcessStateBudgetWork(document, planId);
                ProcessScienceGroup(document, planId);
                ProcessStudentsResearch(document, planId);
                ProcessPublication(document, planId);
                ProcessTrainingWork(document, planId);
                ProcessProfessionalWork(document, planId);
				ProcessDissertationWork(document, planId);
				ProcessQualificationWork(document, planId);
				ProcessAdditionalWork(document, planId);
				ProcessContractWork(document, planId);
				ProcessOtherWork(document, planId);
				ProcessPlannedWork(document, planId);

                // Сформировать директорию сохранения и сохранить
                var resultFileDirectory = new DirectoryInfo(Path.Combine(_resourcesDirectory, user.UserId.ToString()));
                if (!resultFileDirectory.Exists)
                {
                    resultFileDirectory.Create();
                }
                var resultFilePath = Path.Combine(resultFileDirectory.ToString(), plan.Name + ".docx");

                document.SaveAs(resultFilePath);

                // Открыть файл на просмотр
                if (openAfterSave)
                {
                    Process.Start(resultFilePath);
                }
            }
        }

        /// <summary>
        /// Обработать шапку плана.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        /// <param name="userId">Идентификатор пользака.</param>
        private void ProcessPlan(DocX document, int planId, int userId)
        {
            var plan = _planService.GetPlanById(planId);
            var user = _userService.GetUserById(plan.UserId);

            document.ReplaceText("{year}", DateTime.Now.Year.ToString() ?? string.Empty);
            document.ReplaceText("{plan_year}", plan.PlanYear?.ToString() ?? string.Empty);
            document.ReplaceText("{plan_date_from}", plan.DateFrom.ToString("dd.MM.yyyy") ?? string.Empty);
            document.ReplaceText("{plan_date_to}", plan.DateTo.ToString("dd.MM.yyyy") ?? string.Empty);

            document.ReplaceText("{user_name_upper}", user.Name?.ToUpper() ?? string.Empty);
            document.ReplaceText("{user_name_lower}", user.Name ?? string.Empty);
            document.ReplaceText("{user_position}", user.Position?.ToString() ?? string.Empty);
            document.ReplaceText("{user_academic_degree}", user.AcademicDegree?.ToString() ?? string.Empty);
            document.ReplaceText("{user_academic_rank}", user.AcademicRank?.ToString() ?? string.Empty);
            document.ReplaceText("{user_position_type}", user.PositionType?.ToString() ?? string.Empty);
            document.ReplaceText("{user_position_volume}", user.PositionVolume?.ToString() ?? string.Empty);
            document.ReplaceText("{user_department}", user.Department?.ToString() ?? string.Empty);
        }

        #region УЧЕБНАЯ РАБОТА

        /// <summary>
        /// Обработать учебную работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessEducationalWork(DocX document, int planId)
        {
            var table = document.Tables[2];

            var educationalWorks = _educationalWorkService.LoadEducationalWorksByPlan(planId);
            var educationalWorkTypes = _educationalWorkService.GetAllEducationalWorkTypes();

            // В цикле по всем типам работ
            foreach (var educationalWorkType in educationalWorkTypes)
            {
                var rowIndex = GetWorkRowIndex(table, educationalWorkType.Name);
                if (rowIndex == -1)
                {
                    continue;
                }

                var workTypeRow = table.Rows[rowIndex];

                var currentWorks = educationalWorks
                    .Where(e => e.EducationalWorkTypeId == educationalWorkType.EducationalWorkTypeId)
                    .ToList();

                if (!(currentWorks?.Any() ?? false))
                {
                    continue;
                }

                // В цикле по всем работам
                for (int workIndex = 0; workIndex < currentWorks.Count; workIndex++)
                {
                    var currentWork = currentWorks[workIndex];

                    // Выбираем предыдущую строку
                    var requiredIndex = rowIndex + workIndex + 1;
                    var currentRow = table.InsertRow(table.Rows[rowIndex], requiredIndex);
                    //currentRow.MinHeight = table.Rows[rowIndex].Height;

                    // Заполняем строку данными. Причем название показателя только у 1 строки.
                    FillEducationalWorkRow(currentRow, currentWork);
                }

                FillEducationalWorkAll(workTypeRow, currentWorks);
            }
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Документ реестра.</param>
        /// <param name="workTypeName">Название показателя.</param>
        private void FillEducationalWorkRow(Row row, EducationalWork work)
        {
            for (int i = 0; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.FirstSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.FirstSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.SecondSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.SecondSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));

            string allPlan = work.FirstSemesterPlan > 0 && work.SecondSemesterPlan > 0
                ? (work.FirstSemesterPlan + work.SecondSemesterPlan).ToString()
                : string.Empty;
            string allFact = work.FirstSemesterFact > 0 && work.SecondSemesterFact > 0
                ? (work.FirstSemesterFact + work.SecondSemesterFact).ToString()
                : string.Empty;

            row.Cells[5].Paragraphs.First().Append(allPlan)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(allFact)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillEducationalWorkAll(Row row, List<EducationalWork> works)
        {
            for (int i = 1; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[1].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterPlan ?? 0) + (e.SecondSemesterPlan ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterFact ?? 0) + (e.SecondSemesterFact ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessEducationalMethodicWork(DocX document, int planId)
        {
            var table = document.Tables[3];
            var startEmptyIndex = 2;
            var emptyLinesCount = 5;

            var works = _eduMethodWorkService.LoadEduMethodWorksByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];
                //currentRow.MinHeight = table.Rows[rowIndex].Height;

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, EduMethodWork work)
        {
            for (int i = 0; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.FirstSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.FirstSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.SecondSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.SecondSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));

            string allPlan = work.FirstSemesterPlan > 0 && work.SecondSemesterPlan > 0
                ? (work.FirstSemesterPlan + work.SecondSemesterPlan).ToString()
                : string.Empty;
            string allFact = work.FirstSemesterFact > 0 && work.SecondSemesterFact > 0
                ? (work.FirstSemesterFact + work.SecondSemesterFact).ToString()
                : string.Empty;

            row.Cells[5].Paragraphs.First().Append(allPlan)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(allFact)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<EduMethodWork> works)
        {
            for (int i = 1; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[1].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterPlan ?? 0) + (e.SecondSemesterPlan ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterFact ?? 0) + (e.SecondSemesterFact ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region ОРГАНИЗАЦИОННО-МЕТОДИЧЕСКАЯ РАБОТА

        #region Учебники

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessBookWriting(DocX document, int planId)
        {
            var table = document.Tables[4];
            var startEmptyIndex = 2;
            var emptyLinesCount = 5;

            var works = _booksWritingService.LoadBooksWritingsByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 2], table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, BookWriting work)
        {
            for (int i = 0; i <= 4; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.FirstSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.FirstSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.SecondSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.SecondSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, Row lastRow, List<BookWriting> works)
        {
            ClearRow(row, 1, 4);
            ClearRow(lastRow, 1, 1);

            row.Cells[1].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));

            lastRow.Cells[1].Paragraphs.First()
                .Append(works
                    .Sum(e => (e.FirstSemesterPlan ?? 0) + (e.FirstSemesterFact ?? 0) + (e.SecondSemesterPlan ?? 0) + (e.SecondSemesterFact ?? 0))
                    .ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region Издание учебника

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessBookPublishing(DocX document, int planId)
        {
            var table = document.Tables[5];
            var startEmptyIndex = 1;
            var emptyLinesCount = 6;

            var works = _booksPublishingService.LoadBooksPublishingsByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем следующую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными.
                FillWorkRow(currentRow, currentWork);
            }
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, BooksPublishing work)
        {
            ClearRow(row, 0, 4);

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.Output ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.Purpose ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.Coauthors ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.Volume?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #endregion

        #region НАУЧНО-ИССЛЕДОВАТЕЛЬСКАЯ РАБОТА

        #region Госбюджетная работа

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessStateBudgetWork(DocX document, int planId)
        {
            var table = document.Tables[6];
            var startEmptyIndex = 2;
            var emptyLinesCount = 5;

            var works = _stateBudgetWorkService.LoadStateBudgetWorksByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, StateBudgetWork work)
        {
            ClearRow(row, 0, 5);

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.FirstSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.FirstSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.SecondSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.SecondSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(work.Execution ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<StateBudgetWork> works)
        {
            ClearRow(row, 1, 1);

            row.Cells[1].Paragraphs.First()
                .Append(works
                    .Sum(e => (e.FirstSemesterPlan ?? 0) + (e.FirstSemesterFact ?? 0) + (e.SecondSemesterPlan ?? 0) + (e.SecondSemesterFact ?? 0))
                    .ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region Научные кружки, студенческие творческие группы

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessScienceGroup(DocX document, int planId)
        {
            var table = document.Tables[7];
            var startEmptyIndex = 2;
            var emptyLinesCount = 2;

            var works = _scienceGroupService.LoadScienceGroupsByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем следующую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 2], table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, ScienceGroup work)
        {
            ClearRow(row, 0, 5);

            row.Cells[0].Paragraphs.First().Append(work.Place ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.StudentsCount?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.Name ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.PublicationsCount?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.ConferencesCount?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(work.DiplomasCount?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, Row lastRow, List<ScienceGroup> works)
        {
            ClearRow(row, 1, 5);
            ClearRow(lastRow, 1, 1);

            row.Cells[1].Paragraphs.First().Append(works.Sum(e => e.StudentsCount ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            //row.Cells[2].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterFact ?? 0).ToString() ?? string.Empty)
            //    .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(works.Sum(e => e.PublicationsCount ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(works.Sum(e => e.ConferencesCount ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(works.Sum(e => e.DiplomasCount ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));

            //lastRow.Cells[1].Paragraphs.First()
            //    .Append("всего: " + works
            //        .Sum(e => (e.FirstSemesterPlan ?? 0) + (e.FirstSemesterFact ?? 0) + (e.SecondSemesterPlan ?? 0) + (e.SecondSemesterFact ?? 0))
            //        .ToString() ?? string.Empty)
            //    .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region Руководство научной исследовательской работой студентов

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessStudentsResearch(DocX document, int planId)
        {
            var table = document.Tables[8];
            var startEmptyIndex = 1;
            var emptyLinesCount = 4;

            var works = _studentResearchService.LoadStudentResearchsByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, StudentResearch work)
        {
            ClearRow(row, 0, 4);

            row.Cells[0].Paragraphs.First().Append(work.StudentName ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.StudentGroup ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.OopCode ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.Research ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.Execution ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<StudentResearch> works)
        {
            ClearRow(row, 1, 1);

            //row.Cells[1].Paragraphs.First()
            //    .Append(works
            //        .Sum(e => (e.Hou ?? 0) + (e.FirstSemesterFact ?? 0) + (e.SecondSemesterPlan ?? 0) + (e.SecondSemesterFact ?? 0))
            //        .ToString() ?? string.Empty)
            //    .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region Печатные (научные) труды за год

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessPublication(DocX document, int planId)
        {
            var table = document.Tables[9];
            var startEmptyIndex = 2;
            var emptyLinesCount = 5;

            var works = _publicationService.LoadPublicationsByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            var nonpublishedWorks = works.Where(e => !e.IsPublished).ToList();
            var publishedWorks = works.Where(e => e.IsPublished).ToList();

            for (int workIndex = 0; workIndex < nonpublishedWorks.Count; workIndex++)
            {
                var currentWork = nonpublishedWorks[workIndex];

                // Выбираем следующую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными.
                FillWorkRow(currentRow, currentWork);
            }

            var publishedText = "Вышло из печати";
            var publishedFirstIndex = GetWorkRowIndex(table, publishedText) + 1;

            for (int workIndex = 0; workIndex < publishedWorks.Count; workIndex++)
            {
                var currentWork = publishedWorks[workIndex];

                // Выбираем следующую строку
                var requiredIndex = workIndex + publishedFirstIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[publishedFirstIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными.
                FillWorkRow(currentRow, currentWork);
            }
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, Publication work)
        {
            ClearRow(row, 0, 2);

            row.Cells[0].Paragraphs.First().Append(work.Name ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.Coauthors ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.Volume?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #endregion

        #region ВОСПИТАТЕЛЬНАЯ и ВНЕАУДИТОРНАЯ И РАБОТА СО СТУДЕНТАМИ

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessTrainingWork(DocX document, int planId)
        {
            var table = document.Tables[10];
            var startEmptyIndex = 1;
            var emptyLinesCount = 4;

            var works = _trainingWorkService.LoadTrainingWorksByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, TrainingWork work)
        {
            ClearRow(row, 0, 2);

            row.Cells[0].Paragraphs.First().Append(work.Name ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.Date ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.Execution ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<TrainingWork> works)
        {
            ClearRow(row, 1, 1);

            row.Cells[1].Paragraphs.First()
                .Append(works
                    .Sum(e => e.Hours ?? 0)
                    .ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        #region ПРОФОРИЕНТАЦИОННАЯ РАБОТА

        /// <summary>
        /// Обработать работу.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="planId">Идентификатор плана.</param>
        private void ProcessProfessionalWork(DocX document, int planId)
        {
            var table = document.Tables[11];
            var startEmptyIndex = 1;
            var emptyLinesCount = 4;

            var works = _professionalWorkService.LoadProfessionalWorksByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, ProfessionalWork work)
        {
            ClearRow(row, 0, 2);

            row.Cells[0].Paragraphs.First().Append(work.Name ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.Date ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.Execution ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<ProfessionalWork> works)
        {
            ClearRow(row, 1, 1);

            row.Cells[1].Paragraphs.First()
                .Append(works
                    .Sum(e => e.Hours ?? 0)
                    .ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

		#endregion

		#region ПОВЫШЕНИЕ ПРЕПОДАВАТЕЛЬСКОГО МАСТЕРСТВА

		#region Работа над диссертацией

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessDissertationWork(DocX document, int planId)
		{
			var table = document.Tables[12];
			var startEmptyIndex = 1;
			var emptyLinesCount = 4;

			var works = _dissertationWorkService.LoadDissertationWorksByPlan(planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			for (int workIndex = 0; workIndex < works.Count; workIndex++)
			{
				var currentWork = works[workIndex];

				// Выбираем предыдущую строку
				var requiredIndex = workIndex + startEmptyIndex;
				var currentRow = workIndex > emptyLinesCount
					? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
					: table.Rows[requiredIndex];

				// Заполняем строку данными. Причем название показателя только у 1 строки.
				FillWorkRow(currentRow, currentWork);
			}

			FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
		}

		/// <summary>
		/// Заполнить строку таблицы.
		/// </summary>
		/// <param name="row">Строка таблицы.</param>
		/// <param name="work">Сущность.</param>
		private void FillWorkRow(Row row, DissertationWork work)
		{
			ClearRow(row, 0, 2);

			row.Cells[0].Paragraphs.First().Append(work.Name ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[1].Paragraphs.First().Append(work.Date ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[2].Paragraphs.First().Append(work.Execution ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		/// <summary>
		/// Заполнить сумму строки.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="works"></param>
		private void FillWorkAllRow(Row row, List<DissertationWork> works)
		{
			ClearRow(row, 1, 1);

			row.Cells[1].Paragraphs.First()
				.Append(works
					.Sum(e => e.Hours ?? 0)
					.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		#endregion

		#region Повышение квалификации

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessQualificationWork(DocX document, int planId)
		{
			var table = document.Tables[13];
			var startEmptyIndex = 1;
			var emptyLinesCount = 2;

			var works = _qualificationWorkService.LoadQualificationWorksByPlan(planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			for (int workIndex = 0; workIndex < works.Count; workIndex++)
			{
				var currentWork = works[workIndex];

				// Выбираем предыдущую строку
				var requiredIndex = workIndex + startEmptyIndex;
				var currentRow = workIndex > emptyLinesCount
					? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
					: table.Rows[requiredIndex];

				// Заполняем строку данными. Причем название показателя только у 1 строки.
				FillWorkRow(currentRow, currentWork, workIndex + 1);
			}

			FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
		}

		/// <summary>
		/// Заполнить строку таблицы.
		/// </summary>
		/// <param name="row">Строка таблицы.</param>
		/// <param name="work">Сущность.</param>
		private void FillWorkRow(Row row, QualificationWork work, int counter)
		{
			ClearRow(row, 0, 6);

			row.Cells[0].Paragraphs.First().Append(counter.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[1].Paragraphs.First().Append(work.CourseType ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[2].Paragraphs.First().Append(work.CourseName ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[3].Paragraphs.First().Append(work.CourseVolume?.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[4].Paragraphs.First().Append(work.Place ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[5].Paragraphs.First().Append(work.Date ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[6].Paragraphs.First().Append(work.Execution ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		/// <summary>
		/// Заполнить сумму строки.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="works"></param>
		private void FillWorkAllRow(Row row, List<QualificationWork> works)
		{
			ClearRow(row, 2, 2);

			row.Cells[2].Paragraphs.First()
				.Append(works
					.Sum(e => e.CourseVolume ?? 0)
					.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		#endregion

		#endregion

		#region ПРОЧИЕ ВИДЫ РАБОТ

		#region Хоздоговорная работа

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessContractWork(DocX document, int planId)
		{
			var table = document.Tables[14];
			var startEmptyIndex = 1;
			var emptyLinesCount = 7;

			var works = _contractWorkService.LoadContractWorksByPlan(planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			for (int workIndex = 0; workIndex < works.Count; workIndex++)
			{
				var currentWork = works[workIndex];

				// Выбираем предыдущую строку
				var requiredIndex = workIndex + startEmptyIndex;
				var currentRow = workIndex > emptyLinesCount
					? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
					: table.Rows[requiredIndex];

				// Заполняем строку данными. Причем название показателя только у 1 строки.
				FillWorkRow(currentRow, currentWork, workIndex + 1);
			}
		}

		/// <summary>
		/// Заполнить строку таблицы.
		/// </summary>
		/// <param name="row">Строка таблицы.</param>
		/// <param name="work">Сущность.</param>
		private void FillWorkRow(Row row, ContractWork work, int counter)
		{
			ClearRow(row, 0, 6);

			row.Cells[0].Paragraphs.First().Append(counter.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[1].Paragraphs.First().Append(work.Name ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[2].Paragraphs.First().Append(work.Type ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[3].Paragraphs.First().Append(work.Volume?.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[4].Paragraphs.First().Append(work.Duty ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[5].Paragraphs.First().Append(work.Execution ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[6].Paragraphs.First().Append(work.Comment ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		#endregion

		#region Дополнительная образовательная деятельность

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessAdditionalWork(DocX document, int planId)
		{
			var table = document.Tables[15];
			var startEmptyIndex = 1;
			var emptyLinesCount = 6;

			var works = _additionalWorkService.LoadAdditionalWorksByPlan(planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			for (int workIndex = 0; workIndex < works.Count; workIndex++)
			{
				var currentWork = works[workIndex];

				// Выбираем предыдущую строку
				var requiredIndex = workIndex + startEmptyIndex;
				var currentRow = workIndex > emptyLinesCount
					? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
					: table.Rows[requiredIndex];

				// Заполняем строку данными. Причем название показателя только у 1 строки.
				FillWorkRow(currentRow, currentWork, workIndex + 1);
			}
		}

		/// <summary>
		/// Заполнить строку таблицы.
		/// </summary>
		/// <param name="row">Строка таблицы.</param>
		/// <param name="work">Сущность.</param>
		private void FillWorkRow(Row row, AdditionalWork work, int counter)
		{
			ClearRow(row, 0, 6);

			row.Cells[0].Paragraphs.First().Append(counter.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[1].Paragraphs.First().Append(work.Name ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[2].Paragraphs.First().Append(work.Students ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[3].Paragraphs.First().Append(work.Place ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[4].Paragraphs.First().Append(work.Program ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[5].Paragraphs.First().Append(work.EducationType ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[6].Paragraphs.First().Append(work.Volume?.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		#endregion

		#region Выполнение общественных поручений, прочие виды работ 

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessOtherWork(DocX document, int planId)
		{
			var table = document.Tables[16];
			var startEmptyIndex = 1;
			var emptyLinesCount = 5;

			var works = _otherWorkService.LoadOtherWorksByPlan(planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			for (int workIndex = 0; workIndex < works.Count; workIndex++)
			{
				var currentWork = works[workIndex];

				// Выбираем предыдущую строку
				var requiredIndex = workIndex + startEmptyIndex;
				var currentRow = workIndex > emptyLinesCount
					? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
					: table.Rows[requiredIndex];

				// Заполняем строку данными. Причем название показателя только у 1 строки.
				FillWorkRow(currentRow, currentWork, workIndex + 1);
			}
		}

		/// <summary>
		/// Заполнить строку таблицы.
		/// </summary>
		/// <param name="row">Строка таблицы.</param>
		/// <param name="work">Сущность.</param>
		private void FillWorkRow(Row row, OtherWork work, int counter)
		{
			ClearRow(row, 0, 3);

			row.Cells[0].Paragraphs.First().Append(counter.ToString() ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[1].Paragraphs.First().Append(work.Name ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[2].Paragraphs.First().Append(work.Date ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
			row.Cells[3].Paragraphs.First().Append(work.Execution ?? string.Empty)
				.FontSize(11).Font(new FontFamily("Times New Roman"));
		}

		#endregion

		#endregion

		#region ВЫПОЛНЕНИЕ ЗАПЛАНИРОВАННОЙ РАБОТЫ

		/// <summary>
		/// Обработать работу.
		/// </summary>
		/// <param name="document">Документ.</param>
		/// <param name="planId">Идентификатор плана.</param>
		private void ProcessPlannedWork(DocX document, int planId)
        {
            var table = document.Tables[17];
            var startEmptyIndex = 2;
            var emptyLinesCount = 8;

            var works = _plannedWorkService.LoadPlannedWorksByPlan(planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            for (int workIndex = 0; workIndex < works.Count; workIndex++)
            {
                var currentWork = works[workIndex];

                // Выбираем предыдущую строку
                var requiredIndex = workIndex + startEmptyIndex;
                var currentRow = workIndex > emptyLinesCount
                    ? table.InsertRow(table.Rows[startEmptyIndex], requiredIndex)
                    : table.Rows[requiredIndex];

                // Заполняем строку данными. Причем название показателя только у 1 строки.
                FillWorkRow(currentRow, currentWork);
            }

            FillWorkAllRow(table.Rows[table.Rows.Count - 1], works);
        }

        /// <summary>
        /// Заполнить строку таблицы.
        /// </summary>
        /// <param name="row">Строка таблицы.</param>
        /// <param name="work">Сущность.</param>
        private void FillWorkRow(Row row, PlannedWork work)
        {
            for (int i = 0; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[0].Paragraphs.First().Append(work.Name).FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[1].Paragraphs.First().Append(work.FirstSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(work.FirstSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(work.SecondSemesterPlan?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(work.SecondSemesterFact?.ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));

            string allPlan = work.FirstSemesterPlan > 0 && work.SecondSemesterPlan > 0
                ? (work.FirstSemesterPlan + work.SecondSemesterPlan).ToString()
                : string.Empty;
            string allFact = work.FirstSemesterFact > 0 && work.SecondSemesterFact > 0
                ? (work.FirstSemesterFact + work.SecondSemesterFact).ToString()
                : string.Empty;

            row.Cells[5].Paragraphs.First().Append(allPlan)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(allFact)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        /// <summary>
        /// Заполнить сумму строки.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="works"></param>
        private void FillWorkAllRow(Row row, List<PlannedWork> works)
        {
            for (int i = 1; i <= 6; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }

            row.Cells[1].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[2].Paragraphs.First().Append(works.Sum(e => e.FirstSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[3].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterPlan ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[4].Paragraphs.First().Append(works.Sum(e => e.SecondSemesterFact ?? 0).ToString() ?? string.Empty)
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[5].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterPlan ?? 0) + (e.SecondSemesterPlan ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
            row.Cells[6].Paragraphs.First().Append(works.Sum(e => (e.FirstSemesterFact ?? 0) + (e.SecondSemesterFact ?? 0)).ToString())
                .FontSize(11).Font(new FontFamily("Times New Roman"));
        }

        #endregion

        /// <summary>
        /// Найти строку с работой.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="workName"></param>
        /// <returns></returns>
        private int GetWorkRowIndex(Table table, string workName)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var currentRow = table.Rows[i];
                if (currentRow.Cells.Count > 0 &&
                    currentRow.Cells.First().Paragraphs.Count > 0 &&
                    !string.IsNullOrEmpty(currentRow.Cells.First().Paragraphs.First().Text) &&
                    currentRow.Cells.First().Paragraphs.First().Text.Contains(workName))
                {
                    return i;
                }
            }

            throw new Exception($"Не найдена строка для показателя {workName}");
        }

        /// <summary>
        /// Очистить строку.
        /// </summary>
        /// <param name="row">Строка.</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void ClearRow(Row row, int startIndex = 0, int endIndex = 1)
        {
            for (int i = startIndex; i <= endIndex; i++)
            {
                var paragraph = row.Cells[i].Paragraphs.First();
                if (!string.IsNullOrEmpty(paragraph.Text))
                {
                    paragraph.RemoveText(0);
                }
            }
        }
    }
}
