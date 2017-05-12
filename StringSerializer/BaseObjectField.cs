using System;
using System.Runtime.CompilerServices;

namespace StringSerializer
{
	public class BaseObjectField
	{
		internal int Index
		{
			get;
			set;
		}

		public virtual string PropertyName
		{
			get;
			set;
		}

		public virtual System.Type Type
		{
			get;
			set;
		}

		public BaseObjectField()
		{
		}
	}
}