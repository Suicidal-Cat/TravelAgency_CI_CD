using AutoMapper;
using Models.Dtos;
using Models.Entities;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Services.Implementation
{
    public class UserService(IUserRepository _userRepository, IMapper mapper) : IUserService
    {
        public async Task Create(RegisterDto model)
        {
            var user = mapper.Map<User>(model);
            user = _userRepository.Add(user);
            await _userRepository.SaveChanges();
        }

        public async Task<User?> GetByUsername(string username)
        {
            var user = await _userRepository.FindOne(u => u.Username == username);
            return user;
        }
    }
}
