using AutoMapper;
using RelibreApi.ViewModel;

namespace RelibreApi.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<UserViewModel, Models.User>();


            CreateMap<Models.User, UserViewModel>();
        }
    }
}