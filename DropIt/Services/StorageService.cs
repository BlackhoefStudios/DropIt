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
	public abstract class StorageService {
		
		protected string FileName { get; private set; }
		protected ISaveAndLoad DeviceStorage { get; set; }

		protected StorageService (string fileName)
		{
			FileName = fileName;
			DeviceStorage = DependencyService.Get<ISaveAndLoad>();
		}

		public async Task<IEnumerable<TReturn>> Get<TReturn>()
			where TReturn : class, new()
		{
			return await System.Threading.Tasks.Task.Run<IEnumerable<TReturn>>(() => {
				var originalJson = DeviceStorage.LoadText(FileName);

				if(!String.IsNullOrEmpty(originalJson)){
					return JsonConvert.DeserializeObject<List<TReturn>> (originalJson);
				}

				return new List<TReturn>();
			});
		}

		public async System.Threading.Tasks.Task Save<TReturn>(TReturn toSave) {
			await System.Threading.Tasks.Task.Run (() => {
				var originalJson = DeviceStorage.LoadText(FileName);

				var currentItems = new List<TReturn>();

				if(!String.IsNullOrEmpty(originalJson)){
					//add it to the existing list
					currentItems = JsonConvert.DeserializeObject<List<TReturn>> (originalJson);
				}

				currentItems.Add(toSave);
				var json = JsonConvert.SerializeObject(currentItems);
				DeviceStorage.SaveText(FileName, json);
			});
		}

		public async System.Threading.Tasks.Task<bool> Delete<TType>(Guid toDelete) 
			where TType : BaseData, new()
		{
			return await System.Threading.Tasks.Task.Run (() => {
				var originalJson = DeviceStorage.LoadText(FileName);

				var currentObjects = new List<TType>();

				if(!String.IsNullOrEmpty(originalJson)){
					//add it to the existing list
					currentObjects = JsonConvert.DeserializeObject<List<TType>> (originalJson);
					var toRemove = currentObjects.First(p => p.Id == toDelete);
					currentObjects.Remove(toRemove);
					var json = JsonConvert.SerializeObject(currentObjects);
					DeviceStorage.SaveText(FileName, json);
					return true;
				}
				return false;
			});
		}

	}
}

