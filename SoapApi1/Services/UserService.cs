using System.ServiceModel;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Mappers;
using SoapApi.Repositories;

namespace SoapApi.Services;

public class UserService : IUserContract{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    public Task<IList<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<UserResponseDto>> GetAllByEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponseDto> GetUserById(Guid userId, CancellationToken cancellationToken){
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        if(user is not null){
            return user.ToDto();
        }

        throw new FaultException("User not found");
    }
}