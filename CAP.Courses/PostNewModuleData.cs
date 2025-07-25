using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sif;
using Sif.Agreements;
using Sif.Data;
using Sif.Enterprises;
using Sif.Services;

namespace CAP.Courses
{
	public class PostNewModuleData : DataService
	{
		public PostNewModuleData(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			using (SifDBCommand command = DBFactory.DefaultFactory.NewDBCommand(fModules, this.Connection))
			{
				command.AddParameter(this.Dictionary.Agreements, DataDictAgreements.CollectionTypeNameName, this.Dictionary.Agreements.CollectionTypeName);
				command.AddParameter(this.Dictionary.Agreements, DataDictAgreements.AgreementDescriptionName, this.Dictionary.Agreements.AgreementDescription);
				command.AddParameter(this.Dictionary.Enterprises, DataDictEnterprises.BranchIdName, this.Dictionary.Enterprises.BranchId);
				command.AddParameter(this.Dictionary.Agreements, DataDictAgreements.AgreementNameName, this.Dictionary.Agreements.AgreementName);
				var idParams = command.AddParameter(this.Dictionary.Agreements, DataDictAgreements.AgreementIdName, 0);
				idParams.DbType = DbType.Int64;
				idParams.Direction = ParameterDirection.Output;
				Int32 rows = command.ExecuteNonQuery(this.Message);
				if (rows > 0)
				{
					state = ServiceState.Accepted; 
				}
				Int64 newId = Convert.ToInt64(idParams.Value.ToString());
				this.Dictionary.Agreements.AgreementId = newId; 
				return state;
			}
		}

		private static readonly String fModules = "insert into CAP.Modules (type, descripcion, course_id, name)" +
			"values (" + DataDictAgreements.ParCollectionTypeName + ", " + DataDictAgreements.ParAgreementDescription + ", "
			+ DataDictEnterprises.ParBranchId + ", " + DataDictAgreements.ParAgreementName + ") " + "Returning ModuleId into " +
			DataDictAgreements.ParAgreementId;

		//DataDictEnterprises.ParBranchId
	}
}
