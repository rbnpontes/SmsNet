﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using SmsNet.Data.Drivers;

namespace SmsNet.Data
{
    public abstract class DatabaseContext : IDisposable
    {
		private string mConnectionString;
		private bool mConnected;
		private IDatabaseDriver mDriver;
		public IDatabaseDriver Driver {
			get {
				return mDriver;
			}
			set
			{
				if (mConnected)
					throw new UnauthorizedAccessException("Could not Change driver because connection has Initialized");
				this.mDriver = value;
			}
		}
		public DatabaseContext(string connectionString)
		{
			this.mConnectionString = connectionString;
			/// By Default, use Sql Driver
			this.mDriver = new SqlDriver();
		}
		public void Dispose()
		{
			if (mConnected)
				this.Close();
		}
		public virtual void Connect()
		{
			mDriver.Open(mConnectionString);
		}
		public virtual void Close()
		{
			this.mDriver.Close();
			mConnected = false;
		}
		public virtual IReadOnlyList<T> Fetch<T>(string query)
		{
			List<T> result = new List<T>();

			/// Initialize ObjectBuilder
			/// This is used for serialize Driver Database data into object
			ObjectBuilder factory = ObjectBuilder.Create();

			/// Make a Mape of Object
			factory.Map<T>();
			/// Each driver loop, the code will call factory Bind for 
			/// instantiate a 'T' object and fill with driver data. 
			mDriver.Fetch(query, x => result.Add(factory.Bind<T>(x)));
			return result;
		}
		/// <summary>
		/// Fetch data from Database using Query
		/// Use @p0, @p1, @p2... if you want filter parameters
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="query"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public virtual IReadOnlyList<T> Fetch<T>(string query,params object[] parameters)
		{
			QueryParameter[] queryParams = new QueryParameter[parameters.Length];
			for(int i=0;i<queryParams.Length;i++)
			{
				queryParams[i] = new QueryParameter();
				queryParams[i].Name = "@p" + i;
				queryParams[i].Value = parameters[i];
			}
			List<T> result = new List<T>();

			/// Initialize ObjectBuilder
			/// This is used for serialize Driver Database data into object
			ObjectBuilder factory = ObjectBuilder.Create();

			/// Make a Mape of Object
			factory.Map<T>();
			/// Each driver loop, the code will call factory Bind for 
			/// instantiate a 'T' object and fill with driver data. 
			mDriver.Fetch(query,queryParams, x => result.Add(factory.Bind<T>(x)));
			return result;
		}
		public virtual void Fetch(string query, Action<QueryResult[]> callback)
		{
			mDriver.Fetch(query, callback);
		}
		public virtual void Fetch(string query, Action<QueryResult[]> callback, params object[] parameters)
		{
			QueryParameter[] queryParams = new QueryParameter[parameters.Length];
			for (int i = 0; i < queryParams.Length; i++)
			{
				queryParams[i] = new QueryParameter();
				queryParams[i].Name = "@p" + i;
				queryParams[i].Value = parameters[i];
			}

			mDriver.Fetch(query,queryParams, callback);
		}
	}
}
