
using System.Collections.Generic;
using System.Text;

namespace ClassGenerator
{
	public interface Generator
	{
		string GenerationTypeName { get; }

		Dictionary<string, StringBuilder> generate(string sAuthorName, string sIncludeGuardDefinition, string sNamespaceDeclaration, string sTargetName, bool bUseSpaceForIndentation);
	}
}