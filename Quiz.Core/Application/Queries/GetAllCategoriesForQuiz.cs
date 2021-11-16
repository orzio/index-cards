using MediatR;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz.Core.Application.Queries
{
    public class GetAllCategoriesForQuiz : IRequest<IEnumerable<CategoryWithChildrenDto>>
    {
    }
}
