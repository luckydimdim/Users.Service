using Cmas.Infrastructure.Security;
using System.Collections.Generic;

namespace Cmas.Services.Users.Dtos.Requests
{
    public class CreateUserRequest
    {
      
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name;
         
        /// <summary>
        /// Роли
        /// </summary>
        public IEnumerable<Role> Roles;
         
    }
}
