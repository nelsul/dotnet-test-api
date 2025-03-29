using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTestApi.DTOs.User;
using MyTestApi.Models;

namespace MyTestApi.Mappers
{
    public static class UserMappers
    {
        public static AppUser ToUser(this CreateUserDTO createUserDTO)
        {
            return new AppUser { UserName = createUserDTO.Username, Email = createUserDTO.Email };
        }

        public static UserDTO ToUserDTO(this AppUser user, string token)
        {
            return new UserDTO
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = token,
            };
        }
    }
}
