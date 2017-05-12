using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace StringSerializer
{
	public class FixedWidthString : BaseObjectString<FixedWidthField>
	{
		public override FixedWidthField[] Fields
		{
			get;
			set;
		}

		public FixedWidthString()
		{
		}

		public FixedWidthString(FixedWidthField[] fields)
		{
			this.Fields = fields;
		}

		public FixedWidthString(List<FixedWidthField> fields) : this(fields.ToArray())
		{
		}

		public T Deserialize<T>(string objectString)
		where T : new()
		{
			string[] stringFields = new string[(int)this.Fields.Length];
			for (int i = 0; i < (int)this.Fields.Length; i++)
			{
				FixedWidthField field = this.Fields[i];
				field.Index = i;
				stringFields[i] = objectString.Substring(field.StartingPosition, field.Length).Trim();
			}
			return base.Deserialize<T>(stringFields);
		}

		public string Serialize(object obj)
		{
			Type type = obj.GetType();
			FixedWidthField maxField = this.Fields.First<FixedWidthField>((FixedWidthField a) => a.StartingPosition == ((IEnumerable<FixedWidthField>)this.Fields).Max<FixedWidthField>((FixedWidthField b) => b.StartingPosition));
			StringBuilder sb = new StringBuilder(new string(' ', maxField.StartingPosition + maxField.Length));
			for (int i = 0; i < (int)this.Fields.Length; i++)
			{
				FixedWidthField field = this.Fields[i];
				sb.Remove(field.StartingPosition, field.Length);
				string fieldValue = type.GetProperty(field.PropertyName).GetValue(obj, null).ToString();
				if (fieldValue.Length == field.Length)
				{
					sb.Insert(field.StartingPosition, fieldValue);
				}
				else if (fieldValue.Length <= field.Length)
				{
					sb.Insert(field.StartingPosition, fieldValue.PadRight(field.Length));
				}
				else
				{
					sb.Insert(field.StartingPosition, fieldValue.Remove(field.Length));
				}
			}
			return sb.ToString();
		}
	}
}