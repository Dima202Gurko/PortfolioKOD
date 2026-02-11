using _27sep.Requests.Answer;
using _27sep.Responses.Answer;
using AutoMapper;
using TestingPlatform.Application.Dtos;

namespace _27sep.Mappings
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnswerDto, AnswerResponse>();
            CreateMap<CreateAnswerRequest, AnswerDto>();
            CreateMap<UpdateAnswerRequest, AnswerDto>();
        }
    }
}
