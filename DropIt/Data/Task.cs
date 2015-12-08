using System;
using System.Collections.Generic;

namespace DropIt.Data
{
	public class Task : BaseData
	{
		public List<Guid> Comments {get;set;}

		public Task ()
		{
			Comments = new List<Guid> ();
		}
	}
}

