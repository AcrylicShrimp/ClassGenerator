
using System.Collections.Generic;
using System.Linq;

namespace ClassGenerator
{
	public class GenerateManager
	{
		public static GenerateManager Instance { get { return GenerateManager.sInstance; } }

		private static GenerateManager sInstance;

		Dictionary<string, Generator> sGeneratorDict;

		public static void init()
		{
			if (GenerateManager.sInstance == null)
				GenerateManager.sInstance = new GenerateManager();
		}

		public static void fin()
		{
			if (GenerateManager.sInstance != null)
				GenerateManager.sInstance = null;
		}

		public GenerateManager()
		{
			this.sGeneratorDict = new Dictionary<string, Generator>();

			{
				var sGenerator = new CPPClassGenerator();
				this.sGeneratorDict.Add(sGenerator.GenerationTypeName, sGenerator);
			}
		}

		public List<KeyValuePair<string, Generator>> getGeneratorList()
		{
			return this.sGeneratorDict.ToList();
		}
	}
}