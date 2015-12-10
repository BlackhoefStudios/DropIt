using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DropIt.Data
{
	public class Project : BaseData<Category>
	{
		public string Name {get;set;}

		public Project ()
		{
		}
	}
}

