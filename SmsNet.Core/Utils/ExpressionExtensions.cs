using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Utils
{
	public static class ExpressionExtensions
	{
		public static PropertyInfo GetPropertyInfo<TEntity,TProperty>(this Expression<Func<TEntity, TProperty>> expression)
		{
			MemberExpression exp = null;

			if (expression.Body is UnaryExpression)
			{
				var unExp = (UnaryExpression)expression.Body;
				if (unExp.Operand is MemberExpression)
					exp = (MemberExpression)unExp.Operand;
				else
					throw new ArgumentException("Invalid Expression Type");
			}
			else if (expression.Body is MemberExpression)
				exp = (MemberExpression)expression.Body;
			else
				throw new ArgumentException();

			if (exp.Member is FieldInfo)
				throw new ArgumentException("Invalid Expression, the param need to be a property instead a field");

			return exp.Member as PropertyInfo;
		}
		public static Func<TEntity,TProperty> GetGetterMethod<TEntity,TProperty>(this Expression<Func<TEntity, TProperty>> expression)
		{
			return expression.Compile();
		} 
		public static Action<TEntity,TProperty> GetSetterMethod<TEntity,TProperty>(this Expression<Func<TEntity,TProperty>> expression)
		{
			MethodInfo method = GetPropertyInfo(expression).SetMethod;
			return (Action<TEntity,TProperty>)method.CreateDelegate(typeof(Action<TEntity,TProperty>));
		}
	}
}
