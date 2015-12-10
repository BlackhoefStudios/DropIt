using System;

namespace DropIt.Data
{
	public class Comment : BaseData
	{
		public string Description { get; set; }
		public string CommenterName { get; set; }

		public Comment ()
		{
		}
	}
}

