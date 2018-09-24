using SmsNet.Data.Drivers.Annotations;
using SmsNet.Repository.Data.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Data.Drivers
{
	internal sealed class ObjectBuilder
	{
		private class PropertyHolder
		{
			public string Name;
			public PropertyInfo Property;
			public MethodInfo MethodInfo;
			public Delegate Setter;
		}
		private Type type;
		private IList<PropertyHolder> properties = new List<PropertyHolder>();
		private ObjectBuilder()
		{
		}
		public static ObjectBuilder Create()
		{
			return new ObjectBuilder();
		}
		
		public ObjectBuilder Map<T>()
		{
			return Map(typeof(T));
		}
		public ObjectBuilder Map(Type type)
		{
			this.type = type;

			PropertyInfo[] props = type.GetProperties(BindingFlags.Public);

			foreach (PropertyInfo prop in props)
			{
				if (!CheckValidProperty(prop))
					continue;
				PropertyHolder property = new PropertyHolder();
				property.Property = prop;
				property.Name = prop.Name;

				ColumnAttribute columnName = prop.GetCustomAttribute<ColumnAttribute>();
				if (columnName != null)
					property.Name = columnName.Name;

				MethodInfo method = prop.GetSetMethod();
				/// Cache Setter Method for fast reflection
				/// Save method is used to bypass security check in reflection
				property.MethodInfo = method;
				properties.Add(property);
			}
			return this;
		}
		
		public T Bind<T>(QueryResult[] results)
		{
			T target = default(T);

			foreach(QueryResult result in results)
			{
				PropertyHolder property = GetPropertyByColumn(result.Column);
				if (property == null)
					continue;
				if(property.Setter == null)
					property.Setter = (Action<T, object>)Delegate.CreateDelegate(typeof(Action<T, object>), null, property.MethodInfo);
				
				/// Invoke Setter Method
				((Action<T, object>)property.Setter)(target, result.Value);
			}

			return target;
		}

		private PropertyHolder GetPropertyByColumn(string column)
		{
			foreach(PropertyHolder prop in properties)
			{
				if (prop.Name.Equals(column))
					return prop;
			}
			return null;
		}
		private bool CheckValidProperty(PropertyInfo prop)
		{
			if (!prop.CanWrite)
				return false;
			if (prop.GetCustomAttribute<NotMappingAttribute>() == null)
				return false;
			return true;
		}
	}
}
