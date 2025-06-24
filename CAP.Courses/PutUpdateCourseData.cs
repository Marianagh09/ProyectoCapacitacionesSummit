using Sif;
using Sif.Data;
using Sif.ImEx;
using Sif.Journal;
using Sif.Security;
using Sif.Services;

namespace CAP.Courses
{
	public class PutUpdateCourseData : DataService
	{
		public PutUpdateCourseData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;

			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fUpdate, this.Connection))
			{
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.FileIdName, this.Dictionary.ImEx.FieldId);
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.NameName, this.Dictionary.ImEx.Name);
				command.AddParameter(this.Dictionary.ImEx, DataDictImEx.DescriptionName, this.Dictionary.ImEx.Description);
				command.AddParameter(this.Dictionary.Security, DataDictSecurity.TellerIdName, this.Dictionary.Security.TellerId);
				command.AddParameter(this.Dictionary.Journal, DataDictJournal.StartDateTimeName, this.Dictionary.Journal.StartDateTime);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
				return state;
			}
		}

		private static readonly String fUpdate = "UPDATE CAP.Courses SET title =" + DataDictImEx.ParName +
			", description =" + DataDictImEx.ParDescription +
			", creator_Id =" + DataDictSecurity.ParTellerId +
			", creation_date =" + DataDictJournal.StartDateTimeName + "where coursesId = " + DataDictImEx.ParFieldId; 
	}
}
