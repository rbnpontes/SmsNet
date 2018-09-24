using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Data.Drivers
{
	public interface IDatabaseDriver : IDisposable
	{
		void Open(string connectionString);
		void Close();
		void Fetch(string query,Action<QueryResult[]> callback);
		void Fetch(string query,QueryParameter[] parameters, Action<QueryResult[]> callback);
		void Exec(string query);
		void Exec(string query, QueryParameter[] parameters);
	}
}
