using SmsNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.SimpleAppTest
{
	class AppDatabase : DatabaseContext
	{
		public AppDatabase() : base("Server=localhost;Database=Sms;Trusted_Connection=true")
		{
		}
	}
}
