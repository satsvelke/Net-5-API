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
        public async Task<Tuple<UserViewModel, GenericMessage>> CreateTokenAsync(UserViewModel user)
        {
            var existingUser = await userRepository.GetUserByEmail(mapper.Map<User>(user));

            if (existingUser == null)
                return Tuple.Create<UserViewModel, GenericMessage>(null, messages.Value.LoginError); ;

            var decyrptedPassword = dataProtectionProvider.CreateProtector(encryptionSettings.Value.Key).Unprotect(existingUser.Password);

            if (existingUser.Email == user.Email && decyrptedPassword == user.Password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Valid", "1"),
                    new Claim("UserId", existingUser.UserId.ToString())
                };

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(jwtSettings.Value.Issuer, //Issure    
                                jwtSettings.Value.Audience,  //Audience    
                                claims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);

                var createdToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Tuple.Create<UserViewModel, GenericMessage>(new UserViewModel()
                {
                    Email = existingUser.Email,
                    Token = createdToken
                }, null);
            }

            return Tuple.Create<UserViewModel, GenericMessage>(null, messages.Value.LoginError); ;
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