using AutoMapper;
using Model;
using ViewModel;

namespace BusinessLayer.Depenedency
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>().ForMember(x => x.Password, o => o.Ignore());
        }
    }
}