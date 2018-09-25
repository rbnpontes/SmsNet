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
			try
			{
				mCurrentAnim.FromGetter = expression.GetGetterMethod();
				mCurrentAnim.FromSetter = expression.GetSetterMethod();
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
	}
}
