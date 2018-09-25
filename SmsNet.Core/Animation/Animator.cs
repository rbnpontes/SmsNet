using SmsNet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public sealed class Animator : IFactory
	{

		private TaskFactory mTaskFactory;
		public void OnCreateFactory()
		{
			mTaskFactory = new TaskFactory();
		}
		public IAnimate<T> Bind<T>(T obj)
		{
			AnimateHandler<T> handler = new AnimateHandler<T>(obj);
			return handler;
		}
		public Animator Play<T>(IAnimate<T> anim)
		{
			if (!(anim is AnimateHandler<T>))
				throw new ArgumentException("Invalid Animation Object");

			AnimateHandler<T> handler = (AnimateHandler<T>)anim;
			mTaskFactory.StartNew(HandleAnimation,handler.AnimationsHeap);
			return this;
		}
		private void HandleAnimation(object state)
		{
			IReadOnlyList<AnimObject> animations = (IReadOnlyList<AnimObject>)state;
			Stopwatch watch = new Stopwatch();
			watch.Start();
		}
	}
}
