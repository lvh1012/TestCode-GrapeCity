using TestCode.Entities;

namespace TestCode.Services.Interfaces
{
    public interface IUserService
    {
        Task<Azure.AsyncPageable<UserEntity>> GetAll();
        Task<UserEntity> Create(UserEntity entity);
    }
}
