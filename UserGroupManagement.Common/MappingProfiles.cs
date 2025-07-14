using AutoMapper;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Repository.Entities;

namespace UserGroupManagement.Common
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>()
                .ForMember(dest => dest.Members, opt => opt.MapFrom(src => src.UserGroups.Select(ug => ug.User)));

            //CreateMap<GroupDto, Group>();
            CreateMap<UserCreateDto, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
