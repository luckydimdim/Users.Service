using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cmas.Infrastructure.ErrorHandler;
using Cmas.Infrastructure.Security;
using Cmas.Services.Users.Dtos.Requests;
using Cmas.Services.Users.Dtos.Responses;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;

namespace Cmas.Services.Users
{
    public class UsersModule : NancyModule
    {
        private IServiceProvider _serviceProvider;

        private UsersService usersService;

        private UsersService _usersService
        {
            get
            {
                if (usersService == null)
                    usersService = new UsersService(_serviceProvider, Context);

                return usersService;
            }
        }


        public UsersModule(IServiceProvider serviceProvider) : base("/users")
        {
            this.RequiresAuthentication();
            this.RequiresAnyRole(new[] { Role.Administrator });
            _serviceProvider = serviceProvider;


            /// <summary>
            /// Получить пользователей
            /// </summary>
            Get<IEnumerable<SimpleUserResponse>>("/", GetUsersHandlerAsync);

            /// <summary>
            /// Получить пользователя по ID
            /// </summary>
            Get<DetailedUserResponse>("/{id}", GetUserHandlerAsync);

            /// <summary>
            /// Получить пользователя по ID
            /// </summary>
            Get<bool>("/is-user-exist/{login}", GetUserExistingHandlerAsync);

            /// <summary>
            /// Создать пользователя
            /// </summary>
            Post<string>("/", CreateUserHandlerAsync);

            /// <summary>
            /// Обновить пользователя
            /// </summary>
            Put<string>("/{id}", UpdateUserHandlerAsync);

            /// <summary>
            /// Удалить пользователя
            /// </summary>
            Delete<string>("/{id}", DeleteUserHandlerAsync);
        }

        #region Обработчики

        private async Task<bool> GetUserExistingHandlerAsync(dynamic args, CancellationToken ct)
        {
            return await _usersService.IsUserExistAsync(args.login);
        }

        private async Task<IEnumerable<SimpleUserResponse>> GetUsersHandlerAsync(dynamic args,  CancellationToken ct)
        {
            return await _usersService.GetUsersAsync();
        }

        private async Task<DetailedUserResponse> GetUserHandlerAsync(dynamic args, CancellationToken ct)
        {
            return await _usersService.GetUserAsync(args.id);
        }

        private async Task<string> CreateUserHandlerAsync(dynamic args, CancellationToken ct)
        {
            var request = this.Bind<CreateUserRequest>();

            var validationResult = this.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationErrorException(validationResult.FormattedErrors);
            }

            return  await _usersService.CreateUserAsync(request);
        }

        private async Task<string> UpdateUserHandlerAsync(dynamic args, CancellationToken ct)
        {
            var request = this.Bind<UpdateUserRequest>();

            var validationResult = this.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationErrorException(validationResult.FormattedErrors);
            }

            return await _usersService.UpdateUserAsync(args.Id, request);
        }

        private async Task<string> DeleteUserHandlerAsync(dynamic args, CancellationToken ct)
        {
            return await _usersService.DeleteUserAsync(args.id);
        }

        #endregion

    }
}
