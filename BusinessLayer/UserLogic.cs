using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.AppSettings;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using Persistence.Interface;
using ViewModel;

namespace BusinessLayer
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
        => (this.userRepository, this.mapper, this.jwtSettings, this.messages, this.dataProtectionProvider, this.encryptionSettings)
        = (iUserRepository, mapper, jwtSettings, messages, dataProtectionProvider, encryptionSettings);



        /// <summary>
        /// Creates a token for valid user
        /// </summary>
        /// <param name="user">Email and Password</param>
        /// <returns></returns>
        public async Task<Tuple<UserViewModel, ErrorMessage>> CreateTokenAsync(UserViewModel user)
        {
            var existingUser = await this.userRepository.GetUserByEmail(this.mapper.Map<User>(user));

            if (existingUser == null)
                return Tuple.Create<UserViewModel, ErrorMessage>(null, this.messages.Value.LoginError); ;

            var decyrptedPassword = this.dataProtectionProvider.CreateProtector(this.encryptionSettings.Value.Key).Unprotect(existingUser.Password);

            if (existingUser.Email == user.Email && decyrptedPassword == user.Password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Value.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                var claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim("Valid", "1"));
                claims.Add(new Claim("UserId", existingUser.UserId.ToString()));

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(this.jwtSettings.Value.Issuer, //Issure    
                                this.jwtSettings.Value.Audience,  //Audience    
                                claims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);

                var createdToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Tuple.Create<UserViewModel, ErrorMessage>(new UserViewModel()
                {
                    Token = createdToken
                }, null);
            }

            return Tuple.Create<UserViewModel, ErrorMessage>(null, this.messages.Value.LoginError); ;
        }

        /// <summary>
        /// Create a new User 
        /// </summary>
        /// <param name="user">User Details </param>
        /// <returns></returns>
        public async Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            user.Password = this.dataProtectionProvider.CreateProtector(this.encryptionSettings.Value.Key).Protect(user.Password);
            return this.mapper.Map<UserViewModel>(await this.userRepository.AddAsync(this.mapper.Map<User>(user)));
        }
    }
}