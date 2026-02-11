using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Test mappings
            CreateMap<Test, TestDto>().ReverseMap();

            // Answer mappings  
            CreateMap<Answer, AnswerDto>().ReverseMap();

            CreateMap<Student, StudentDto>()
            .ForMember(d => d.User, m => m.MapFrom(s => s.User));
            CreateMap<StudentDto, Student>()
                .ForMember(d => d.User, m => m.Ignore());

            // User mappings
            CreateMap<User, UserDto>().ReverseMap();

            // Group mappings
            CreateMap<Group, GroupDto>();
            CreateMap<GroupDto, Group>()
                .ForMember(d => d.Course, o => o.Ignore())
                .ForMember(d => d.Direction, o => o.Ignore())
                .ForMember(d => d.Project, o => o.Ignore());
        }
    }
}
