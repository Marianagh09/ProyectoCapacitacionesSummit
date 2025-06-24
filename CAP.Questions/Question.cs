using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP.Questions
{
	public interface IQuestion
	{
		Int32 QuizId { get; }
		String CorrectAnswer { get; }
	}
}
