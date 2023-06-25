using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barArcadeGame
{
    public class Question
    {
        public string QuestionText { get; }
        public List<string> Options { get; }
        public int CorrectAnswerIndex { get; }

        public Question(string questionText, List<string> options)
        {
            QuestionText = questionText;
            Options = options;
        }
    }
}


