using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Data.Drivers
{
	public class SqlDriver : IDatabaseDriver
	{
		private SqlConnection mConnection = new SqlConnection();
		private bool mOpened = false;

		public void Dispose()
		{
			if(this.mOpened)
				this.Close();
			mConnection.Dispose();
		}
		public void Close()
		{
			if (!mOpened)
				throw new UnauthorizedAccessException("Can't close connection, because this has not opened");

			mConnection.Close();
			this.mOpened = false;
		}
		public void Open(string connectionString)
		{
			if (mOpened)
				throw new UnauthorizedAccessException("Could not open a Connection because connection has opened");
			mConnection.ConnectionString = connectionString;
			mConnection.Open();
			mOpened = true;
		}
		public void Exec(string query)
		{
			this.Exec(query, null);
		}
		public void Exec(string query, QueryParameter[] parameters)
		{
			GetCommand(query, parameters).ExecuteNonQuery();
		}
		public void Fetch(string query, Action<QueryResult[]> callback)
		{
			this.Fetch(query, null, callback);
		}
		public void Fetch(string query, QueryParameter[] parameters, Action<QueryResult[]> callback)
		{
			if (callback == null)
				throw new ArgumentNullException("callback param is null");

			SqlCommand command = GetCommand(query,parameters);
			using (SqlDataReader reader = command.ExecuteReader())
			{
				string[] columns = null;
				QueryResult[] results = null;
				while (reader.Read())
				{
					/// Get column names for mount QueryResult
					if (columns == null)
					{
						columns = GetColumns(reader);
						results = new QueryResult[columns.Length];
					}

					for (int i = 0; i < results.Length; i++)
					{
						/// Instead allocate a new QueryResult, use the same
						/// for improving perfomance
						if (results[i] == null)
						{
							results[i] = new QueryResult();
							results[i].Column = columns[i];
						}
						results[i].Value = reader[i];
					}

					/// Invoke Callback
					callback(results);
				}
			}
		}
		private SqlCommand GetCommand(string query, QueryParameter[] parameters =null)
		{
			SqlCommand command = new SqlCommand(query, mConnection);
			if (parameters == null)
				return command;
			foreach (QueryParameter param in parameters)
				command.Parameters.Add(new SqlParameter(param.Name, param.Value));
			return command;
		}
		private string[] GetColumns(SqlDataReader reader)
		{
			string[] columns = new string[reader.FieldCount];
			for (int i = 0; i < columns.Length; i++)
				columns[i] = reader.GetName(i);
			return columns;
		}
	}
}
