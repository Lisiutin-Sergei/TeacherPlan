using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherPlan.Configuration;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Interface.Import;

namespace TeacherPlan.Interface
{
    /// <summary>
    /// Форма редактирования плана преподавателя.
    /// </summary>
    public partial class EditPlanForm : Form
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        private readonly int _userId;

        /// <summary>
        /// Идентификатор плана.
        /// </summary>
        private readonly int _planId;

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
		private readonly IDissertationWorkService _dissertationWorkService;
		private readonly IQualificationWorkService _qualificationWorkService;
		private readonly IPlannedWorkService _plannedWorkService;
        private readonly IImportService _importService;
		private readonly IAdditionalWorkService _additionalWorkService;
		private readonly IOtherWorkService _otherWorkService;
		private readonly IContractWorkService _contractWorkService;

		#endregion

		public EditPlanForm(
            int userId,
            int planId,
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
			IPlannedWorkService plannedWorkService,
            IImportService importService)
        {
            InitializeComponent();

            _userId = userId;
            _planId = planId;
            _importService = importService;

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

        #region Обработчики событий

        /// <summary>
        /// При загрузке формы.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                var isEdit = _planId > 0;

                if (isEdit)
                {
                    var plan = _planService.GetPlanById(_planId);

                    tbPlanName.Text = plan.Name;
                    tbPlanYear.Text = plan.PlanYear;
                    dpDateFrom.Value = plan.DateFrom;
                    dpDateTo.Value = plan.DateTo;

                    RefreshUserName(plan.UserId);

                    // Табы
                    RefreshEducationalWorkTypes();
                    RefreshEduMethodWorks();
                    RefreshBooksWritings();
                    RefreshBooksPublishings();
                    RefreshStateBudgetWorks();
                    RefreshScienceGroups();
                    RefreshStudentsResearches();
                    RefreshPublications();
                    RefreshTrainingWorks();
                    RefreshProfessionalWorks();
                    RefreshContractWorks();
                    RefreshAdditionalWorks();
                    RefreshOtherWorks();
                    RefreshPlannedWorks();
					RefreshDissertationWorks();
					RefreshQualificationWorks();
				}
                else
                {
                    tbPlanName.Text = "ИНДИВИДУАЛЬНЫЙ ПЛАН РАБОТЫ";
                    tbPlanYear.Text = "2016/2017";

                    RefreshUserName(_userId);
                }

                // Заблокировать дерево и таблицу для новых реестров
                tabControlMain.Enabled = isEdit;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сохранить план.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var plan = new Plan
                {
                    PlanId = _planId,
                    UserId = _userId,
                    Name = tbPlanName.Text,
                    PlanYear = tbPlanYear.Text,
                    DateFrom = dpDateFrom.Value,
                    DateTo = dpDateTo.Value
                };
                _planService.SavePlan(plan);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрыть форму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region УЧЕБНАЯ РАБОТА

        /// <summary>
        /// Добавить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddEducationalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvEducationalWorkTypes.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите вид учебной работы.");
                }

                var educationalWorkTypeId = (int)dgvEducationalWorkTypes.SelectedRows[0].Cells[0].Value;
                var form = IoC.Instance.Resolve<EditEducationalWorkForm>(
                   new IoC.NinjectArgument("educationalWorkTypeId", educationalWorkTypeId),
                   new IoC.NinjectArgument("educationalWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += Dgv_EducationalWorkTypes_SelectionChanged;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditEducationalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvEducationalWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебную работу.");
                }

                var educationalWorkTypeId = (int)dgvEducationalWorkTypes.SelectedRows[0].Cells[0].Value;
                var educationalWorkId = (int)dgvEducationalWorks.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditEducationalWorkForm>(
                   new IoC.NinjectArgument("educationalWorkTypeId", educationalWorkTypeId),
                   new IoC.NinjectArgument("educationalWorkId", educationalWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += Dgv_EducationalWorkTypes_SelectionChanged;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteEducationalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvEducationalWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебную работу.");
                }

                var educationalWorkId = (int)dgvEducationalWorks.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить учебную работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _educationalWorkService.DeleteEducationalWork(educationalWorkId);
                    Dgv_EducationalWorkTypes_SelectionChanged();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// При выборе типа учебной работы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_EducationalWorkTypes_SelectionChanged(object sender = null, EventArgs e = null)
        {
            try
            {
                var selectedRowsCount = dgvEducationalWorkTypes.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    RefreshEducationalWorks(0);
                    return;
                }

                var educationalWorkTypeId = (int)dgvEducationalWorkTypes.SelectedRows[0].Cells[0].Value;
                RefreshEducationalWorks(educationalWorkTypeId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список учебных работ.
        /// </summary>
        /// <param name="educationalWorkTypeId"></param>
        private void RefreshEducationalWorks(int educationalWorkTypeId)
        {
            dgvEducationalWorks.Rows.Clear();
            if (educationalWorkTypeId <= 0)
            {
                return;
            }

            var educationalWorks = _educationalWorkService
            .LoadEducationalWorksByPlan(_planId)
            .Where(e => e.EducationalWorkTypeId == educationalWorkTypeId);
            if (!(educationalWorks?.Any() ?? false))
            {
                return;
            }

            foreach (var educationalWork in educationalWorks)
            {
                dgvEducationalWorks.Rows.Add(
                    educationalWork.EducationalWorkId,
                    educationalWork.EducationalWorkTypeId,
                    educationalWork.Name,
                    educationalWork.FirstSemesterPlan,
                    educationalWork.FirstSemesterFact,
                    educationalWork.SecondSemesterPlan,
                    educationalWork.SecondSemesterFact);
            }
        }

        /// <summary>
        /// Обновить списк видов учебных работ.
        /// </summary>
        private void RefreshEducationalWorkTypes()
        {
            dgvEducationalWorkTypes.Rows.Clear();

            var educationalWorkTypes = _educationalWorkService.GetAllEducationalWorkTypes();
            if (!(educationalWorkTypes?.Any() ?? false))
            {
                return;
            }

            foreach (var educationalWorkType in educationalWorkTypes)
            {
                dgvEducationalWorkTypes.Rows.Add(educationalWorkType.EducationalWorkTypeId, educationalWorkType.Name);
            }
        }

        #endregion

        #region УЧЕБНО-МЕТОДИЧЕСКАЯ РАБОТА

        /// <summary>
        /// Добавить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddEduMethodWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditEduMethodWorkForm>(
                   new IoC.NinjectArgument("eduMethodWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshEduMethodWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditEduMethodWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvEduMethodWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебно-методическую работу.");
                }

                var eduMethodWorkId = (int)dgvEduMethodWork.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditEduMethodWorkForm>(
                   new IoC.NinjectArgument("eduMethodWorkId", eduMethodWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshEduMethodWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить учебную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteEduMethodWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvEduMethodWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебно-методическую работу.");
                }

                var eduMethodWorkId = (int)dgvEduMethodWork.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить учебно-методическую работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _eduMethodWorkService.DeleteEduMethodWork(eduMethodWorkId);
                    RefreshEduMethodWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список учебно-методических работ.
        /// </summary>
        private void RefreshEduMethodWorks(object sender = null, EventArgs e = null)
        {
            dgvEduMethodWork.Rows.Clear();

            var works = _eduMethodWorkService.LoadEduMethodWorksByPlan(_planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            foreach (var work in works)
            {
                dgvEduMethodWork.Rows.Add(
                    work.EduMethodWorkId,
                    work.Name,
                    work.FirstSemesterPlan,
                    work.FirstSemesterFact,
                    work.SecondSemesterPlan,
                    work.SecondSemesterFact);
            }
        }

        #endregion

        #region ОРГАНИЗАЦИОННО-МЕТОДИЧЕСКАЯ РАБОТА

        #region Учебники

        /// <summary>
        /// Добавить учебник.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddBookWriting_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditBookWritingForm>(
                   new IoC.NinjectArgument("bookWritingId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshBooksWritings;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить учебник.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditBookWriting_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvBooksWriting.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебник.");
                }

                var bookWritingId = (int)dgvBooksWriting.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditBookWritingForm>(
                   new IoC.NinjectArgument("bookWritingId", bookWritingId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshBooksWritings;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить учебник.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteBookWriting_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvBooksWriting.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите учебник.");
                }

                var bookWritingId = (int)dgvBooksWriting.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить учебник?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _booksWritingService.DeleteBooksWriting(bookWritingId);
                    RefreshBooksWritings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список учебников.
        /// </summary>
        private void RefreshBooksWritings(object sender = null, EventArgs e = null)
        {
            dgvBooksWriting.Rows.Clear();

            var entities = _booksWritingService.LoadBooksWritingsByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvBooksWriting.Rows.Add(
                    entity.BookWritingId,
                    entity.Name,
                    entity.FirstSemesterPlan,
                    entity.FirstSemesterFact,
                    entity.SecondSemesterPlan,
                    entity.SecondSemesterFact);
            }
        }

        #endregion Учебники

        #region Издание учебника

        /// <summary>
        /// Добавить издание учебника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddBookPublishing_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditBookPublishingForm>(
                   new IoC.NinjectArgument("bookPublishingId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshBooksPublishings;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить издание учебника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditBookPublishing_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvBooksPublishing.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите издание учебника.");
                }

                var bookPublishingId = (int)dgvBooksPublishing.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditBookPublishingForm>(
                   new IoC.NinjectArgument("bookPublishingId", bookPublishingId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshBooksPublishings;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить издание учебника.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteBookPublishing_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvBooksPublishing.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите издание учебника.");
                }

                var bookPublishingId = (int)dgvBooksPublishing.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить издание учебника?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _booksPublishingService.DeleteBooksPublishing(bookPublishingId);
                    RefreshBooksPublishings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список изданий учебников.
        /// </summary>
        private void RefreshBooksPublishings(object sender = null, EventArgs e = null)
        {
            dgvBooksPublishing.Rows.Clear();

            var entities = _booksPublishingService.LoadBooksPublishingsByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvBooksPublishing.Rows.Add(
                    entity.BooksPublishingId,
                    entity.Name,
                    entity.Output,
                    entity.Purpose,
                    entity.Coauthors,
                    entity.Volume);
            }
        }

        #endregion Издание учебника

        #endregion

        #region НАУЧНО-ИССЛЕДОВАТЕЛЬСКАЯ РАБОТА

        #region Госбюджетная работа

        /// <summary>
        /// Добавить госбюджетую работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddStateBudgetWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditStateBudgetWorkForm>(
                   new IoC.NinjectArgument("stateBudgetWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshStateBudgetWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить госбюджетую работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditStateBudgetWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvStateBudgetWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите госбюджетую работу.");
                }

                var stateBudgetWorkId = (int)dgvStateBudgetWork.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditStateBudgetWorkForm>(
                   new IoC.NinjectArgument("stateBudgetWorkId", stateBudgetWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshStateBudgetWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить госбюджетую работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteStateBudgetWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvStateBudgetWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите госбюджетую работу.");
                }

                var stateBudgetWorkId = (int)dgvStateBudgetWork.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить госбюджетую работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _stateBudgetWorkService.DeleteStateBudgetWork(stateBudgetWorkId);
                    RefreshStateBudgetWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список госбюджетных работ.
        /// </summary>
        private void RefreshStateBudgetWorks(object sender = null, EventArgs e = null)
        {
            dgvStateBudgetWork.Rows.Clear();

            var entities = _stateBudgetWorkService.LoadStateBudgetWorksByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvStateBudgetWork.Rows.Add(
                    entity.StateBudgetWorkId,
                    entity.Name,
                    entity.FirstSemesterPlan,
                    entity.FirstSemesterFact,
                    entity.SecondSemesterPlan,
                    entity.SecondSemesterFact,
                    entity.Execution);
            }
        }

        #endregion Госбюджетная работа

        #region Научные кружки, студенческие творческие группы

        /// <summary>
        /// Добавить научный кружок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddScienceGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditScienceGroupForm>(
                   new IoC.NinjectArgument("scienceGroupId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshScienceGroups;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить научный кружок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditScienceGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvScienceGroup.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите научный кружок.");
                }

                var scienceGroupId = (int)dgvScienceGroup.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditScienceGroupForm>(
                   new IoC.NinjectArgument("scienceGroupId", scienceGroupId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshScienceGroups;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить научный кружок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteScienceGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvScienceGroup.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите научный кружок.");
                }

                var scienceGroupId = (int)dgvScienceGroup.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить научный кружок?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _scienceGroupService.DeleteScienceGroup(scienceGroupId);
                    RefreshScienceGroups();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список научных кружков.
        /// </summary>
        private void RefreshScienceGroups(object sender = null, EventArgs e = null)
        {
            dgvScienceGroup.Rows.Clear();

            var entities = _scienceGroupService.LoadScienceGroupsByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvScienceGroup.Rows.Add(
                    entity.ScienceGroupId,
                    entity.Place,
                    entity.StudentsCount,
                    entity.Name,
                    entity.PublicationsCount,
                    entity.ConferencesCount,
                    entity.DiplomasCount);
            }
        }

        #endregion Госбюджетная работа

        #region Руководство научной исследовательской работой студентов

        /// <summary>
        /// Добавить руководство научной исследовательской работой студентов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddStudentsResearch_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditStudentResearchForm>(
                   new IoC.NinjectArgument("studentResearchId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshStudentsResearches;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить руководство научной исследовательской работой студентов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditStudentsResearch_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvStudentResearch.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите руководство научной исследовательской работой студентов.");
                }

                var studentResearchId = (int)dgvStudentResearch.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditStudentResearchForm>(
                   new IoC.NinjectArgument("studentResearchId", studentResearchId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshStudentsResearches;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить руководство научной исследовательской работой студентов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteStudentsResearch_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvStudentResearch.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите руководство научной исследовательской работой студентов.");
                }

                var studentResearchId = (int)dgvStudentResearch.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить руководство научной исследовательской работой студентов?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _studentResearchService.DeleteStudentResearch(studentResearchId);
                    RefreshStudentsResearches();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список руководств научной исследовательской работой студентов.
        /// </summary>
        private void RefreshStudentsResearches(object sender = null, EventArgs e = null)
        {
            dgvStudentResearch.Rows.Clear();

            var entities = _studentResearchService.LoadStudentResearchsByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvStudentResearch.Rows.Add(
                    entity.StudentResearchId,
                    entity.StudentName,
                    entity.StudentGroup,
                    entity.OopCode,
                    entity.Research,
                    entity.Execution);
            }
        }

        #endregion Госбюджетная работа

        #region Печатные (научные) труды за год 

        /// <summary>
        /// Добавить печатный труд.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddPublication_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditPublicationForm>(
                   new IoC.NinjectArgument("publicationId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshPublications;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить печатный труд.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditPublication_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvPublications.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите печатный труд.");
                }

                var publicationId = (int)dgvPublications.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditPublicationForm>(
                   new IoC.NinjectArgument("publicationId", publicationId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshPublications;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить печатный труд.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeletePublication_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvPublications.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите печатный труд.");
                }

                var publicationId = (int)dgvPublications.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить печатный труд?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _publicationService.DeletePublication(publicationId);
                    RefreshPublications();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список печатных трудов.
        /// </summary>
        private void RefreshPublications(object sender = null, EventArgs e = null)
        {
            dgvPublications.Rows.Clear();

            var entities = _publicationService.LoadPublicationsByPlan(_planId);
            if (!(entities?.Any() ?? false))
            {
                return;
            }

            foreach (var entity in entities)
            {
                dgvPublications.Rows.Add(
                    entity.PublicationId,
                    entity.Name,
                    entity.Coauthors,
                    entity.Volume,
                    entity.IsPublished ? "+" : "-");
            }
        }

        #endregion Госбюджетная работа

        #endregion

        #region ВОСПИТАТЕЛЬНАЯ и ВНЕАУДИТОРНАЯ И РАБОТА СО СТУДЕНТАМИ

        /// <summary>
        /// Добавить воспитательную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddTrainingWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditTrainingWorkForm>(
                   new IoC.NinjectArgument("trainingWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshTrainingWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить воспитательную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditTrainingWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvTrainingWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите воспитательную работу.");
                }
                
               var trainingWorkId = (int)dgvTrainingWorks.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditTrainingWorkForm>(
                   new IoC.NinjectArgument("trainingWorkId", trainingWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshTrainingWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить воспитательную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteTrainingWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvTrainingWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите воспитательную работу.");
                }

                var trainingWorkId = (int)dgvTrainingWorks.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить воспитательную работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _trainingWorkService.DeleteTrainingWork(trainingWorkId);
                    RefreshTrainingWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список воспитательных работ.
        /// </summary>
        private void RefreshTrainingWorks(object sender = null, EventArgs e = null)
        {
            dgvTrainingWorks.Rows.Clear();

            var works = _trainingWorkService.LoadTrainingWorksByPlan(_planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            foreach (var work in works)
            {
                dgvTrainingWorks.Rows.Add(
                    work.TrainingWorkId,
                    work.Name,
                    work.Date,
                    work.Execution);
            }
        }

        #endregion

        #region ПРОФОРИЕНТАЦИОННАЯ РАБОТА

        /// <summary>
        /// Добавить профориентационную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddProfessionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditProfessionalWorkForm>(
                   new IoC.NinjectArgument("professionalWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshProfessionalWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить профориентационную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditProfessionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvProfessionalWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите профориентационную работу.");
                }

                var professionalWorkId = (int)dgvProfessionalWork.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditProfessionalWorkForm>(
                   new IoC.NinjectArgument("professionalWorkId", professionalWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshProfessionalWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить профориентационную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteProfessionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvProfessionalWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите профориентационную работу.");
                }

                var professionalWorkId = (int)dgvProfessionalWork.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить профориентационную работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _professionalWorkService.DeleteProfessionalWork(professionalWorkId);
                    RefreshProfessionalWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список профориентационных работ.
        /// </summary>
        private void RefreshProfessionalWorks(object sender = null, EventArgs e = null)
        {
            dgvProfessionalWork.Rows.Clear();

            var works = _professionalWorkService.LoadProfessionalWorksByPlan(_planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            foreach (var work in works)
            {
                dgvProfessionalWork.Rows.Add(
                    work.ProfessionalWorkId,
                    work.Name,
                    work.Date,
                    work.Execution);
            }
        }

		#endregion

		#region ПОВЫШЕНИЕ ПРЕПОДАВАТЕЛЬСКОГО МАСТЕРСТВА

		#region Работа над диссертацией

		/// <summary>
		/// Добавить работу над диссертацией.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_AddDissertationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var form = IoC.Instance.Resolve<EditDissertationWorkForm>(
				   new IoC.NinjectArgument("dissertationWorkId", 0),
				   new IoC.NinjectArgument("planId", _planId));
				form.FormClosed += RefreshDissertationWorks;
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Изменить работу над диссертацией.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_EditDissertationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedRowsCount = dgvDissertationWorks.SelectedRows.Count;
				if (selectedRowsCount != 1)
				{
					throw new Exception("Выберите работу над диссертацией.");
				}

				var dissertationWorkId = (int)dgvDissertationWorks.SelectedRows[0].Cells[0].Value;

				var form = IoC.Instance.Resolve<EditDissertationWorkForm>(
				   new IoC.NinjectArgument("dissertationWorkId", dissertationWorkId),
				   new IoC.NinjectArgument("planId", _planId));
				form.FormClosed += RefreshDissertationWorks;
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Удалить работу над диссертацией.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_DeleteDissertationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedRowsCount = dgvDissertationWorks.SelectedRows.Count;
				if (selectedRowsCount != 1)
				{
					throw new Exception("Выберите работу над диссертацией.");
				}

				var dissertationWorkId = (int)dgvDissertationWorks.SelectedRows[0].Cells[0].Value;

				var confirmation = MessageBox.Show("Удалить работу над диссертацией?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (confirmation == DialogResult.OK)
				{
					_dissertationWorkService.DeleteDissertationWork(dissertationWorkId);
					RefreshDissertationWorks();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Обновить список работ над диссертацией.
		/// </summary>
		private void RefreshDissertationWorks(object sender = null, EventArgs e = null)
		{
			dgvDissertationWorks.Rows.Clear();

			var works = _dissertationWorkService.LoadDissertationWorksByPlan(_planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			foreach (var work in works)
			{
				dgvDissertationWorks.Rows.Add(
					work.DissertationWorkId,
					work.Name,
					work.Date,
					work.Execution);
			}
		}

		#endregion Работа над диссертацией

		#region Повышение квалификации, стажировка и переподготовка

		/// <summary>
		/// Добавить повышение квалификации.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_AddQualificationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var form = IoC.Instance.Resolve<EditQualificationWorkForm>(
				   new IoC.NinjectArgument("qualificationWorkId", 0),
				   new IoC.NinjectArgument("planId", _planId));
				form.FormClosed += RefreshQualificationWorks;
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Изменить повышение квалификации.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_EditQualificationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedRowsCount = dgvQualification.SelectedRows.Count;
				if (selectedRowsCount != 1)
				{
					throw new Exception("Выберите повышение квалификации.");
				}

				var qualificationWorkId = (int)dgvQualification.SelectedRows[0].Cells[0].Value;

				var form = IoC.Instance.Resolve<EditQualificationWorkForm>(
				   new IoC.NinjectArgument("qualificationWorkId", qualificationWorkId),
				   new IoC.NinjectArgument("planId", _planId));
				form.FormClosed += RefreshQualificationWorks;
				form.ShowDialog();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Удалить повышение квалификации.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_DeleteQualificationWork_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedRowsCount = dgvQualification.SelectedRows.Count;
				if (selectedRowsCount != 1)
				{
					throw new Exception("Выберите повышение квалификации.");
				}

				var qualificationWorkId = (int)dgvQualification.SelectedRows[0].Cells[0].Value;

				var confirmation = MessageBox.Show("Удалить повышение квалификации?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				if (confirmation == DialogResult.OK)
				{
					_qualificationWorkService.DeleteQualificationWork(qualificationWorkId);
					RefreshQualificationWorks();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Обновить список повышений квалификации.
		/// </summary>
		private void RefreshQualificationWorks(object sender = null, EventArgs e = null)
		{
			dgvQualification.Rows.Clear();

			var works = _qualificationWorkService.LoadQualificationWorksByPlan(_planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			foreach (var work in works)
			{
				dgvQualification.Rows.Add(
					work.QualificationWorkId,
					work.CourseType,
					work.CourseName,
					work.CourseVolume,
					work.Place,
					work.Date,
					work.Execution);
			}
		}

		#endregion Работа над диссертацией

		#endregion

		#region ПРОЧИЕ ВИДЫ РАБОТ

		#region Хоздоговорная работа

		/// <summary>
		/// Добавить хоздоговорную работу.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_AddContractWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditContractWorkForm>(
                   new IoC.NinjectArgument("contractWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshContractWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить хоздоговорную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditContractWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvContractWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите хоздоговорную работу.");
                }

                var contractWorkId = (int)dgvContractWorks.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditContractWorkForm>(
                   new IoC.NinjectArgument("contractWorkId", contractWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshContractWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить хоздоговорную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteContractWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvContractWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите хоздоговорную работу.");
                }

                var contractWorkId = (int)dgvContractWorks.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить хоздоговорную работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
					_contractWorkService.DeleteContractWork(contractWorkId);
					RefreshContractWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список хоздоговорных работ.
        /// </summary>
        private void RefreshContractWorks(object sender = null, EventArgs e = null)
        {
            dgvContractWorks.Rows.Clear();

			var works = _contractWorkService.LoadContractWorksByPlan(_planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			foreach (var work in works)
			{
				dgvContractWorks.Rows.Add(
					work.ContractWorkId,
					work.Name,
					work.Type,
					work.Volume,
					work.Duty,
					work.Execution,
					work.Comment);
			}
		}

        #endregion

        #region Дополнительная образовательная деятельность 

        /// <summary>
        /// Добавить дополнительную образовательную деятельность .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddAdditionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditAdditionalWorkForm>(
                   new IoC.NinjectArgument("additionalWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshAdditionalWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить дополнительную образовательную деятельность.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditAdditionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvAdditionalWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите дополнительную образовательную деятельность.");
                }

                var additionalWorkId = (int)dgvAdditionalWork.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditAdditionalWorkForm>(
                   new IoC.NinjectArgument("additionalWorkId", additionalWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshAdditionalWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить дополнительную образовательную деятельность.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteAdditionalWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvAdditionalWork.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите дополнительную образовательную деятельность.");
                }

                var additionalWorkId = (int)dgvAdditionalWork.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить дополнительную образовательную деятельность?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
					_additionalWorkService.DeleteAdditionalWork(additionalWorkId);
					RefreshAdditionalWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список дополнительных образовательных деятельностей.
        /// </summary>
        private void RefreshAdditionalWorks(object sender = null, EventArgs e = null)
        {
            dgvAdditionalWork.Rows.Clear();

			var works = _additionalWorkService.LoadAdditionalWorksByPlan(_planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			foreach (var work in works)
			{
				dgvAdditionalWork.Rows.Add(
					work.AdditionalWorkId,
					work.Name,
					work.Students,
					work.Place,
					work.Program,
					work.EducationType,
					work.Volume);
			}
		}

        #endregion

        #region Прочие виды работ 

        /// <summary>
        /// Добавить прочие виды работ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddOtherWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditOtherWorkForm>(
                   new IoC.NinjectArgument("otherWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshOtherWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить прочие виды работ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditOtherWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvOtherWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите прочие виды работ.");
                }

                var otherWorkId = (int)dgvOtherWorks.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditOtherWorkForm>(
                   new IoC.NinjectArgument("otherWorkId", otherWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshOtherWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить прочие виды работ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeleteOtherWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvOtherWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите прочие виды работ.");
                }

                var otherWorkId = (int)dgvOtherWorks.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить прочие виды работ?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
					_otherWorkService.DeleteOtherWork(otherWorkId);
					RefreshOtherWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список прочих видов работ.
        /// </summary>
        private void RefreshOtherWorks(object sender = null, EventArgs e = null)
        {
            dgvOtherWorks.Rows.Clear();

			var works = _otherWorkService.LoadOtherWorksByPlan(_planId);
			if (!(works?.Any() ?? false))
			{
				return;
			}

			foreach (var work in works)
			{
				dgvOtherWorks.Rows.Add(
					work.OtherWorkId,
					work.Name,
					work.Date,
					work.Execution);
			}
		}

        #endregion

        #endregion

        #region ВЫПОЛНЕНИЕ ЗАПЛАНИРОВАННОЙ РАБОТЫ 

        /// <summary>
        /// Добавить запланированную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_AddPlannedWork_Click(object sender, EventArgs e)
        {
            try
            {
                var form = IoC.Instance.Resolve<EditPlannedWorkForm>(
                   new IoC.NinjectArgument("plannedWorkId", 0),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshPlannedWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Изменить запланированную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_EditPlannedWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvPlannedWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите запланированную работу.");
                }

                var plannedWorkId = (int)dgvPlannedWorks.SelectedRows[0].Cells[0].Value;

                var form = IoC.Instance.Resolve<EditPlannedWorkForm>(
                   new IoC.NinjectArgument("plannedWorkId", plannedWorkId),
                   new IoC.NinjectArgument("planId", _planId));
                form.FormClosed += RefreshPlannedWorks;
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удалить запланированную работу.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DeletePlannedWork_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRowsCount = dgvPlannedWorks.SelectedRows.Count;
                if (selectedRowsCount != 1)
                {
                    throw new Exception("Выберите запланированную работу.");
                }

                var plannedWorkId = (int)dgvPlannedWorks.SelectedRows[0].Cells[0].Value;

                var confirmation = MessageBox.Show("Удалить запланированную работу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (confirmation == DialogResult.OK)
                {
                    _plannedWorkService.DeletePlannedWork(plannedWorkId);
                    RefreshPlannedWorks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновить список запланированных работ.
        /// </summary>
        private void RefreshPlannedWorks(object sender = null, EventArgs e = null)
        {
            dgvPlannedWorks.Rows.Clear();

            var works = _plannedWorkService.LoadPlannedWorksByPlan(_planId);
            if (!(works?.Any() ?? false))
            {
                return;
            }

            foreach (var work in works)
            {
                dgvPlannedWorks.Rows.Add(
                    work.PlannedWorkId,
                    work.Name,
                    work.FirstSemesterPlan,
                    work.FirstSemesterFact,
                    work.SecondSemesterPlan,
                    work.SecondSemesterFact);
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Обновить имя пользователя.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        private void RefreshUserName(int userId)
        {
            var user = _userService.GetUserById(userId);
            tbUserName.Text = user.Name ?? user.Login;
        }

        #endregion
    }
}
