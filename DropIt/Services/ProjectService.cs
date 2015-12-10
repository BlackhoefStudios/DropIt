using System;

using Xamarin.Forms;
using System.Collections.Generic;
using DropIt.ViewModels.Projects;
using System.Threading.Tasks;
using DropIt.Data;
using System.Linq;
using Newtonsoft.Json;
using DropIt.Data.Interfaces.Services;
using System.Collections;

namespace DropIt.Services
{
	public class ProjectService : StorageService<Project>
	{
		public const string ProjectFileName = "projects.json";

		public ProjectService () : base(ProjectFileName)
		{
		}

		public async Task<IEnumerable<ProjectViewModel>> GetProjects() {
			
			return await Task.Run<IEnumerable<ProjectViewModel>>(async () => {
				await GetAll();
				Filtered = All;
				var toReturn = new List<ProjectViewModel>();

				foreach (var item in Filtered) {
					var toAdd = new ProjectViewModel{
						Id = item.Id,
						Name = item.Name
					};
					int sum = 0;

					foreach (var category in item.NavigationIds) {
						sum += await ServiceResolver.Tasks.CountTasks(category);
					}

					toAdd.Subtitle = sum.ToString();
					toReturn.Add(toAdd);
				}

				return toReturn;
			});
		}

		public async Task<ProjectViewModel> SaveProject(Project toSave) {
			await Task.Run (async () => {
				Filtered.Add(toSave);
				await Save();
			});

			return new ProjectViewModel(){
				Id = toSave.Id,
				Name = toSave.Name
			};
		}

		public async Task<bool> DeleteProject(Guid toDelete) {
			return await Task.Run (async () => {
				if(All == null)
					return false;
				
				var toRemove = All.FirstOrDefault(p => p.Id == toDelete);

				if(toRemove != null){
					Filtered.Remove(toRemove);

					//remove categories
					var categoryService = ServiceResolver.Categories;

					await categoryService.DeleteCategories(toRemove.NavigationIds);

					await Save();
					return true;
				}

				return false;
			});
		}


	}
}


