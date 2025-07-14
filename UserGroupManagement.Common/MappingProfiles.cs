using AutoMapper;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Repository.Entities;

namespace UserGroupManagement.Common
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<UserEntity, User>().ReverseMap();
            //CreateMap<GroupEntity, Group>().ReverseMap();
            //CreateMap<UserDto, User>().ReverseMap();
            //CreateMap<GroupDto, Group>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            //CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}
