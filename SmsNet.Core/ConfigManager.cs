using SmsNet.Core.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SmsNet.Core
{
	public sealed class ConfigManager : IFactory
	{
		/// <summary>
		///  used for cache configs and fast load
		/// </summary>
		private Dictionary<string,object> cached = new Dictionary<string,object>();

		public T Create<T>(string path)
		{
			if (cached.ContainsKey(path))
				throw new ArgumentException("Current configs has exists");
			T target = Activator.CreateInstance<T>();
			cached[path] = target;
			return target;
		}
		public T Load<T>(string path)
		{
			if (string.IsNullOrEmpty(path))
				throw new ArgumentException("Invalid Argument, the path arg is null or empty");

			T value = TryLoad<T>(path);
			cached[path] = value;
			return value;
		}
		public ConfigManager Save(string path)
		{
			TrySave(path);
			return this;
		}
		public ConfigManager SaveAll()
		{
			foreach(var pair in cached)
			{
				TrySave(pair.Key);
			}
			return this;
		}
		public ConfigManager ClearCache()
		{
			cached.Clear();
			return this;
		}

		private T TryLoad<T>(string path)
		{
			string absolutePath = Path.GetFullPath(path);
			T result = default(T);
			if(cached.ContainsKey(path))
			{
				result = (T)cached[path];
				return result;
			}
			if(!File.Exists(absolutePath))
			{
				result = Activator.CreateInstance<T>();
				cached[path] = result;
				TrySave(path);
				return result;
			}
			string raw = File.ReadAllText(path);
			try
			{
				result = JsonConvert.DeserializeObject<T>(raw);
			}catch(Exception e){
				throw e;
			}
			return result;
		}
		private void TrySave(string path)
		{
			object config = cached[path];
			if (config == null)
				throw new NullReferenceException("Couldn't save config, doesn't exist a config for this path");
			string absolutePath = Path.GetFullPath(path);
			string dirPath = Path.GetDirectoryName(path);
			if (!Directory.Exists(dirPath))
				Directory.CreateDirectory(dirPath);

			if (!File.Exists(absolutePath))
				File.Create(absolutePath).Close();

			File.WriteAllText(absolutePath, JsonConvert.SerializeObject(config));
		}

		public void OnCreateFactory()
		{
		}
	}
}
