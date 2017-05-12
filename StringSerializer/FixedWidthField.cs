using System;
using System.Runtime.CompilerServices;

namespace StringSerializer
{
	public class FixedWidthField : BaseObjectField
	{
		private System.Type _type;

		public int Length
		{
			get;
			set;
		}

		public int StartingPosition
		{
			get;
			set;
		}

		public override System.Type Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		public FixedWidthField()
		{
		}

		public FixedWidthField(int startingPosition, int length, string propertyName)
		{
			this.StartingPosition = startingPosition;
			this.Length = length;
			this.PropertyName = propertyName;
		}

		public FixedWidthField(int startingPosition, int length, string propertyName, System.Type type) : this(startingPosition, length, propertyName)
		{
			this.Type = type;
		}

		public FixedWidthField(int startingPosition, int length, string propertyName, string type) : this(startingPosition, length, propertyName, System.Type.GetType(type))
		{
		}
	}
}