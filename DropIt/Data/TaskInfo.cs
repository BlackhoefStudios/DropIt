using System;
using System.Collections.Generic;

namespace DropIt.Data
{
	public class TaskInfo : BaseData
	{
		public string Name { get; set; }
		public List<Comment> Comments { get; set; }
		public DateTime DateCreated { get; set; }
		public string Description { get; set; }
		public string AssignedTo { get; set; }
		public bool IsComplete { get; set; }

		public TaskInfo ()
		{
			Comments = new List<Comment> ();
		}
	}
}

