using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Utils
{
	public static class CollectionExtensions
	{
		public static UTarget[] Map<TSource,UTarget>(this TSource[] sources, Func<TSource,UTarget> converter)
		{ 
			UTarget[] results = new UTarget[sources.Length];

			for(int i = 0; i < sources.Length; i++)
			{
				TSource source = sources[i];
				if (source == null)
					continue;
				results[i] = converter(source);
			}
			return results;
		}
		public static UTarget[] Map<TSource,UTarget>(this IEnumerable<TSource> sources, Func<TSource,UTarget> converter)
		{
			UTarget[] results = new UTarget[sources.Count()];

			for(int i = 0; i < sources.Count(); i++)
			{
				TSource source = sources.ElementAt(i);
				if (source == null)
					continue;

				results[i] = converter(source);
			}
			return results;
		}
		public static ICollection<UTarget> Map<TSource,UTarget>(this ICollection<TSource> sources, Func<TSource,UTarget> converter)
		{
			List<UTarget> results = new List<UTarget>(new UTarget[sources.Count]);
			
			for(int i=0;i<sources.Count;i++)
			{
				TSource source = sources.ElementAt(i);
				if (source == null)
					continue;

				results[i] = converter(source);
			}
			return results;
		}
	}
}
