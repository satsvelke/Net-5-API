using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Interface;
using Microsoft.IdentityModel.Tokens;
using Model;
using Persistence.Interface;
using ViewModel;

namespace BusinessLayer
{
    public partial class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserLogic(IUserRepository IUserRepository, IMapper mapper)
        {
            _userRepository = IUserRepository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> CreateTokenAsync(UserViewModel user)
        {
            var u = await _userRepository.GetUserByEmail(_mapper.Map<User>(user));
            if (u == null)
                return null;

            if (u.Email == user.Email && u.Password == user.Password)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("94320lk9(*&(**yasdjklah"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, u.Email), new Claim("UserId", u.UserId.ToString()) };
                var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
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