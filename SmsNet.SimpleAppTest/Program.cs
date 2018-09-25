using SmsNet.Core;
using SmsNet.Core.Network;
using SmsNet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.SimpleAppTest
{
	class Foo
	{
		public int X { get; set; }
	}
	class Program
	{
		static void TestEvent(object sender, object[] parameters)
		{
			Console.WriteLine("Hello World!!!");
		}
		static void TestExpression<TEntity,TProperty>(TEntity obj,Expression<Func<TEntity, TProperty>> expression)
		{
			Console.WriteLine(expression.GetGetterMethod().Invoke(obj));
			Delegate @delegate = expression.GetSetterMethod();
			@delegate.DynamicInvoke(new object[] { obj, 20 });
		}
		static void Main(string[] args)
		{
			Foo foo = new Foo();
			foo.X = 10;
			TestExpression(foo, x => x.X);
			Console.WriteLine(foo.X);
			Context.Singleton.RegisterFactory<ConfigManager>();
			Context.Singleton.RegisterFactory<Request>();
			Console.WriteLine(Context.Singleton.GetSubsystem<ConfigManager>().Load<DatabaseConfig>("Data/db.conf.json").ToJson());
			Context.Singleton.AddListener("E_TEST", TestEvent).SendEvent("E_TEST");
			Context.Singleton.GetSubsystem<Request>()
				.Get("https://jsonplaceholder.typicode.com/todos/1").Success((url, data) => Console.WriteLine(data));
			Console.ReadKey();
		}
	}
}
  