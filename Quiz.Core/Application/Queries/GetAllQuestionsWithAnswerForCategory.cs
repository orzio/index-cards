using MediatR;
using Quiz.Core.DTO;
using System.Collections.Generic;

namespace Quiz.Core.Application.Queries
{
    public class GetAllQuestionsWithAnswerForCategory : IRequest<IEnumerable<QuestionWithAnswerForCategoryDto>>
    {
        public int Id { get; set; }
    }
}