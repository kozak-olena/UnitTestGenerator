using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestTemplateGenerator
{
	class SourceCodeParser
	{
		public static (IEnumerable<UsingDirectiveSyntax> Usings, NamespaceDeclarationSyntax NamespaceNode, ClassDeclarationSyntax ClassNode, ConstructorDeclarationSyntax CtorNode) ParseSourceCode(string sourceCode)
		{
			var tree = CSharpSyntaxTree.ParseText(sourceCode);

			// non-CompilationUnitSyntax roots are not supported
			var root = (CompilationUnitSyntax)tree.GetRoot();

			// multinamespace source files not supported
			var namespaceNode = root.Members.OfType<NamespaceDeclarationSyntax>().Single();

			// multiclass source files not supported
			var classNode = namespaceNode.Members.OfType<ClassDeclarationSyntax>().Single();

			// multictor classes not supported
			var ctorNode = classNode.Members.OfType<ConstructorDeclarationSyntax>().SingleOrDefault();

			return (root.Usings, namespaceNode, classNode, ctorNode);
		}
	}
}
