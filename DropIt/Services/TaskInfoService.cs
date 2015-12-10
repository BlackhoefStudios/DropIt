using System;

using Xamarin.Forms;
using DropIt.ViewModels.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using DropIt.Data;
using System.Linq;

namespace DropIt.Services
{
	public class TaskInfoService : StorageService<TaskInfo>
	{
		
		const string TaskFileName = "tasks.json";

		public TaskInfoService () : base(TaskFileName)
		{
		}


		public async Task<TaskInfo> GetTask(Guid id){
			await GetAll ();
			return await Task.Run (() => All.FirstOrDefault (t => t.Id == id));
		}

		public async Task<IEnumerable<TaskItemViewModel>> Filter(Guid categoryId) {

			return await Task.Run(async () => {
				await GetAll();
				var filtered = new List<TaskInfo>(All.Where(c => c.ParentForeignKey == categoryId));

				return filtered
					.Select(p => new TaskItemViewModel{
						Id = p.Id,
						ModelId = p.Id,
						Name = p.Name
					});
			});
		}

		public async Task<TaskItemViewModel> SaveTask(TaskInfo toSave) {
			await Task.Run (async () => {
				await GetAll();

				All.Add(toSave);

				await Save();
			});

			return new TaskItemViewModel(){
				Id = toSave.Id,
				ModelId = toSave.Id,
				Name = toSave.Name
			};
		}

		public async Task DeleteTasks(IEnumerable<Guid> toDelete) {
			await Task.Run (async () => {
				bool removed = false;
				await GetAll();
				foreach (var id in toDelete) {
					var toRemove = All.FirstOrDefault(c => c.Id == id);
					if(toRemove != null){
						//Filtered.Remove(toRemove);
						All.Remove(toRemove);
						removed = true;
					}
				}

				if(removed)
					await Save();
			});
		}

		public async Task<bool> DeleteTask(Guid toDelete) {
			return await Task.Run (async () => {
				await GetAll();
				var toRemove = All.FirstOrDefault(p => p.Id == toDelete);

				if(toRemove != null){
					//Filtered.Remove(toRemove);
					All.Remove(toRemove);

					await Save();
					return true;
				}

				return false;
			});
		}


		public async Task<int> CountTasks(Guid categoryId){
			return await Task.Run (() => {
				if (All != null)
					return All.Count (t => t.ParentForeignKey == categoryId);
				return 0;
			});
		}
	}
}


