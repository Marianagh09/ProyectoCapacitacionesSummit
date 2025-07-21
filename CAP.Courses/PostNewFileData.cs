using Sif;
using Sif.Agreements;
using Sif.Data;
using Sif.Enterprises;
using Sif.Services;

namespace CAP.Courses
{
	public class PostNewFileData : DataService
	{
		public PostNewFileData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fFile, this.Connection))
			{
				command.AddParameter(this.Dictionary.Enterprises, DataDictEnterprises.BranchNameName, this.Dictionary.Enterprises.BranchName);
				command.AddParameter(this.Dictionary.Enterprises, DataDictEnterprises.DocumentTypeName, this.Dictionary.Enterprises.DocumentType);
				command.AddParameter(this.Dictionary.Agreements, DataDictAgreements.AgreementIdName, this.Dictionary.Agreements.AgreementId);
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted;
				}
				return state;
			}
		}

		private static readonly String fFile = "insert into CAP.Files (filename, fileurl, module_id) values ( "+
			DataDictEnterprises.ParBranchName + ", " + DataDictEnterprises.ParDocumentType + ", " +
			DataDictAgreements.ParAgreementId;
	}
}
