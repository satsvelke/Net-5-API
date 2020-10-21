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
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserLogic(IUserRepository IUserRepository, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = IUserRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings;
        }

        public async Task<UserViewModel> CreateTokenAsync(UserViewModel user)
        {
            var u = await _userRepository.GetUserByEmail(_mapper.Map<User>(user));
            if (u == null)
                return null;

            if (u.Email == user.Email && u.Password == user.Password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

                var claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                claims.Add(new Claim("Valid", "1"));
                claims.Add(new Claim("UserId", u.UserId.ToString()));

                //Create Security Token object by giving required parameters    
                var token = new JwtSecurityToken(_jwtSettings.Value.Issuer, //Issure    
                                _jwtSettings.Value.Audience,  //Audience    
                                claims,
                                expires: DateTime.Now.AddDays(1),
                                signingCredentials: credentials);

                var createdToken = new JwtSecurityTokenHandler().WriteToken(token);

                return new UserViewModel()
                {
                    Token = createdToken
                };
            }

            return null;
        }

        public async Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            var addedUser = await _userRepository.AddAsync(_mapper.Map<User>(user));
            return _mapper.Map<UserViewModel>(addedUser);
        }
    }
}