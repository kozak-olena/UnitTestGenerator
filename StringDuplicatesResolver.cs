using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestTemplateGenerator
{
    class StringDuplicatesResolver
    {
        private readonly IEqualityComparer<string> _equalityComparer;

        private readonly List<string> _stringsProcessedSoFar = new List<string>();

        public StringDuplicatesResolver(IEqualityComparer<string> equalityComparer = null)
        {
            _equalityComparer = equalityComparer ?? StringComparer.Ordinal;
        }
        public int? GeneratePostfixThatWillMakeStringUniqueOrDefault(string str)
        {
            var currentStr = str;
            int? uniquePostfix = null;

            while (_stringsProcessedSoFar.Contains(currentStr, _equalityComparer))
            {
                uniquePostfix ??= 0;

                uniquePostfix++;

                currentStr = string.Concat(str, uniquePostfix);
            }

            _stringsProcessedSoFar.Add(currentStr);

            return uniquePostfix;
        }
    }
}
