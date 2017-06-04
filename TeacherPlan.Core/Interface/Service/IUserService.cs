using System;
using System.Collections.Generic;
using TeacherPlan.Core.Model.Domain;

namespace TeacherPlan.Core.Interface.Service
{
    /// <summary>
    /// Интерфейс сервиса пользователей.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserById(int userId);

        /// <summary>
        /// Получить пользователя по логину.
        /// </summary>
        /// <param name="id">Логин пользователя.</param>
        /// <returns>Пользователь.</returns>
        User GetUserByLogin(string login);

        /// <summary>
        /// Загрузить список пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        List<User> LoadAllUsers();

        /// <summary>
        /// Получить список пользователей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список пользователей.</returns>
        List<User> LoadUsersByFilter(Func<User, bool> filter);

        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="user">Новый пользователь.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Идентификатор пользователя.</returns>
        int InsertUser(User user, string password);

        /// <summary>
        /// Обновить профиль пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Проверить регистрационные данные пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Валидны ли регистрационные данные пользователя.</returns>
        bool VerifyUserPassword(string login, string password);

    }
}
