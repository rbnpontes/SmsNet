using SmsNet.Repository.Data.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.SimpleAppTest
{
	class UserTest{
		[Column("ID")]
		public int Id { get; set; }
		[Column("NAME")]
		public string Name { get; set; }
		[Column("USERNAME")]
		public string Username { get; set; }
	}
	class Program
	{
		static void Main(string[] args)
		{
			using(AppDatabase app = new AppDatabase())
			{
				app.Connect();
				UserTest[] users = app.Fetch<UserTest>("SELECT * FROM USERS").Where(x => x.Id == 1).ToArray();
				foreach (var user in users)
					Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Username: {user.Username}");
			}
			Console.ReadKey();
		}
	}
}
