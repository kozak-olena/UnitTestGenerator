using System;
using System.Linq;

namespace UnitTestTemplateGenerator
{
    class UnitTestNamespaceGenerator
    {
        // TODO: move to config, etc.
        //private const string ConventionNamespacePrefix = "<your namespace here>";
        //private const string ConventionUnitTestNamespacePartToInsert = "UnitTests";

        public static string GenerateBasedOnCutNamespace(string cutNamespace)
        {
            //TODO: Uncomment and fix


            //if (!cutNamespace.StartsWith(ConventionNamespacePrefix))
            //{
            //	return string.Empty;
            //}

            //var cutNamespaceParts =
            //	cutNamespace
            //		.Substring(ConventionNamespacePrefix.Length)
            //		.Split('.', StringSplitOptions.RemoveEmptyEntries);

            //if (!cutNamespace.Any())
            //{
            //	return string.Empty;
            //}

            //var unitTestNamespace = 
            //	string.Concat
            //	(
            //		ConventionNamespacePrefix, 
            //		".", 
            //		cutNamespaceParts.First(), 
            //		".", 
            //		string.Join(".", new[] { ConventionUnitTestNamespacePartToInsert }.Concat(cutNamespaceParts.Skip(1)))
            //	);

            //return unitTestNamespace;
            return $"{cutNamespace}.Tests";
        }
    }
}
