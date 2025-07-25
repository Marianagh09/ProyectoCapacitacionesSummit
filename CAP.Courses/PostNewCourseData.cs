using System.Data;
using Sif;
using Sif.Agreements;
using Sif.Data;
using Sif.Enterprises;
using Sif.ImEx;
using Sif.Journal;
using Sif.Security;
using Sif.Services;

namespace CAP.Courses
{
	public class PostNewCourseData : DataService
	{
		public PostNewCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			try
			{
				using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fNew, this.Connection))
				{
					command.AddParameter(this.Dictionary.ImEx, DataDictImEx.NameName, this.Dictionary.ImEx.Name);
					command.AddParameter(this.Dictionary.ImEx, DataDictImEx.DescriptionName, this.Dictionary.ImEx.Description);
					command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
					command.AddParameter(this.Dictionary.Journal, DataDictJournal.StartDateTimeName, this.Dictionary.Journal.StartDateTime);
					var idParam = command.AddParameter(this.Dictionary.Enterprises, DataDictEnterprises.BranchIdName, ParameterDirection.Output);
					Int32 rows = command.ExecuteNonQuery(this.Message);
					if (rows > 0)
					{
						state = ServiceState.Accepted;
					}
					Int64 newId = Convert.ToInt64(idParam.Value.ToString());
					this.Dictionary.Enterprises.BranchId = newId;
					return state;
				}
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		private static readonly String fNew = "INSERT INTO CAP.Courses (title, description, creator_Id, creation_date) VALUES ("
			+ DataDictImEx.ParName + ", " + DataDictImEx.ParDescription + ", " + DataDictSecurity.ParTellerId + ", " + DataDictJournal.ParStartDateTime + ") RETURNING COURSESID INTO "
			+ DataDictEnterprises.ParBranchId;
	}
}
