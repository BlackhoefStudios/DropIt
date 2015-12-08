using System;

namespace DropIt.Data
{
	public class BaseData
	{
		public Guid Id { get; set; }
		public BaseData ()
		{
			Id = Guid.NewGuid ();
		}
	}
}

