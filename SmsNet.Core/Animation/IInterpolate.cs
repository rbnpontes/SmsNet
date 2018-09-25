using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public interface IInterpolate<TSource>
	{
		void OnInterpolate(TSource from, TSource to, float alpha);
	}
}
