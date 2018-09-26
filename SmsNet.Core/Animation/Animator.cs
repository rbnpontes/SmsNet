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
					float alpha = item.Watch.ElapsedMilliseconds / (float)item.Duration;
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
			alpha = anim.EasingMethod.Evaluate(alpha);
			AnimVariant rawFromValue = new AnimVariant(anim.InitialValue);
			AnimVariant rawToValue = new AnimVariant(anim.To.DynamicInvoke(anim.Source));

			object value = null;
			switch (rawFromValue.VariantType)
			{
				case AnimVariantType.Int:
					value = Interpolate(rawFromValue.GetInt(), rawToValue.GetInt(), alpha);
					break;
				case AnimVariantType.Short:
					value = Interpolate(rawFromValue.GetShort(), rawToValue.GetShort(), alpha);
					break;
				case AnimVariantType.Long:
					value = Interpolate(rawFromValue.GetLong(), rawToValue.GetLong(), alpha);
					break;
				case AnimVariantType.Float:
					value = Interpolate(rawFromValue.GetFloat(), rawToValue.GetFloat(), alpha);
					break;
				case AnimVariantType.Double:
					value = Interpolate(rawFromValue.GetDouble(), rawToValue.GetDouble(), alpha);
					break;
				case AnimVariantType.Interpolator:
					{
						IInterpolator interpolator = rawFromValue.GetInterpolator();
						interpolator.OnInterpolate(rawFromValue.Source,rawToValue.Source,alpha);
						value = interpolator;
					}
					break;
				default:
					throw new InvalidOperationException("Cannot Interpolate a Unknow Type value");
			}
			anim.Listener?.DynamicInvoke(anim.Source, rawFromValue, rawToValue, alpha);
			anim.FromSetter.DynamicInvoke(anim.Source, value);
		}
		private T Interpolate<T>(T from, T to, float alpha)
		{
			dynamic first = from;
			dynamic second = to;

			return (T)((second - first) * alpha);
		}
	}
}
