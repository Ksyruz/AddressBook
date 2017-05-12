using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StringSerializer
{
	public class BaseObjectString<T>
	where T : BaseObjectField
	{
		public virtual T[] Fields
		{
			get;
			set;
		}

		public BaseObjectString()
		{
		}

		public virtual U Deserialize<U>(string[] stringFields)
		where U : new()
		{
			object obj;
			U item = Activator.CreateInstance<U>();
			Type type = typeof(U);
			T[] fields = this.Fields;
			for (int i = 0; i < (int)fields.Length; i++)
			{
				T field = fields[i];
				string fieldString = stringFields[field.Index];
				if (field.Type == null)
				{
					obj = fieldString;
				}
				else
				{
					obj = Convert.ChangeType(fieldString, field.Type);
				}
				object fieldValue = obj;
				type.GetProperty(field.PropertyName).SetValue(item, fieldValue, null);
			}
			return item;
		}
	}
}