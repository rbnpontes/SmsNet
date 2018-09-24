using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Common
{
	public static class Hashing
	{
		/// <summary>
		/// Make SDBM Hash by a given input
		/// </summary>
		/// <param name="str">Input text for transform into SDBM Hash</param>
		/// <returns>return unsigned value hash</returns>
		public static uint SDBM(string input)
		{
			uint result = 0;
			for (int i = 0; i < input.Length; i++)
				result = input[i] + (result << 6) + (result << 16) - result;
			return result;
		}
		/// <summary>
		/// Make Md5 Hash by a given input
		/// </summary>
		/// <param name="input"></param>
		/// <returns>return a MD5 string hash</returns>
		public static string MD5(string input)
		{
			var md5 = System.Security.Cryptography.MD5.Create();

			byte[] inputBytes = Encoding.Default.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			StringBuilder sb = new StringBuilder();
			foreach (byte value in hash)
				sb.Append(value.ToString("X2"));
			return sb.ToString();
		}
	}
}
