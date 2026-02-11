using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllAsync();

        User GetByIdAsync(int id);

        int CreateAsync(User user);

        void UpdateAsync(User user);

        void DeleteAsync(int id);
    }
}
