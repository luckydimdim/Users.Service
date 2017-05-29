using System.Collections.Generic;

namespace Cmas.Services.Users.Dtos.Responses
{
    public class DetailedUserResponse
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string Id;

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login;

        /// <summary>
        /// Наименование пользователя
        /// </summary>
        public string Name;

        /// <summary>
        /// true - пользователь активирован
        /// </summary>
        public bool isActivated;

        /// <summary>
        /// true - выслали ссылку, пока не активировался
        /// </summary>
        public bool isActivating;

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public IList<string> Roles;
    }
}
