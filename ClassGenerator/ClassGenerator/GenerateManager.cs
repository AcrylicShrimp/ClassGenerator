
using System.Collections.Generic;

namespace ClassGenerator
{
	public class GenerateManager
	{
		public static GenerateManager Instance { get { return GenerateManager.sInstance; } }

		private static GenerateManager sInstance;

		Dictionary<string, Generator> sGeneratorDict;

		public GenerateManager()
		{
			GenerateManager.sInstance = this;

			this.sGeneratorDict = new Dictionary<string, Generator>();
			
		}
	}
}