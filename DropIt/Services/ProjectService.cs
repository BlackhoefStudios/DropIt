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

		public async Task<Project> GetProject(Guid id){
			await GetAll ();
			return await Task.Run (() => All.FirstOrDefault (t => t.Id == id));
		}

		public async Task<IEnumerable<ProjectViewModel>> GetProjects() {
			
			return await Task.Run<IEnumerable<ProjectViewModel>>(async () => {
				await GetAll();
				var toReturn = new List<ProjectViewModel>();

				foreach (var item in All) {
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
				var existing = All.FirstOrDefault(p => p.Id == toSave.Id);
				if(existing == null)
					All.Add(toSave);
				else {
					All.Remove(existing);
					All.Add(toSave);
				}
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
					All.Remove(toRemove);

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


