using SmsNet.Core.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Container
{
	public class HashMap<TObject> : IEnumerable<TObject>
	{
		public class KeyValuePair
		{
			public uint Hash;
			public TObject Value;
			internal KeyValuePair() { }
		}
		protected IList<KeyValuePair> Heap = new List<KeyValuePair>();

		public virtual TObject this[string input]{ 
			get{
				KeyValuePair pair = FindPairInput(input);
				if (pair == null)
					return default(TObject);
				return pair.Value;
			}
			set
			{
				KeyValuePair pair = FindOrCreatePair(input);
				pair.Value = value;
			}
		}
		public virtual TObject this[int index]
		{
			get
			{
				if (index >= Count)
					throw new IndexOutOfRangeException("Index is out size");
				return Heap[index].Value;
			}
			set
			{
				if (index >= Count)
					throw new IndexOutOfRangeException("Index is out size");
				Heap[index].Value = value;
			}
		}
		public virtual TObject this[uint hash]
		{
			get
			{
				KeyValuePair pair = FindPairByHash(hash);
				if (pair ==null)
					return default(TObject);
				return pair.Value;
			}
			set
			{
				KeyValuePair pair = FindOrCreatePair(hash);
				pair.Value = value;
			}
		}
		public int Count { get { return Heap.Count; }}

		public HashMap<TObject> Add(string input, TObject value)
		{
			KeyValuePair pair = FindOrCreatePair(input);
			pair.Value = value;

			return this;
		}
		public HashMap<TObject> Add(uint input, TObject value)
		{
			KeyValuePair pair = FindOrCreatePair(input);
			pair.Value = value;

			return this;
		}
		public HashMap<TObject> Remove(string input)
		{
			uint hash = Hashing.SDBM(input);
			for(int i = 0; i < Heap.Count; i++)
			{
				if(Heap[i].Hash == hash)
				{
					Heap.RemoveAt(i);
					break;
				}
			}
			return this;
		}
		public HashMap<TObject> Remove(uint hash)
		{
			for (int i = 0; i < Heap.Count; i++)
			{
				if (Heap[i].Hash == hash)
				{
					Heap.RemoveAt(i);
					break;
				}
			}
			return this;
		}
		public HashMap<TObject> RemoveAt(int index)
		{
			if (index < 0 || index >= Heap.Count)
				throw new IndexOutOfRangeException("Index out of bounds");
			Heap.RemoveAt(index);

			return this;
		}

		public TObject Find(string input)
		{
			return this[input];
		}
		public TObject Find(uint hash)
		{
			return this[hash];
		} 

		public HashMap<TObject> ForEach(Action<KeyValuePair> action)
		{
			if (action == null)
				return this;
			foreach(KeyValuePair pair in Heap)
				action.Invoke(pair);
			return this;
		}
		public HashMap<TObject> ForEachAsync(Action<KeyValuePair> action)
		{
			if (action == null)
				return this;
			Parallel.ForEach(Heap, x =>
			 {
				 action.Invoke(x);
			 });
			return this;
		}
		public bool HasKey(string input)
		{
			return HasKey(Hashing.SDBM(input));
		}
		public bool HasKey(uint hash)
		{
			return FindPairByHash(hash) != null;
		}
		public IEnumerator<TObject> GetEnumerator()
		{
			foreach(KeyValuePair item in Heap)
				yield return item.Value;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		private KeyValuePair FindPairInput(string input)
		{
			uint hash = Hashing.SDBM(input);
			foreach(KeyValuePair item in Heap)
			{
				if (item.Hash == hash)
					return item;
			}
			return null;
		}
		private KeyValuePair FindOrCreatePair(string input)
		{
			uint hash = Hashing.SDBM(input);
			KeyValuePair pair = null;
			foreach (KeyValuePair item in Heap)
			{
				if (item.Hash == hash)
				{
					pair = item;
					break;
				}
			}
			if(pair == null)
			{
				pair = new KeyValuePair();
				pair.Hash = hash;
				Heap.Add(pair);
			}
			return pair;
		}
		private KeyValuePair FindOrCreatePair(uint hash)
		{
			KeyValuePair pair = null;
			foreach(KeyValuePair item in Heap)
			{
				if(item.Hash == hash)
				{
					pair = item;
					break;
				}
			}
			if(pair == null)
			{
				pair = new KeyValuePair();
				pair.Hash = hash;

				Heap.Add(pair);
			}
			return pair;
		}
		private KeyValuePair FindPairByHash(uint hash)
		{
			foreach(KeyValuePair pair in Heap)
			{
				if (pair.Hash == hash)
					return pair;
			}
			return null;
		}
	}
}
