using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UnitTestTemplateGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			var cutSourceCodeFilePath = ValidateParameters(args);

			var sourceCode = File.ReadAllText(cutSourceCodeFilePath);

			var generatedCode = UnitTestTemplateGenerator.Generate(sourceCode);

			ShowGeneratedCode(generatedCode);
		}

		static string ValidateParameters(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Single parameter is required - path to cut source code file.");
				Environment.Exit(1);
			}

			var cutSourceCodeFilePath = args.Single();

			if (!File.Exists(cutSourceCodeFilePath))
			{
				Console.WriteLine($"Could not find file '{cutSourceCodeFilePath}'. Make sure path is valid.");
				Environment.Exit(1);
			}

			return cutSourceCodeFilePath;
		}

		static void ShowGeneratedCode(string code)
		{
			var filePath = Path.GetTempFileName();
			try
			{
				File.WriteAllText(filePath, code);

				Process.Start("notepad.exe", filePath).WaitForExit();
			}
			finally
			{
				File.Delete(filePath);
			}
		}
	}
}
