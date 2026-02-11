using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestDto>> GetAllAsync(
                   bool? isPublic = null,
                   List<int>? groupIds = null,
                   List<int>? studentIds = null,
                   string? sortBy = null,
                   bool sortDescending = true,
                   int pageNumber = 1,
                   int pageSize = 10
                 );
        Task<IEnumerable<TestDto>> GetAllForStudent(int studentId);
        Task<TestDto> GetByIdAsync(int id);
        Task<int> CreateAsync(TestDto testDto);
        Task UpdateAsync(TestDto testDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5);
        Task<IEnumerable<object>> GetTestCountByTypeAsync();
        Task<IEnumerable<object>> GetCourseStatsAsync();
        Task<IEnumerable<object>> GetDirectionAveragesAsync();
        Task<IEnumerable<object>> GetTestTimelineByPublicAsync();
        Task<IEnumerable<object>> GetTopGroupsByTestCountAsync(int top = 10);
    }
}
