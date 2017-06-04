namespace TeacherPlan.Core.Model.Domain
{
    /// <summary>
    /// Доменная модель пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// ФИО.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ФИО в родительном падеже.
        /// </summary>
        public string NameGenitive { get; set; }

        /// <summary>
        /// Ученая степень.
        /// </summary>
        public string AcademicDegree { get; set; }

        /// <summary>
        /// Ученое звание.
        /// </summary>
        public string AcademicRank { get; set; }

        /// <summary>
        /// Тип должности.
        /// </summary>
        public string PositionType { get; set; }

        /// <summary>
        /// Тип должности.
        /// </summary>
        public string PositionVolume { get; set; }

        /// <summary>
        /// Объем должности.
        /// </summary>
        public string Department { get; set; }
    }
}
