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
using System.Collections;

namespace DropIt.Services
{
	public abstract class StorageService<TSource> where TSource : class, new() {
		
		protected string FileName { get; private set; }
		protected ISaveAndLoad DeviceStorage { get; set; }
		protected List<TSource> Filtered { get; set; }
		protected List<TSource> All { get; set; }


		protected StorageService (string fileName)
		{
			FileName = fileName;
			DeviceStorage = DependencyService.Get<ISaveAndLoad>();
			Filtered = new List<TSource> ();
			All = null;
		}

		protected async Task<List<TSource>> GetAsSource() {
			return await Task.Run (() => {
				var originalJson = DeviceStorage.LoadText(FileName);

				if(!String.IsNullOrEmpty(originalJson)){
					return JsonConvert.DeserializeObject<List<TSource>> (originalJson);
				}

				return new List<TSource>();
			});
		}

		protected async Task GetAll() {
			if(All == null)
				All = await GetAsSource();
		}

		protected async Task Save() {
			await Task.Run (() => {
				var json = JsonConvert.SerializeObject (Filtered);
				DeviceStorage.SaveText (FileName, json);
			});
		}
	}
}

