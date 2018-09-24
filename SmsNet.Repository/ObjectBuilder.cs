using SmsNet.Data.Drivers.Annotations;
using SmsNet.Data.Drivers;
using SmsNet.Repository.Data.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Data
{
	internal sealed class ObjectBuilder
	{
		private delegate void SetterHandler(object target, object[] values);
		private class PropertyHolder
		{
			public string Name;
			public PropertyInfo Property;
			public MethodInfo MethodInfo;
			public Delegate Setter;
		}
		private Type type;
		private IList<PropertyHolder> properties = new List<PropertyHolder>();
		private Func<object> ConstructorMethod;
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
			ConstructorInfo ctorInfo = type.GetConstructor(Type.EmptyTypes);
			if (ctorInfo == null)
				throw new UnauthorizedAccessException("Can't map a object without constructor");
			ConstructorMethod = Expression.Lambda<Func<object>>(Expression.New(ctorInfo)).Compile();
			PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

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
			if (ConstructorMethod == null)
				throw new Exception("No constructor method found, do you use Map first instead Bind ?");

			T target = (T)ConstructorMethod();

			foreach(QueryResult result in results)
			{
				PropertyHolder property = GetPropertyByColumn(result.Column);
				if (property == null)
					continue;
				property.MethodInfo.Invoke(target, new object[1] { result.Value });
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
			if (prop.GetCustomAttribute<NotMappingAttribute>() != null)
				return false;
			return true;
		}
	}
}
