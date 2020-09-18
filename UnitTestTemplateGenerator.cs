using HandlebarsDotNet;
using System.IO;
using System.Reflection;

namespace UnitTestTemplateGenerator
{
	class UnitTestTemplateGenerator
	{
		public static string Generate(string cutSourceCode)
		{
			var (usings, namespaceNode, classNode, ctorNode) = SourceCodeParser.ParseSourceCode(cutSourceCode);

			var viewModel = UnitTestViewModelFactory.GetViewModel(usings, namespaceNode, classNode, ctorNode);

			var generatedCode = RenderUnitTestSourceCode(viewModel);

			return generatedCode;
		}

		private static string RenderUnitTestSourceCode(UnitTestCodeViewModel viewModel)
		{
			var templateText = GetUnitTestSourceCodeTemplate();

			var template = Handlebars.Compile(templateText);

			var unitTestSourceCode = template(viewModel);

			return unitTestSourceCode;
		}

		private static string GetUnitTestSourceCodeTemplate()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "UnitTestTemplateGenerator.Templates.UnitTestSourceCodeTemplate.hbt";

			using Stream stream = assembly.GetManifestResourceStream(resourceName);
			using StreamReader reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
	}
}
