using System;
using BlackhoefStudios.Common.ViewModels.Bases;
using DropIt.Data;
using BlackhoefStudios.Common.Interfaces.Pages;

namespace DropIt.ViewModels.Categories
{
	public class EditCategoryViewModel : ObservableViewModel
	{
		public Category Model { get; private set; }
		public string Name {
			get { return Model.Name; }
			set {
				Model.Name = value;
				OnPropertyChanged ("Name");
			}
		}

		public int TaskCount {
			get { return Model.NavigationIds.Count; }
		}

		protected override void OnPropertyChanged (string propertyName)
		{
			base.OnPropertyChanged (propertyName);
			if (propertyName == "Model" && Model != null) {
				OnPropertyChanged ("Name");
				OnPropertyChanged ("TaskCount");
			}
		}

		public EditCategoryViewModel (Category model)
		{
			Model = model;
		}
	}
}

