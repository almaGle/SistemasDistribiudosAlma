using SoapApi.Dtos;

namespace SoapApi.Repositories;

public interface IUserRepository{
    public Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}