
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassGenerator
{
	public class CPPClassGenerator : Generator
	{
		public string GenerationTypeName { get { return "Class"; } }

		public Dictionary<string, StringBuilder> generate(string sAuthorName, string sIncludeGuardDefinition, string sNamespaceDeclaration, string sTargetName, bool bUseSpaceForIndentation)
		{
			var sResult = new Dictionary<string, StringBuilder>();

			var sIndent = "\t";

			if (bUseSpaceForIndentation)
				sIndent = "    ";

			var sCurrentDate = DateTime.Now.ToLocalTime();

			sResult.Add(".h", this.generateClassHeader(sCurrentDate, sAuthorName, sIncludeGuardDefinition, sNamespaceDeclaration, sTargetName, sIndent));
			sResult.Add(".cpp", this.generateClassBody(sCurrentDate, sAuthorName, sIncludeGuardDefinition, sNamespaceDeclaration, sTargetName, sIndent));

			return sResult;
		}

		private StringBuilder generateClassHeader(DateTime sNow, string sAuthorName, string sIncludeGuardDefinition, string sNamespaceDeclaration, string sTargetName, string sIndent)
		{
			var sHeaderFileBuilder = new StringBuilder();

			sHeaderFileBuilder.AppendLine();
			sHeaderFileBuilder.AppendLine("/*");
			sHeaderFileBuilder.Append(sIndent).Append(sNow.Year.ToString("0000")).Append('.').Append(sNow.Month.ToString("00")).Append('.').AppendLine(sNow.Day.ToString("00"));

			if (sAuthorName != null)
				sHeaderFileBuilder.Append(sIndent).Append("Created by ").Append(sAuthorName).AppendLine(".");

			sHeaderFileBuilder.AppendLine("*/");
			sHeaderFileBuilder.AppendLine();

			if (sIncludeGuardDefinition != null)
			{
				sHeaderFileBuilder.Append("#ifndef ").AppendLine(sIncludeGuardDefinition);
				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.Append("#define ").AppendLine(sIncludeGuardDefinition);
				sHeaderFileBuilder.AppendLine();
			}

			sHeaderFileBuilder.AppendLine("/*");
			sHeaderFileBuilder.Append(sIndent).AppendLine("TODO : Place the include directives here.");
			sHeaderFileBuilder.AppendLine("*/");
			sHeaderFileBuilder.AppendLine();
			sHeaderFileBuilder.AppendLine("#include <utility>");
			sHeaderFileBuilder.AppendLine();

			if (sNamespaceDeclaration != null)
			{
				sHeaderFileBuilder.Append("namespace ").AppendLine(sNamespaceDeclaration);
				sHeaderFileBuilder.AppendLine("{");

				this.generateClassHeader(sHeaderFileBuilder, sIndent, sTargetName);

				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.Append("}");
			}
			else
				this.generateClassHeader(sHeaderFileBuilder, string.Empty, sTargetName);

			if (sIncludeGuardDefinition != null)
			{
				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.Append("#endif");
			}

			return sHeaderFileBuilder;
		}

		private void generateClassHeader(StringBuilder sClassHeaderBuilder, string sIndent, string sTargetName)
		{
			sClassHeaderBuilder.Append(sIndent).Append("class ").AppendLine(sTargetName);
			sClassHeaderBuilder.Append(sIndent).AppendLine("{");
			sClassHeaderBuilder.Append(sIndent).AppendLine("private:");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the class member variable declarations here.");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).AppendLine("();");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append("(const ").Append(sTargetName).AppendLine(" &sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append("(").Append(sTargetName).AppendLine(" &&sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append('~').Append(sTargetName).AppendLine("();");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the declarations of other constructors here.");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append(" &operator=(const ").Append(sTargetName).AppendLine(" &sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append(" &operator=(").Append(sTargetName).AppendLine(" &&sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place other operator overloadings here.");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the member function declarations here.");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).Append("};");
		}

		private StringBuilder generateClassBody(DateTime sNow, string sAuthorName, string sIncludeGuardDefinition, string sNamespaceDeclaration, string sTargetName, string sIndent)
		{
			var sSourceFileBuilder = new StringBuilder();

			sSourceFileBuilder.AppendLine();
			sSourceFileBuilder.AppendLine("/*");
			sSourceFileBuilder.Append(sIndent).Append(sNow.Year.ToString("0000")).Append('.').Append(sNow.Month.ToString("00")).Append('.').AppendLine(sNow.Day.ToString("00"));

			if (sAuthorName != null)
				sSourceFileBuilder.Append(sIndent).Append("Created by ").Append(sAuthorName).AppendLine(".");

			sSourceFileBuilder.AppendLine("*/");
			sSourceFileBuilder.AppendLine();

			sSourceFileBuilder.Append("#include \"").Append(sTargetName).AppendLine(".h\"");
			sSourceFileBuilder.AppendLine();

			if (sNamespaceDeclaration != null)
			{
				sSourceFileBuilder.Append("namespace ").AppendLine(sNamespaceDeclaration);
				sSourceFileBuilder.AppendLine("{");

				this.generateClassBody(sSourceFileBuilder, sIndent, sTargetName);

				sSourceFileBuilder.AppendLine();
				sSourceFileBuilder.Append("}");
			}
			else
				this.generateClassBody(sSourceFileBuilder, string.Empty, sTargetName);

			return sSourceFileBuilder;
		}

		private void generateClassBody(StringBuilder sClassBodyBuilder, string sIndent, string sTargetName)
		{
			sClassBodyBuilder.Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("TODO : Place the static class member variable definitions here.");
			sClassBodyBuilder.Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sTargetName).Append("::").Append(sTargetName).AppendLine("()");
			sClassBodyBuilder.Append(sIndent).AppendLine("{");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the default constructor here.");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sTargetName).Append("::").Append(sTargetName).Append("(const ").Append(sTargetName).AppendLine(" &sSrc)");
			sClassBodyBuilder.Append(sIndent).AppendLine("{");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the implementation of the copy constructor here.");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sTargetName).Append("::").Append(sTargetName).Append("(").Append(sTargetName).AppendLine(" &&sSrc)");
			sClassBodyBuilder.Append(sIndent).AppendLine("{");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the implementation of the move constructor here.");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("TODO : Place the implementations of other constructors here.");
			sClassBodyBuilder.Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sTargetName).Append(" &").Append(sTargetName).Append("::operator=(const ").Append(sTargetName).AppendLine(" &sSrc)");
			sClassBodyBuilder.Append(sIndent).AppendLine("{");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("if (&sSrc == this)");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the implementation of the copy assignment operator here.");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndent).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sTargetName).Append(" &").Append(sTargetName).Append("::operator=(").Append(sTargetName).AppendLine(" &&sSrc)");
			sClassBodyBuilder.Append(sIndent).AppendLine("{");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("if (&sSrc == this)");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place the implementation of the move assignment operator here.");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndent).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("TODO : Implement other operator overloadings here.");
			sClassBodyBuilder.Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append(sIndent).AppendLine("/*");
			sClassBodyBuilder.Append(sIndent).Append(sIndent).AppendLine("TODO : Place the member function implementations here.");
			sClassBodyBuilder.Append(sIndent).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndent);
			sClassBodyBuilder.Append("}");
		}
	}
}