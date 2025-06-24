using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAP.Questions
{
	public static class QuestionFactory
	{
		public static IQuestion CreateQuestion(String questionType)
		{
			switch (questionType.ToLower())
			{
				case "Simple":
					return new SimpleQuestion();
				case "Choice":
					return new ChoiceQuestion();
				case "Kahoot":
					return new KahootQuestion();
				default:
					throw new ArgumentException("Tipo de pregunta no válido");
			}
		}
	}
}
