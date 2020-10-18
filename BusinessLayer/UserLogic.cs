using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Interface;
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

        public async Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            var addedUser = await _userRepository.AddAsync(_mapper.Map<User>(user));
            return _mapper.Map<UserViewModel>(addedUser);
        }
    }
}