using System;
using System.Collections.Generic;
using DropIt.Data;
using DropIt.ViewModels.Projects;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using DropIt.Data.Interfaces.Services;
using Xamarin.Forms;

namespace DropIt.Services
{
	public class StorageService
	{
		const string ProjectFileName = "projects.json";
		const string CategoriesFileName = "categroies.json";
		const string TaskFileName = "tasks.json";

		public async Task<IEnumerable<ProjectViewModel>> GetProjects() {
			return await System.Threading.Tasks.Task.Run<IEnumerable<ProjectViewModel>>(() => {
				var platform = DependencyService.Get<ISaveAndLoad>();
				var projectsJson = platform.LoadText(ProjectFileName);

				if(!String.IsNullOrEmpty(projectsJson)){
					var projects = JsonConvert.DeserializeObject<List<Project>> (projectsJson);

					var listOfProjects = projects.Select(p => new ProjectViewModel{
						Id = p.Id,
						Name = p.Name
					});

					return listOfProjects;
				}

				return new List<ProjectViewModel>();
			});
		}
//
		public async System.Threading.Tasks.Task<ProjectViewModel> SaveProject(Project toSave) {
			return await System.Threading.Tasks.Task.Run (() => {
				var platform = DependencyService.Get<ISaveAndLoad>();
				var projectsJson = platform.LoadText(ProjectFileName);

				var currentProjects = new List<Project>();

				if(!String.IsNullOrEmpty(projectsJson)){
					//add it to the existing list
					currentProjects = JsonConvert.DeserializeObject<List<Project>> (projectsJson);
				}

				currentProjects.Add(toSave);
				var json = JsonConvert.SerializeObject(currentProjects);
				platform.SaveText(ProjectFileName, json);
				return new ProjectViewModel(){
					Id = toSave.Id,
					Name = toSave.Name
				};
			});

		}

		public async System.Threading.Tasks.Task<bool> DeleteProject(Guid toDelete) {
			return await System.Threading.Tasks.Task.Run (() => {
				var platform = DependencyService.Get<ISaveAndLoad>();
				var projectsJson = platform.LoadText(ProjectFileName);

				var currentProjects = new List<Project>();

				if(!String.IsNullOrEmpty(projectsJson)){
					//add it to the existing list
					currentProjects = JsonConvert.DeserializeObject<List<Project>> (projectsJson);
					var toRemove = currentProjects.First(p => p.Id == toDelete);
					currentProjects.Remove(toRemove);
					var json = JsonConvert.SerializeObject(currentProjects);
					platform.SaveText(ProjectFileName, json);
					return true;
				}
				return false;
			});
		}


		public StorageService ()
		{
		}
	}
}

