using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public interface IInterpolator
	{
		void OnInterpolate(object from, object to, float alpha);
	}
}
