using System;

namespace StringSerializer.Constants
{
	public class DelimitedExpressions
	{
		public const string WITHENCLOSURES = "{0}(?!(?:[^{1}{0}]|[^{1}]{0}[^{1}])+{1})";

		public const string WITHOUTENCLOSURES = "";

		public DelimitedExpressions()
		{
		}
	}
}