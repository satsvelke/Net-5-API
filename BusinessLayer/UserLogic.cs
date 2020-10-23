using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.AppSettings;
using BusinessLayer.Interface;
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
        public UserLogic(IUserRepository iUserRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings, IOptions<DefaultMessage> messages)
        {
            this.userRepository = iUserRepository;
            this.mapper = mapper;
            this.jwtSettings = jwtSettings;
            this.messages = messages;
        }

        public async Task<Tuple<UserViewModel, ErrorMessage>> CreateTokenAsync(UserViewModel user)
        {
            var existingUser = await this.userRepository.GetUserByEmail(this.mapper.Map<User>(user));

            if (existingUser == null)
                return Tuple.Create<UserViewModel, ErrorMessage>(null, this.messages.Value.LoginError); ;

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

        public async Task<UserViewModel> CreateUserAsync(UserViewModel user) => this.mapper.Map<UserViewModel>(await this.userRepository.AddAsync(this.mapper.Map<User>(user)));
    }
}