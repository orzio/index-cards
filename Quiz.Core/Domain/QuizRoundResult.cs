using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Core.Domain
{
    public class QuizRoundResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int QuizRoundId { get; set; }
        public int QuestionId { get; set; }
        public float Weight { get; set; }
        public QuizRound QuizRound { get; set; }
        public Question Question { get; set; }
    }
}
