using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Application.Responses
{
   public class Error
    {
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }

        public override string ToString()
        {
            return $"{PropertyName}: {ErrorMessage}";
        }
    }
}
