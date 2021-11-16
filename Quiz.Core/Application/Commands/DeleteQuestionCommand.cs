using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Commands
{
    public class DeleteQuestionCommand:IRequest
    {
        public int Id { get; set; }
    }
}
