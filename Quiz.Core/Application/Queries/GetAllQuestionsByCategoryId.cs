using MediatR;
using Quiz.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetAllQuestionsByCategoryId : IRequest<IEnumerable<QuestionDto>>
    {
        public int CategoryId { get; set; }
    }
}
