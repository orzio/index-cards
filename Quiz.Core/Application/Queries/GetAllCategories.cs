using MediatR;
using Quiz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Queries
{
    public class GetAllCategories : IRequest<IEnumerable<CategoryWithChildrenDto>>
    {
    }
}
