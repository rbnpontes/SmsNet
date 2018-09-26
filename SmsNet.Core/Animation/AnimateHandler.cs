using SmsNet.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	internal sealed class AnimateHandler<TSource> : IAnimate<TSource>
	{
		public int Duration
		{
			get
			{
				if (mCurrentAnim == null)
					throw new Exception("Can't get Duration value, do you call Begin() ?");
				return mCurrentAnim.Duration;
			}
			set
			{
				if (mCurrentAnim == null)
					return;
				mCurrentAnim.Duration = value;
			}
		}
		public int Loop {
			get
			{
				if (mCurrentAnim == null)
					throw new Exception("Can't get Loop value, do you call Begin() ?");
				return mCurrentAnim.Loop;
			}
			set
			{
				if (mCurrentAnim == null)
					return;
				SetLoop(value);
			}
		}
		public int Delay
		{
			get
			{
				if (mCurrentAnim == null)
					throw new Exception("Can't get Delay value, do you call Begin() ?");
				return mCurrentAnim.Delay;
			}
			set
			{
				if (mCurrentAnim == null)
					return;
				SetDelay(value);
			}
		}

		private AnimObject mCurrentAnim;
		private TSource mSource;
		private IList<AnimObject> mAnimations = new List<AnimObject>();

		public AnimateHandler(TSource src){
			mSource = src;
		}
		public IReadOnlyList<AnimObject> AnimationsHeap { get { return (IReadOnlyList<AnimObject>)mAnimations; } }
		public IAnimate<TSource> Begin()
		{
			mCurrentAnim = new AnimObject();
			mCurrentAnim.Source = mSource;
			mCurrentAnim.EasingMethod = new LinearEasing();
			return this;
		}
		public IAnimate<TSource> End()
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot End Animate, do you call Begin() before End() ?");
			mAnimations.Add(mCurrentAnim);
			mCurrentAnim = null;
			return this;
		}
		public IAnimate<TSource> From<TProperty>(System.Linq.Expressions.Expression<Func<TSource, TProperty>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("@expression cannot be null");
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set From, do you call Begin() before From() ?");

			Type propType = typeof(TProperty);
			if (!(propType.IsAssignableFrom(typeof(int))
				|| propType.IsAssignableFrom(typeof(float))
				|| propType.IsAssignableFrom(typeof(double))
				|| propType.IsAssignableFrom(typeof(short))
				|| propType.IsAssignableFrom(typeof(long))))
			{
				if(!propType.IsAssignableFrom(typeof(IInterpolator)))
					throw new InvalidOperationException("Invalid type Property");
			}

			try
			{
				mCurrentAnim.FromGetter = expression.GetGetterMethod();
				mCurrentAnim.FromSetter = expression.GetSetterMethod();
				mCurrentAnim.InitialValue = mCurrentAnim.FromGetter.DynamicInvoke(mCurrentAnim.Source);
			}
			catch (Exception e)
			{
				throw e;
			}
			return this;
		}
		public IAnimate<TSource> To<TProperty>(Func<TSource, TProperty> expression)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set To, do you call Begin() before To() ?");

			mCurrentAnim.To = expression ?? throw new ArgumentNullException("@expression cannot be null");
			return this;
		}
		public IAnimate<TSource> SetDuration(int time)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set Duration, do you call Begin() before SetDuration() ?");

			mCurrentAnim.Duration = time;
			return this;
		}
		public IAnimate<TSource> SetLoop(int count)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set Duration, do you call Begin() before SetLoop() ?");

			mCurrentAnim.Loop = count;
			return this;
		}
		public IAnimate<TSource> SetDelay(int delay)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set Duration, do you call Begin() before SetDelay() ?");

			mCurrentAnim.Loop = delay;
			return this;
		}
		public IAnimate<TSource> SetOnAnimateListener(Action<TSource, AnimVariant, AnimVariant, float> listener)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set Duration, do you call Begin() before SetDelay() ?");

			mCurrentAnim.Listener = listener ?? throw new ArgumentNullException("listener param can't be null");
			return this;
		}
		public IAnimate<TSource> SetEasingType(IEasing easing)
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot set Duration, do you call Begin() before SetDelay() ?");
			mCurrentAnim.EasingMethod = easing ?? throw new ArgumentNullException("easing param can't be null");
			return this;
		}
	}
}
