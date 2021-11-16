using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Domain
{
   public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
    }
}
