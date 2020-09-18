using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestTemplateGenerator
{
    class UnitTestViewModelFactory
    {
        public static UnitTestCodeViewModel GetViewModel(IEnumerable<UsingDirectiveSyntax> usings, NamespaceDeclarationSyntax namespaceNode, ClassDeclarationSyntax classNode, ConstructorDeclarationSyntax ctorNode)
        {
            var ctorParamList = GetMethodParamsViewModels(ctorNode?.ParameterList.Parameters ?? Enumerable.Empty<ParameterSyntax>());

            var methodsToTest = GetMethodViewModels(classNode.Members.OfType<MethodDeclarationSyntax>());

            var cutNamespace = namespaceNode.Name.ToString();

            var unitTestNamespace = UnitTestNamespaceGenerator.GenerateBasedOnCutNamespace(namespaceNode.Name.ToString());

            var viewModel =
                new UnitTestCodeViewModel
                {
                    Usings = usings.ToString(),
                    CutName = classNode.Identifier.Text,
                    CutNamespace = cutNamespace,
                    CtorParams = ctorParamList,
                    MethodsToTest = methodsToTest,
                    Namespace = unitTestNamespace
                };

            return viewModel;
        }

        private static List<UnitTestMethodParameterViewModel> GetMethodParamsViewModels(IEnumerable<ParameterSyntax> parameters)
        {
            var ctorParamList =
                parameters
                    .Select
                    (
                        x =>
                            new UnitTestMethodParameterViewModel
                            {
                                Type = TypeSyntaxService.GetTypeString(x.Type),
                                Name = x.Identifier.Text
                            }
                    )
                    .ToList();

            return ctorParamList;
        }

        private static List<UnitTestMethodCodeViewModel> GetMethodViewModels(IEnumerable<MethodDeclarationSyntax> methodNodes)
        {
            var nameDuplicatesResolver = new StringDuplicatesResolver();

            var methodsToTest =
                methodNodes
                    .Where(x => x.Modifiers.Any(y => y.Text == "public"))
                    .Select
                    (
                        x =>
                            new UnitTestMethodCodeViewModel
                            {
                                Name = x.Identifier.Text,
                                UniquePostfixForName = nameDuplicatesResolver.GeneratePostfixThatWillMakeStringUniqueOrDefault(x.Identifier.Text),
                                IsAsync = x.Modifiers.Any(y => y.Text == "async"),
                                HasReturn = !IsMethodWithNoReturn(x),
                                Params = GetMethodParamsViewModels(x.ParameterList.Parameters)
                            }
                    )
                    .ToList();

            return methodsToTest;
        }

        private static bool IsMethodWithNoReturn(MethodDeclarationSyntax method)
        {
            var type = TypeSyntaxService.GetTypeString(method.ReturnType);

            var result = type == "void" || type == "Task";

            return result;
        }
    }
}
