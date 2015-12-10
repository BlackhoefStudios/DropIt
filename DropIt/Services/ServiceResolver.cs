using System;

namespace DropIt.Services
{
	public static class ServiceResolver
	{
		public static readonly CategoriesService Categories = new CategoriesService();
		public static readonly TaskInfoService Tasks = new TaskInfoService();
		public static readonly ProjectService Projects = new ProjectService();
	}
}

