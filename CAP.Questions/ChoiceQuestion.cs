using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP.Questions
{
	public class ChoiceQuestion : IQuestion
	{
		public List<Dictionary<String, Int32>>? options {  get; set; }

		public int QuizId => throw new NotImplementedException();

		public string CorrectAnswer => throw new NotImplementedException();
	}
}
