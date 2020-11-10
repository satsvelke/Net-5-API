using AutoMapper;
using Model;
using ViewModel;

namespace BusinessLayer.Depenedency
{
    // Automapper profile settings 
    public partial class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserViewModel>().ForMember(x => x.Password, o => o.Ignore()).ReverseMap();
        }
    }
}