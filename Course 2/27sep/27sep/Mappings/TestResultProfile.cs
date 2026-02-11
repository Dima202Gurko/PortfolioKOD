using _27sep.Responses.TestResult;
using AutoMapper;
using TestingPlatform.Application.Dtos;

namespace _27sep.Mappings
{
    public class TestResultProfile : Profile
    {
        public TestResultProfile()
        {
            CreateMap<TestResultDto, TestResultResponse>();
        }
    }

}
