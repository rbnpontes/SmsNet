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
			List<AnimObject> animations = (List<AnimObject>)state;
			while (animations.Count > 0)
			{
				for(int i=0;i<animations.Count;i++)
				{
					AnimObject item = animations[i];
					if(item.Watch == null)
					{
						item.Watch = new Stopwatch();
						item.Watch.Start();
					}
					float alpha = (float)item.Watch.ElapsedMilliseconds / (float)item.Duration;
					if (alpha > 1.0f)
						alpha = 1.0f;
					TryMakeInterpolation(item, alpha);
					if(alpha >= 1.0f && item.Loop != -1)
					{
						if(item.Loop == 0)
						{
							animations.RemoveAt(i);
							continue;
						}
						item.Loop--;
						item.Watch.Restart();
						item.Watch.Start();
					}
				}
			}
		}
		private void TryMakeInterpolation(AnimObject anim, float alpha)
		{
			object rawFromValue = anim.FromGetter.DynamicInvoke(anim.Source);
			object rawToValue = anim.FromGetter.DynamicInvoke(anim.Source);

			if(rawFromValue is Int32 || rawFromValue is Int16 || rawFromValue is Int64)
			{
				long value = IntegerInterpolation((long)rawFromValue, (long)rawToValue, alpha);
			}
		}
		private long IntegerInterpolation(long from, long to, float alpha)
		{
			long diff = to - from;
			return (long)Math.Floor(diff * alpha);
		}
		private double FloatInterpolation(double from, double to, float alpha)
		{
			double diff = to - from;
			return diff * alpha;
		}
	}
}
