using AutoMapper;
using UserGroupManagement.Common.DTOs;
using UserGroupManagement.Repository.Entities;
using UserGroupManagement.Repository.Interfaces;
using UserGroupManagement.Service.Interfaces;

namespace UserGroupManagement.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FirstName) || string.IsNullOrEmpty(userDto.LastName))
                throw new ArgumentNullException("First and Last name are required");

            if (userDto.Age < 0)
                throw new ArgumentException("Age must be positive number");

            if (!new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(userDto.Email))
                throw new ArgumentException("Invalid email");
                

            var userEntity = _mapper.Map<User>(userDto);
            var savedUser = await _userRepository.AddAsync(userEntity);
            return _mapper.Map<UserDto>(savedUser);
        }
    }
}
