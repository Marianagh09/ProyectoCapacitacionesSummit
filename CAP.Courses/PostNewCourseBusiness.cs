using Newtonsoft.Json;
using ProyectoCapacitacionesSummit.Models;
using Sif;
using Sif.Services;

namespace CAP.Courses
{
	public class PostNewCourseBusiness : BusinessService
	{
		public PostNewCourseBusiness(DataDict dataDictionary) : base(dataDictionary)
		{
		}

		protected override ServiceState Process()
		{
			ServiceState state = ServiceState.Rejected;
			Course? course = JsonConvert.DeserializeObject<Course>(this.Dictionary.Sif.JsonResponseObject);
			if(course != null)
			{
				this.Dictionary.ImEx.Name = course.Title;
				this.Dictionary.ImEx.Description = course.Description;
				this.Dictionary.Security.TellerId = course.CreatorId;

				state = this.StartService(new PostNewCourseData(this.Dictionary));
				if (state == ServiceState.Accepted)
				{
					Boolean isFirstModule = false;
					foreach (Modules item in course.Modules)
					{
						if(!isFirstModule)
						{
							isFirstModule = true;
							continue;
						}

						//ejecutar por cada módulo
						this.Dictionary.Agreements.CollectionTypeName = item.Type;
						this.Dictionary.Agreements.AgreementDescription = item.Description;
						this.Dictionary.Agreements.AgreementName = item.Title;
						this.StartService(new PostNewModuleData(this.Dictionary));

					}
				}
			}

			return state;
		}
	}
}
