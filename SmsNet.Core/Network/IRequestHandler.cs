using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Network
{
	public interface IRequestHandler
	{
		IRequestHandler Success(Action<string, object> callback);
		IRequestHandler Error(Action<string, Exception> callback);
	}
}
