using System;
using System.Collections.Generic;

namespace DropIt.Data
{
	public class Project : BaseData
	{
		public string Name {get;set;}
		public List<Guid> Categories { get; set; }

		public Project ()
		{
			Categories = new List<Guid> ();

		}
	}
}

