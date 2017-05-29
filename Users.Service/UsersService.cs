using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Users;
using Cmas.Services.Users.Dtos.Requests;
using Cmas.Services.Users.Dtos.Responses;
using Microsoft.Extensions.Logging;
using Nancy;

namespace Cmas.Services.Users
{
    public class UsersService
    {
        private readonly UsersBusinessLayer _usersBusinessLayer;
        private readonly IMapper _autoMapper;
        private ILogger _logger;

        public UsersService(IServiceProvider serviceProvider, NancyContext ctx)
        {
           
            var _loggerFactory = (ILoggerFactory)serviceProvider.GetService(typeof(ILoggerFactory));

            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _usersBusinessLayer = new UsersBusinessLayer(serviceProvider, ctx.CurrentUser);

            _logger = _loggerFactory.CreateLogger<UsersService>();
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public async Task<IEnumerable<SimpleUserResponse>> GetUsersAsync()
        {
            var result = new List<SimpleUserResponse>();

            var users = await _usersBusinessLayer.GetUser();

            return result;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public async Task<DetailedUserResponse> GetUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<string> CreateUserAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        public async Task<string> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<string> DeleteUserAsync(string contractId)
        {
            throw new NotImplementedException();
        }
         

    }
}
