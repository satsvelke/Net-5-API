using AutoMapper;
using Model;
using ViewModel;

namespace BusinessLayer.Depenedency
{
    public partial class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<User, UserViewModel>().ForMember(x => x.Password, o => o.Ignore());
        }
    }
}