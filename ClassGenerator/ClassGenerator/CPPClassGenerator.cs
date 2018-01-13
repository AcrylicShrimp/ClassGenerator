
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
			sHeaderFileBuilder.AppendLine();
			sHeaderFileBuilder.AppendLine("#include <utility>");
			sHeaderFileBuilder.AppendLine();

			if (sNamespaceDeclaration != null)
			{
				sHeaderFileBuilder.Append("namespace ").AppendLine(sNamespaceDeclaration);
				sHeaderFileBuilder.AppendLine("{");

				this.generateClassHeader(sHeaderFileBuilder, sIndent, sIndent, sTargetName);

				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.Append("}");
			}
			else
				this.generateClassHeader(sHeaderFileBuilder, string.Empty, sIndent, sTargetName);

			if (sIncludeGuardDefinition != null)
			{
				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.AppendLine();
				sHeaderFileBuilder.Append("#endif");
			}

			return sHeaderFileBuilder;
		}

		private void generateClassHeader(StringBuilder sClassHeaderBuilder, string sIndentFirst, string sIndentSecond, string sTargetName)
		{
			sClassHeaderBuilder.Append(sIndentFirst).Append("class ").AppendLine(sTargetName);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine("{");
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine("private:");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the class member variable declarations here.");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sTargetName).AppendLine("();");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sTargetName).Append("(const ").Append(sTargetName).AppendLine(" &sSrc);");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sTargetName).Append("(").Append(sTargetName).AppendLine(" &&sSrc);");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append('~').Append(sTargetName).AppendLine("();");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the declarations of other constructors here.");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sTargetName).Append(" &operator=(const ").Append(sTargetName).AppendLine(" &sSrc);");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sTargetName).Append(" &operator=(").Append(sTargetName).AppendLine(" &&sSrc);");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place other operator overloadings here.");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the member function declarations here.");
			sClassHeaderBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassHeaderBuilder.Append(sIndentFirst).Append("};");
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

				this.generateClassBody(sSourceFileBuilder, sIndent, sIndent, sTargetName);

				sSourceFileBuilder.AppendLine();
				sSourceFileBuilder.Append("}");
			}
			else
				this.generateClassBody(sSourceFileBuilder, string.Empty, sIndent, sTargetName);

			return sSourceFileBuilder;
		}

		private void generateClassBody(StringBuilder sClassBodyBuilder, string sIndentFirst, string sIndentSecond, string sTargetName)
		{
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("TODO : Place the static class member variable definitions here.");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append("::").Append(sTargetName).AppendLine("()");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the default constructor here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append("::").Append(sTargetName).Append("(const ").Append(sTargetName).AppendLine(" &sSrc)");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the implementation of the copy constructor here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append("::").Append(sTargetName).Append("(").Append(sTargetName).AppendLine(" &&sSrc)");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the implementation of the move constructor here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append("::~").Append(sTargetName).AppendLine("()");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the implementation of the destructor here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("TODO : Place the implementations of other constructors here.");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append(" &").Append(sTargetName).Append("::operator=(const ").Append(sTargetName).AppendLine(" &sSrc)");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("if (&sSrc == this)");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the implementation of the copy assignment operator here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).Append(sTargetName).Append(" &").Append(sTargetName).Append("::operator=(").Append(sTargetName).AppendLine(" &&sSrc)");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("{");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("if (&sSrc == this)");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).Append(sIndentSecond).AppendLine("TODO : Place the implementation of the move assignment operator here.");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine(sIndentSecond);
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("return *this;");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("}");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("TODO : Implement other operator overloadings here.");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("*/");
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.AppendLine(sIndentFirst);
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("/*");
			sClassBodyBuilder.Append(sIndentFirst).Append(sIndentSecond).AppendLine("TODO : Place the member function implementations here.");
			sClassBodyBuilder.Append(sIndentFirst).AppendLine("*/");
			sClassBodyBuilder.Append(sIndentFirst);
		}
	}
}