using AutoMapper;
using TrashPandaNet.Data.Models;
using TrashPandaNet.Logic.Models;

namespace TrashPandaNet.Logic.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(
                    x => x.Email,
                    y => y.MapFrom(z => z.EmailReg))
                .ReverseMap();

            CreateMap<LoginViewModel, User>()
                .ForMember(
                    x => x.UserName,
                    y => y.MapFrom(z => z.Email))
                .ReverseMap();

            CreateMap<UserEditViewModel, User>().ReverseMap();

            CreateMap<UserWithFriendExt, User>().ReverseMap();
        }
    }
}
