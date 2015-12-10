using System;
using DropIt.ViewModels.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;
using DropIt.Data;
using System.Linq;
using DropIt.ViewModels.Tasks;
using DropIt.Data.Interfaces.Services;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace DropIt.Services
{
	public class CategoriesService : StorageService<Category>
	{
		const string CategoriesFileName = "categories.json";


		public CategoriesService () : base(CategoriesFileName)
		{
		}

		public async Task<Category> GetCategory(Guid id){
			await GetAll ();
			return await Task.Run (() => All.DefaultIfEmpty(null).FirstOrDefault (t => t.Id == id));
		}

		public async Task<IEnumerable<CategoryListItemViewModel>> GetCategories(Guid projectId, bool includeTasks) {

			return await Task.Run(async () => {
				await GetAll();
				var filtered = new List<Category>(All.Where(c => c.ParentForeignKey == projectId));
				var toReturn = new List<CategoryListItemViewModel>();

				if(includeTasks){
					var taskService = ServiceResolver.Tasks;

					foreach (var category in filtered) {
						var toAdd = new CategoryListItemViewModel(){
							Id = category.Name,
							ModelId = category.Id,
							Name = category.Name
						};

						foreach (var taskId in category.NavigationIds) {
							var taskInfo = await taskService.GetTask(taskId);
							if(taskInfo != null){
								toAdd.Add(new TaskItemViewModel(){
									Id = taskInfo.Id,
									Name = taskInfo.Description,
									Subtitle = taskInfo.AssignedTo
								});
							}
						}

						toReturn.Add(toAdd);
					}
				}
				else {
					toReturn = filtered.Select(c => new CategoryListItemViewModel(){
						Id = c.Name,
						ModelId = c.Id,
						Name = c.Name
					}).ToList();
				}
				return toReturn;
			});
		}

		public async Task<CategoryListItemViewModel> SaveCategory(Category toSave) {
			await Task.Run (async () => {
				await GetAll();
				All.Add(toSave);
				await Save();
			});

			return new CategoryListItemViewModel(){
				Id = toSave.Name,
				ModelId = toSave.Id,
				Name = toSave.Name
			};
		}

		public async Task DeleteCategories(IEnumerable<Guid> toDelete) {
			await Task.Run (async () => {
				await GetAll();
				bool removed = false;
				var taskService = ServiceResolver.Tasks;

				foreach (var id in toDelete) {
					var toRemove = All.FirstOrDefault(c => c.Id == id);
					if(toRemove != null){
						All.Remove(toRemove);
						await taskService.DeleteTasks(toRemove.NavigationIds);
						removed = true;
					}
				}

				if(removed)
					await Save();
			});
		}

		public async Task AddTask(TaskInfo toAdd){
			All.First (c => c.Id == toAdd.ParentForeignKey).NavigationIds.Add (toAdd.Id);
			await Save ();
		}

		public async Task RemoveTask(TaskInfo toAdd){
			All.First (c => c.Id == toAdd.ParentForeignKey).NavigationIds.Remove (toAdd.Id);
			await Save ();
		}

		public async Task<bool> DeleteCategory(Guid toDelete) {
			return await Task.Run (async () => {
				await GetAll();
				var toRemove = All.FirstOrDefault(p => p.Id == toDelete);
				var taskService = ServiceResolver.Tasks;

				if(toRemove != null){
					All.Remove(toRemove);
					await taskService.DeleteTasks(toRemove.NavigationIds);

					await Save();
					return true;
				}

				return false;
			});
		}

	}
}

