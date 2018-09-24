using SmsNet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Network
{
	public class Request : IFactory
	{
		private class RequestHandler : IRequestHandler
		{
			public Action<string, object> SuccesssAction;
			public Action<string, Exception> FailAction;

			public RequestHandler(Request request)
			{
			}

			public IRequestHandler Error(Action<string, Exception> callback)
			{
				FailAction = callback;
				return this;
			}

			public IRequestHandler Success(Action<string, object> callback)
			{
				SuccesssAction = callback;
				return this;
			}
			public void DispatchSuccess(string url, object data)
			{
				SuccesssAction?.Invoke(url, data);
			}
			public void DispatchError(string url, Exception e)
			{
				FailAction?.Invoke(url, e);
			}
		}

		private HttpClient client;
		public void OnCreateFactory()
		{
			client = new HttpClient();
		}
		public IRequestHandler Get(string url)
		{
			RequestHandler handler = new RequestHandler(this);
			TryGet(url, handler);
			return handler;
		}
		public IRequestHandler Post(string url, object parameter = null)
		{
			/// If parameter is null, initialize like anonymous
			if(parameter == null)
				parameter = new { };
			RequestHandler handler = new RequestHandler(this);
			TryPost(url, parameter, handler);
			return handler;
		}
		private async void TryGet(string url, RequestHandler handler)
		{
			string value = string.Empty;
			try
			{
				value = await client.GetStringAsync(url);
				handler.DispatchSuccess(url, value);
			}catch(Exception e)
			{
				handler.DispatchError(url, e);
			}
		}
		private async void TryPost(string url, object parameter, RequestHandler handler)
		{
			string value = string.Empty;
			try
			{
				var response = await client.PostAsync(url, new StringContent(parameter.ToJson(),Encoding.Default,"application/json"));
				value = await response.Content.ReadAsStringAsync();
				handler.DispatchSuccess(url, value);
			}
			catch (Exception e)
			{
				handler.DispatchError(url, e);
			}
		}
	}
}
