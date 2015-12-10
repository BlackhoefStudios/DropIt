using System;
using Xamarin.Forms;
using DropIt.Data.Interfaces.Services;
using DropIt.Droid.Dependencies;
using System.IO;
using DropIt.Droid.Dependencies;

[assembly: Dependency (typeof (StorageDependency))]
namespace DropIt.Droid.Dependencies {
	public class StorageDependency : ISaveAndLoad {
		public void SaveText (string filename, string text) {
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, filename);

			File.WriteAllText (filePath, text);
		}

		public string LoadText (string filename) {
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var filePath = Path.Combine (documentsPath, filename);
			if (File.Exists (filePath)) {
				return File.ReadAllText (filePath);
			}
			return null;
		}
	}
}