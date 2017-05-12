using System;
using System.Runtime.CompilerServices;

namespace StringSerializer
{
	public class DelimitedField : BaseObjectField
	{
		public int Position
		{
			get;
			set;
		}

		public override System.Type Type
		{
			get;
			set;
		}

		public DelimitedField(int position, string propertyName)
		{
			this.Position = position;
			base.Index = position;
			this.PropertyName = propertyName;
		}

		public DelimitedField(int position, string propertyName, System.Type type) : this(position, propertyName)
		{
			this.Type = type;
		}

		public DelimitedField(int position, string propertyName, string type) : this(position, propertyName, System.Type.GetType(type))
		{
		}
	}
}