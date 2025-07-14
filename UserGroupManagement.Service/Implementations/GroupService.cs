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
            var createdGroup = await _groupRepository.CreateAsync(groupEntity, dto.MemberIds);

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

        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            var groups = await _groupRepository.GetAllAsync();
            return _mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            return _mapper.Map<GroupDto>(group);
        }

        public async Task<GroupDto> UpdateAsync(GroupUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.GroupName))
                throw new ArgumentException("Group name is required.");


            var groupEntity = new Group { Id = dto.Id, GroupName = dto.GroupName };

            var updatedGroup = await _groupRepository.UpdateAsync(groupEntity, dto.MemberIds);

            return new GroupDto
            {
                Id = updatedGroup.Id,
                GroupName = updatedGroup.GroupName,
                Members = updatedGroup.UserGroups.Select(ug => new UserDto
                {
                    Id = ug.User.Id,
                    FirstName = ug.User.FirstName,
                    LastName = ug.User.LastName,
                    Age = ug.User.Age,
                    Email = ug.User.Email
                }).ToList()
            };
        }

        public async Task<GroupDto> DeleteAsync(int id)
        {
            var deletedGroup = await _groupRepository.DeleteAsync(id);

            return new GroupDto
            {
                Id = deletedGroup.Id,
                GroupName = deletedGroup.GroupName,
                Members = deletedGroup.UserGroups.Select(ug => new UserDto
                {
                    Id = ug.User.Id
                }).ToList()
            };
        }
    }
}
