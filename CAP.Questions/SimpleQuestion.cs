using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP.Questions
{
	public class SimpleQuestion : IQuestion
	{
		public String? Question {  get; set; }

		public int QuizId => throw new NotImplementedException();

		public string CorrectAnswer => throw new NotImplementedException();
	}
}
