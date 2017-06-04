using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TeacherPlan.Core.Interface;
using TeacherPlan.Core.Interface.Service;
using TeacherPlan.Core.Model.Domain;
using TeacherPlan.Utilities.Common;
using TeacherPlan.Utilities.ExtensionMethods;

namespace TeacherPlan.Core.Service
{
    /// <summary>
    /// Сервис пользователей.
    /// </summary>
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private IUnitOfWorkFactory _unitOfWorkFactory;

        public UserService(IConfiguration configuration, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _configuration = configuration;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Загрузить список пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public List<User> LoadAllUsers()
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.UserRepository.GetAll().ToList();
            }
        }

        /// <summary>
        /// Получить список пользователей по фильтру.
        /// </summary>
        /// <param name="filter">Фильтр.</param>
        /// <returns>Список пользователей.</returns>
        public List<User> LoadUsersByFilter(Func<User, bool> filter)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.UserRepository.GetByFilter(filter).ToList();
            }
        }

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь.</returns>
        public User GetUserById(int userId)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.UserRepository.GetByID(userId);
            }
        }

        /// <summary>
        /// Получить пользователя по логину.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <returns>Пользователь.</returns>
        public User GetUserByLogin(string login)
        {
            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.UserRepository
                    .GetByFilter(e => e.Login.EqualsIgnoreCase(login))
                    .SingleOrDefault();
            }
        }

        /// <summary>
        /// Зарегистрировать пользователя.
        /// </summary>
        /// <param name="user">Новый пользователь.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Идентификатор пользователя.</returns>
        public int InsertUser(User user, string password)
        {
            Argument.NotNull(user, "Не указаны регистрационные данные пользователя.");
            Argument.NotNullOrWhiteSpace(password, "Не указан пароль пользователя.");

            user.PasswordHash = SecurePasswordHasher.Hash(password);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                return unitOfWork.UserRepository.Insert(user);
            }
        }

        /// <summary>
        /// Валидация обновления профиля пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        private void ValidateUpdateUser(User user)
        {
            Argument.NotNull(user, "Не указаны данные пользователя.");

            Argument.NotNull(user.UserId, "Не указан идентификатор пользователя.");
            Argument.NotNull(user.Name, "Не указано имя пользователя.");
            Argument.NotNull(user.Login, "Не указан логин пользователя.");
        }

        /// <summary>
        /// Обновить профиль пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        public void UpdateUser(User user)
        {
            Argument.NotNull(user, "Не указаны данные пользователя.");

            ValidateUpdateUser(user);

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                // Запомнить пароль
                var userBase = unitOfWork.UserRepository.GetByID(user.UserId);
                user.PasswordHash = userBase.PasswordHash;

                unitOfWork.UserRepository.Update(user);
            }
        }

        /// <summary>
        /// Проверить регистрационные данные пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Валидны ли регистрационные данные пользователя.</returns>
        public bool VerifyUserPassword(string login, string password)
        {
            Argument.NotNullOrWhiteSpace(login, "Не указан логин пользователя.");
            Argument.NotNullOrWhiteSpace(password, "Не указан пароль пользователя.");

            using (IUnitOfWork unitOfWork = _unitOfWorkFactory.Create(_configuration))
            {
                var user = unitOfWork.UserRepository
                    .GetByFilter(e => e.Login.EqualsIgnoreCase(login))
                    .SingleOrDefault();

                if (user == null)
                {
                    throw new Exception($"Не найден пользователь с логином {login}");
                }

                return SecurePasswordHasher.Verify(password, user.PasswordHash);
            }
        }
    }
}
