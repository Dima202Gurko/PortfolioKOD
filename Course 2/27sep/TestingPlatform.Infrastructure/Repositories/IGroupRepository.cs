using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public interface IGroupRepository
    {
        List<Group> GetAll();

        Group GetById(int id);

        int Create(Group group);

        void Update(Group group);

        void Delete(int id);
    }
}
