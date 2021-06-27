using AutoMapper;
using Domain.Model;
using Domain.ViewModel;

namespace Workflow.Depenedency
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