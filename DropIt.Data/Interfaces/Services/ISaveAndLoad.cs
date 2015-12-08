using System;

namespace DropIt.Data.Interfaces.Services
{
	public interface ISaveAndLoad
	{
		void SaveText (string filename, string text);
		string LoadText (string filename);
	}
}

