using _27sep.Requests.Auth;
using _27sep.Responses.Auth;
using AutoMapper;
using TestingPlatform.Application.Dtos;

namespace _27sep.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthRequest, UserLoginDto>();
            CreateMap<UserDto, AuthResponse>();
        }
    }
}
