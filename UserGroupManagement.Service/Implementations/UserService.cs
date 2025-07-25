﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            var userEntity = _mapper.Map<User>(dto);
            var savedUser = await _userRepository.CreateAsync(userEntity);
            return _mapper.Map<UserDto>(savedUser);
        }

        public async Task<UserDto> UpdateAsync(UserDto dto)
        {
            var userEntity = _mapper.Map<User>(dto);
            var updateduser = await _userRepository.UpdateAsync(userEntity);
            return _mapper.Map<UserDto>(updateduser);
        }

        public async Task<UserDto> DeleteAsync(int id)
        {
            var deletedUser = await _userRepository.DeleteAsync(id);
            return _mapper.Map<UserDto>(deletedUser);
        }
    }
}
