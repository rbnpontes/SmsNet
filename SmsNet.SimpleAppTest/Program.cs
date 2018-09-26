using SmsNet.Core;
using SmsNet.Core.Animation;
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
		static void Main(string[] args)
		{
			Context.Singleton.RegisterFactory<ConfigManager>();
			Context.Singleton.RegisterFactory<Request>();
			Context.Singleton.RegisterFactory<Animator>();

			Console.WriteLine(Context.Singleton.GetSubsystem<ConfigManager>().Load<DatabaseConfig>("Data/db.conf.json").ToJson());
			Context.Singleton.AddListener("E_TEST", TestEvent).SendEvent("E_TEST");
			Context.Singleton.GetSubsystem<Request>()
				.Get("https://jsonplaceholder.typicode.com/todos/1").Success((url, data) => Console.WriteLine(data));
			IAnimate<Foo> anim = Context.Singleton.GetSubsystem<Animator>().Bind(new Foo());
			anim.Begin().SetDuration(1000).From(x => x.X).To(x => 100).SetEasingType(new SineEasing()).SetOnAnimateListener((source,from,to,alpha)=> {
				Console.Write($"{source.X}, ");
			}).End();
			Context.Singleton.GetSubsystem<Animator>().Play(anim);
			Console.ReadKey();
		}
	}
}
  