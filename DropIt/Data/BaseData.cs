using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DropIt.Data
{
	public class BaseData {
		public Guid Id { get; set; }

		public Guid ParentForeignKey { get; set; }


		public BaseData ()
		{
			Id = Guid.NewGuid ();
			ParentForeignKey = Guid.Empty;
		}
	}

	public class BaseData<T> : BaseData where T : BaseData
	{

		public List<Guid> NavigationIds { get; set; }

		[JsonIgnore]
		public List<T> NavigationItems { get; set; }

		public BaseData ()
		{
			NavigationItems = new List<T> ();
			NavigationIds = new List<Guid> ();
		}

	}
}

