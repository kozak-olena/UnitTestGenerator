using System.Collections.Generic;

namespace UnitTestTemplateGenerator
{
	public class UnitTestMethodCodeViewModel
	{
		public string Name { get; set; }
		public int? UniquePostfixForName { get; set; }
		public bool IsAsync { get; set; }
		public bool HasReturn { get; set; }
		public IEnumerable<UnitTestMethodParameterViewModel> Params { get; set; }
    }
}
