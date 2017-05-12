using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace StringSerializer
{
	public class DelimitedString : BaseObjectString<DelimitedField>
	{
		private bool hasEnclosure;

		public char Enclosure
		{
			get;
			set;
		}

		public override DelimitedField[] Fields
		{
			get;
			set;
		}

		public char Separator
		{
			get;
			set;
		}

		public DelimitedString(char fieldSeparator)
		{
			this.Separator = fieldSeparator;
		}

		public DelimitedString(char fieldSeparator, char fieldEnclosure) : this(fieldSeparator)
		{
			this.Enclosure = fieldEnclosure;
			this.hasEnclosure = true;
		}

		public T Deserialize<T>(string objectString)
		where T : new()
		{
			string pattern = (this.hasEnclosure ? string.Format("{0}(?!(?:[^{1}{0}]|[^{1}]{0}[^{1}])+{1})", this.Separator, this.Enclosure) : string.Format("", this.Separator));
			string[] stringFields = Regex.Split(objectString, string.Format(pattern, "\""));
			for (int i = 0; i < (int)stringFields.Length; i++)
			{
				stringFields[i] = stringFields[i].Trim(new char[] { this.Enclosure });
			}
			return base.Deserialize<T>(stringFields);
		}

		public string Serialize(object obj)
		{
			string str;
			Type type = obj.GetType();
			StringBuilder sb = new StringBuilder();
			IOrderedEnumerable<DelimitedField> fields = 
				from a in (IEnumerable<DelimitedField>)this.Fields
				orderby a.Position
				select a;
			int cnt = 0;
			int fieldCount = fields.Count<DelimitedField>();
			foreach (DelimitedField field in fields)
			{
				cnt++;
				string fieldValue = type.GetProperty(field.PropertyName).GetValue(obj, null).ToString();
				StringBuilder stringBuilder = sb;
				if (this.hasEnclosure)
				{
					string str1 = this.Enclosure.ToString();
					char enclosure = this.Enclosure;
					str = string.Concat(str1, fieldValue, enclosure.ToString());
				}
				else
				{
					str = fieldValue;
				}
				stringBuilder.Append(str);
				if (cnt == fieldCount)
				{
					continue;
				}
				sb.Append(this.Separator);
			}
			return sb.ToString();
		}
	}
}