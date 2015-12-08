using System;

using Xamarin.Forms;
using System.Collections.Generic;
using DropIt.ViewModels.Projects;
using System.Threading.Tasks;
using DropIt.Data;
using System.Linq;

namespace DropIt.Services
{
	public class ProjectService : StorageService
	{
		const string ProjectFileName = "projects.json";

		public ProjectService () : base(ProjectFileName)
		{

		}

		public async Task<IEnumerable<ProjectViewModel>> GetProjects() {
			var projects = await Get<Project>();

			return await System.Threading.Tasks.Task.Run<IEnumerable<ProjectViewModel>>(() => {
				return projects.Select(p => new ProjectViewModel{
					Id = p.Id,
					Name = p.Name
				});
			});
		}

		public async System.Threading.Tasks.Task<ProjectViewModel> SaveProject(Project toSave) {
			await Save (toSave);
			return new ProjectViewModel(){
				Id = toSave.Id,
				Name = toSave.Name
			};
		}

		public async System.Threading.Tasks.Task<bool> DeleteProject(Guid toDelete) {
			return await Delete<Project> (toDelete);
		}
	}
}


