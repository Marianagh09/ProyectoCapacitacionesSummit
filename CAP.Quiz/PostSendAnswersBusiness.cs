﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Services;

namespace CAP.Quiz
{
	public class PostSendAnswersBusiness : BusinessService
	{
		public PostSendAnswersBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			return this.StartService(new PostSendAnswersData(this.Dictionary));
		}
	}
}
