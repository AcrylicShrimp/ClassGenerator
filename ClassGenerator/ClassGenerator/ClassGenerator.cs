
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassGenerator
{
	public class ClassGenerator : Generator
	{
		public string GenerationTypeName { get { return "Class"; } }

		public Dictionary<string, StringBuilder> generate(string sAuthorName, string sIncludeGuardDefinition, string sNamespaceDeclaration, string sTargetName, bool bUseSpaceForIndentation)
		{
			var sResult = new Dictionary<string, StringBuilder>();

			var sHeaderFileBuilder = new StringBuilder();
			var sClassFileBuilder = new StringBuilder();

			var sIndent = "\t";

			if (bUseSpaceForIndentation)
				sIndent = "    ";

			var sCurrentDate = DateTime.Now.ToLocalTime();

			sHeaderFileBuilder.AppendLine();
			sHeaderFileBuilder.AppendLine("/*");
			sHeaderFileBuilder.Append(sIndent).Append(sCurrentDate.Year.ToString("0000")).Append('.').Append(sCurrentDate.Month.ToString("00")).Append('.').AppendLine(sCurrentDate.Day.ToString("00"));

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

			sResult.Add(string.Format("{0}.h", sTargetName), sHeaderFileBuilder);
			sResult.Add(string.Format("{0}.cpp", sTargetName), sClassFileBuilder);

			return sResult;
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
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place other constructors here.");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("*/");
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine(sIndent);
			sClassHeaderBuilder.Append(sIndent).AppendLine("public:");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append(" &operator=(const ").Append(sTargetName).AppendLine(" &sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sTargetName).Append(" &operator=(").Append(sTargetName).AppendLine(" &&sSrc);");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).AppendLine("/*");
			sClassHeaderBuilder.Append(sIndent).Append(sIndent).Append(sIndent).AppendLine("TODO : Place other operator overloading here.");
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

		private void generateClassBody(StringBuilder sClassHeaderBuilder, string sIndent, string sTargetName)
		{

		}
	}
}