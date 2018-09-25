using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public sealed class Animator : IFactory
	{
		public void OnCreateFactory()
		{
		}
		public IAnimate<T> Bind<T>(T obj)
		{
			return null;
		}
		public Animator Play<T>(IAnimate<T> anim)
		{
			return this;
		}
	}
}
