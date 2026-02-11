using _27sep.Requests.Question;
using AutoMapper;
using TestingPlatform.Application.Dtos;

namespace _27sep.Mappings
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDto, QuestionResponse>();
            CreateMap<CreateQuestionRequest, QuestionDto>();
            CreateMap<UpdateQuestionRequest, QuestionDto>();
        }
    }

}
