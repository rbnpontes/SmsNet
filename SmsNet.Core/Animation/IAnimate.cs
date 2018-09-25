using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public interface IAnimate<TSource>
	{
		void Begin();
		void End();
	}
}
