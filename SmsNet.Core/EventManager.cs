using SmsNet.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core
{
	public delegate void EventHandler(object sender, params object[] parameters);
	internal sealed class EventManager
	{
		public class Holder {
			public uint Key;
			public IList<EventHandler> Actions = new List<EventHandler>();
			public void Invoke(object sender, object[] parameters)
			{
				foreach(EventHandler handler in Actions)
				{
					handler?.Invoke(sender, parameters);
				}
			}
		}
		public IList<Holder> mListeners = new List<Holder>();

		public void AddListener(string listener, EventHandler action)
		{
			AddListener(Hashing.SDBM(listener),action);
		}
		public void AddListener(uint hash, EventHandler action)
		{
			Holder holder = FindOrCreateListener(hash);
			holder.Actions.Add(action);
		}
		public void RemoveListener(string listener, EventHandler action)
		{
			RemoveListener(Hashing.SDBM(listener), action);
		}
		public void RemoveListener(uint hash, EventHandler action)
		{
			Holder holder = FindListener(hash);
			if (holder == null)
				return;
			for (int i = 0; i < holder.Actions.Count; i++)
			{
				if (holder.Actions[i] == action)
				{
					holder.Actions.RemoveAt(i);
					return;
				}
			}

		}
		public void ClearListeners(string listener)
		{
			ClearListeners(Hashing.SDBM(listener));
		}
		public void ClearListeners(uint listener)
		{
			Holder holder = FindListener(listener);
			if(holder != null)
				holder.Actions.Clear();
		}
		public void Invoke(string listener, object sender, object[] parameters)
		{
			Invoke(Hashing.SDBM(listener), sender, parameters);
		}
		public void Invoke(uint hash, object sender, object[] parameters)
		{
			Holder holder = FindListener(hash);
			if (holder != null)
				holder.Invoke(sender, parameters);
		}
		private Holder FindListener(uint hash)
		{
			foreach(Holder holder in mListeners)
			{
				if (holder.Key == hash)
					return holder;
			}
			return null;
		}
		private Holder FindOrCreateListener(uint hash)
		{
			Holder result = null;
			foreach (Holder holder in mListeners)
			{
				if (holder.Key == hash)
				{
					result = holder;
					break;
				}
			}
			if(result == null)
			{
				result = new Holder();
				result.Key = hash;
				mListeners.Add(result);
			}
			return result;
		}
	}
}
