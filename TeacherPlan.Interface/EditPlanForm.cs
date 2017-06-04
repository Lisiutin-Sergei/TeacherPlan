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
            IPublicationService publicationService)
        {
            InitializeComponent();

            _userId = userId;
            _planId = planId;

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
