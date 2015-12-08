using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DropIt.Data
{
	public class Category : BaseData
	{
		public string Name {get;set;}

		public List<Guid> Tasks { get; set; }

		public Category ()
		{
			Tasks = new List<Guid> ();
		}
	}
}

