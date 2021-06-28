using AutoMapper;
using Workflow.AppSettings;
using Workflow.Interface;
using Domain.Model;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Workflow.Authentication;

namespace Workflow
{
    public partial class UserLogic : IUserLogic
    {
        private readonly IOptions<JwtSettings> jwtSettings;
        private readonly IOptions<DefaultMessage> messages;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IDataProtectionProvider dataProtectionProvider;
        private readonly IOptions<EncryptionSettings> encryptionSettings;


        public UserLogic(IUserRepository iUserRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings, IOptions<DefaultMessage> messages,
        IDataProtectionProvider dataProtectionProvider, IOptions<EncryptionSettings> encryptionSettings)
        => (userRepository, this.mapper, this.jwtSettings, this.messages, this.dataProtectionProvider, this.encryptionSettings)
        = (iUserRepository, mapper, jwtSettings, messages, dataProtectionProvider, encryptionSettings);



        /// <summary>
        /// Creates a token for valid user
        /// </summary>
        /// <param name="user">Email and Password</param>
        /// <returns></returns>
        public async Task<IActionResult> CreateTokenAsync(UserViewModel user)
        {
            var genericMessages = new List<GenericMessage>();

            var existingUser = await userRepository.GetUserByEmail(mapper.Map<User>(user));

            // if not exist 
            if (existingUser == null)
            {
                genericMessages.Add(messages.Value.LoginError);
                return new BadRequestObjectResult(new Response()
                {
                    Statuses = genericMessages
                });
            }

            var decyrptedPassword = dataProtectionProvider.CreateProtector(encryptionSettings.Value.Key).Unprotect(existingUser.Password);

            // if username password is wrong
            if (existingUser.Email != user.Email && decyrptedPassword != user.Password)
            {
                genericMessages.Add(messages.Value.LoginError);

                return new BadRequestObjectResult(new Response()
                {
                    Statuses = genericMessages
                });
            }

            genericMessages.Add(messages.Value.DefaultSuccess);

            return new OkObjectResult(new Response()
            {
                Statuses = genericMessages,
                Transaction = new UserViewModel()
                {
                    Email = existingUser.Email,
                    Token = new JWTToken(this.jwtSettings).GetToken(existingUser.UserId.ToString())
                }
            }); 
        }

        /// <summary>
        /// Create a new User 
        /// </summary>
        /// <param name="user">User Details </param>
        /// <returns></returns>
        public async Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            user.Password = dataProtectionProvider.CreateProtector(encryptionSettings.Value.Key).Protect(user.Password);
            return mapper.Map<UserViewModel>(await userRepository.AddAsync(mapper.Map<User>(user)));
        }
    }
}