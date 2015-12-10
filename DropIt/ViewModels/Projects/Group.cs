using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DropIt.ViewModels.Projects
{
	public class Group<T> : ObservableCollection<T> where T : class
	{
		public string Name { get; set; }
		public Group ()
		{
		}

		public Group (IEnumerable<T> items)
		{
			foreach (var item in items) {
				Add (item);
			}
		}
	}
}

