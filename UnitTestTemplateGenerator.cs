using HandlebarsDotNet;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace UnitTestTemplateGenerator
{
    class UnitTestTemplateGenerator
    {
        public static string Generate(string cutSourceCode)
        {
            var (usings, namespaceNode, classNode, ctorNode) = SourceCodeParser.ParseSourceCode(cutSourceCode);

            var viewModel = UnitTestViewModelFactory.GetViewModel(usings, namespaceNode, classNode, ctorNode);

            var generatedCode = RenderUnitTestSourceCode(viewModel);

            var normalizedCode = Normalize(generatedCode);

            return normalizedCode;
        }

        private static string DetectOSLineEnding()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return "\n";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "\r\n";
            }
            else
            {
                throw new NotSupportedException("Current OS is not supported");
            }
        }

        private static string Normalize(string generatedCode)
        {
            string currentOsEnding = DetectOSLineEnding();

            return Regex.Replace(generatedCode, @"\r\n|\n\r|\n|\r", currentOsEnding);
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
