using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UnitTestTemplateGenerator
{
    public static class TypeSyntaxService
    {
        public static string GetTypeString(TypeSyntax type)
        {
            if (type is IdentifierNameSyntax identifierName)
            {
                return identifierName.Identifier.Text;
            }
            // int, bool, string, etc...
            else if (type is PredefinedTypeSyntax predefinedType)
            {
                return predefinedType.Keyword.Text;
            }
            else if (type is GenericNameSyntax genericType)
            {
                var typeName = genericType.Identifier.Text;

                var argumentNames = genericType.TypeArgumentList.Arguments.Select(GetTypeString);

                return $"{typeName}<{string.Join(", ", argumentNames)}>";
            }
            else if (type is ArrayTypeSyntax arrayType)
            {
                var elementType = GetTypeString(arrayType.ElementType);

                return $"{elementType}[]";
            }
            else
            {
                throw new NotSupportedException($"{nameof(TypeSyntax)}'s subtype {type.GetType().FullName} is not supported.");
            }
        }
    }
}
