using System.Collections.Generic;

namespace UnitTestTemplateGenerator
{
	public class UnitTestCodeViewModel
	{
		public string Usings { get; set; }
		public string CutName { get; set; }
		public string CutNamespace { get; set; }
		public IEnumerable<UnitTestMethodParameterViewModel> CtorParams { get; set; }
		public IEnumerable<UnitTestMethodCodeViewModel> MethodsToTest { get; set; }
		public string Namespace { get; set; }
	}
}
