using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.BusinessLayers.Users;
using Cmas.Services.Users.Dtos.Requests;
using Cmas.Services.Users.Dtos.Responses;
using Microsoft.Extensions.Logging;
using Nancy;
using Cmas.Infrastructure.ErrorHandler;
using Cmas.BusinessLayers.Users.Entities;

namespace Cmas.Services.Users
{
    public class UsersService
    {
        private readonly UsersBusinessLayer _usersBusinessLayer;
        private readonly IMapper _autoMapper;
        private ILogger _logger;

        public UsersService(IServiceProvider serviceProvider, NancyContext ctx)
        {
            var _loggerFactory = (ILoggerFactory) serviceProvider.GetService(typeof(ILoggerFactory));

            _autoMapper = (IMapper) serviceProvider.GetService(typeof(IMapper));

            _usersBusinessLayer = new UsersBusinessLayer(serviceProvider, ctx.CurrentUser);

            _logger = _loggerFactory.CreateLogger<UsersService>();
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public async Task<IEnumerable<SimpleUserResponse>> GetUsersAsync()
        {
            var result = new List<SimpleUserResponse>();

            var users = await _usersBusinessLayer.GetUsers();

            foreach (var user in users)
            {
                var resultUser = _autoMapper.Map<SimpleUserResponse>(user);

                resultUser.isActivated = string.IsNullOrEmpty(user.actHash) && !string.IsNullOrEmpty(user.PasswordHash);
                resultUser.isActivating = !string.IsNullOrEmpty(user.actHash);

                result.Add(resultUser);
            }

            return result;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public async Task<DetailedUserResponse> GetUserAsync(string userId)
        {
            User user = await _usersBusinessLayer.GetUserById(userId);

            if (user == null)
            {
                throw new NotFoundErrorException();
            }

            var resultUser = _autoMapper.Map<DetailedUserResponse>(user);

            resultUser.isActivated = string.IsNullOrEmpty(user.actHash) && !string.IsNullOrEmpty(user.PasswordHash);
            resultUser.isActivating = !string.IsNullOrEmpty(user.actHash);

            return resultUser;
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<string> CreateUserAsync()
        {
            return await _usersBusinessLayer.CreateUser();
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        public async Task<string> UpdateUserAsync(string userId, UpdateUserRequest request)
        {
            User currentUser = await _usersBusinessLayer.GetUser(userId);

            if (currentUser == null)
            {
                throw new NotFoundErrorException();
            }

            User newUser = _autoMapper.Map<UpdateUserRequest, User>(request, currentUser);

            return await _usersBusinessLayer.UpdateUser(newUser);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<string> DeleteUserAsync(string userId)
        {
            return await _usersBusinessLayer.DeleteUser(userId);
        }
    }
}