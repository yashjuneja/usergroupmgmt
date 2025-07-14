using AutoMapper;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Interfaces;
using UserGroupManagement.Service.Interfaces;

namespace UserGroupManagement.Service.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupDto> CreateAsync(GroupCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.GroupName))
                throw new ArgumentException("Group name is required.");

            if (dto == null)
                dto.MemberIds = new List<int>();

            var groupEntity = new Group { GroupName = dto.GroupName };
            var createdGroup = await _groupRepository.AddAsync(groupEntity, dto.MemberIds);

            return new GroupDto
            {
                Id = createdGroup.Id,
                GroupName = createdGroup.GroupName,
                Members = createdGroup.UserGroups.Select(ug => new UserDto
                {
                    Id = ug.User.Id,
                    FirstName = ug.User.FirstName,
                    LastName = ug.User.LastName,
                    Age = ug.User.Age,
                    Email = ug.User.Email
                }).ToList()
            };
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GroupDto> UpdateAsync(GroupDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
