using AutoMapper;
using Quiz.Core.Domain;
using Quiz.Core.Domain.Auth;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Mapper
{
   public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Role, RoleDto>();
            
            CreateMap<Answer, AnswerDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Question, QuestionWithAnswerForCategoryDto>()
                .ForMember(dest => dest.Answer, act => act.MapFrom(src => src.Answer))
                .ForMember(dest => dest.Question, act => act.MapFrom(src => new { src.Id, src.Content, src.CategoryId }));

        }
    }
}
