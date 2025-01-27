using SoapApi.Dtos;
using SoapApi.Infrastructure.Entities;

namespace SoapApi.Mappers;

public static class UserMapper{
    public static UserModel ToModel(this UserEntity user){
        if(user is null){
            return null;
        }

        return new UserModel{
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate
        };
    }
    public static UserResponseDto ToDto(this UserModel user){
        return new UserResponseDto{
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate
        };
    }
}