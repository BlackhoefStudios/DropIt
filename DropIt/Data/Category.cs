using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DropIt.Data
{
	public class Category : BaseData<TaskInfo>
	{
		public string Name {get;set;}

		public Category ()
		{
		}
	}
}

