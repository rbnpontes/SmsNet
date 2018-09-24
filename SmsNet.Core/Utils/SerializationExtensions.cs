using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SmsNet.Core.Utils
{
	public static class SerializationExtensions
	{
		public static string ToJson(this object obj, bool idented = true)
		{
			return JsonConvert.SerializeObject(obj, idented ? Formatting.Indented : Formatting.None);
		}
		public static byte[] ToBinary(this object obj)
		{
			byte[] buffer = new byte[0];
			BinaryFormatter formatter = new BinaryFormatter();
			using (MemoryStream stream = new MemoryStream())
			{
				formatter.Serialize(stream, obj);
				buffer = stream.ToArray();
			}
			return buffer;
		}
	}
}
