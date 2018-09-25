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
		public int Fps
		{
			get {
				if (mCurrentAnim == null)
					return 0;
				return mCurrentAnim.Fps;
			}
			set{
				if (mCurrentAnim == null)
					return;
				mCurrentAnim.Fps = value;
			}
		}
		public int Duration
		{
			get
			{
				if (mCurrentAnim == null)
					return 0;
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
					return 0;
				return mCurrentAnim.Loop;
			}
			set
			{
				if (mCurrentAnim == null)
					return;
				mCurrentAnim.Loop = value;
			}
		}
		public int Delay
		{
			get
			{
				if (mCurrentAnim == null)
					return 0;
				return mCurrentAnim.Delay;
			}
			set
			{
				if (mCurrentAnim == null)
					return;
				mCurrentAnim.Delay = value;
			}
		}
		public class Anim
		{
			public int Fps { get; set; }
			public int Duration { get; set; }
			public int Loop { get; set; }
			public int Delay { get; set; }
			public Delegate FromGetter { get; set; }
			public Delegate FromSetter { get; set; }
			public Delegate ToGetter { get; set; }
			public Delegate ToSetter { get; set; }
		}

		private Anim mCurrentAnim;
		private IList<Anim> mAnimations = new List<Anim>();

		public IAnimate<TSource> Begin()
		{
			mCurrentAnim = new Anim();
			return this;
		}

		public IAnimate<TSource> End()
		{
			if (mCurrentAnim == null)
				throw new NullReferenceException("Cannot End Animate, do you call Begin() before End() ?");
			mAnimations.Add(mCurrentAnim);
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
		public IAnimate<TSource> SetDuration(int time)
		{
			throw new NotImplementedException();
		}
		public IAnimate<TSource> SetFps(int fps)
		{
			throw new NotImplementedException();
		}
		public IAnimate<TSource> SetLoop(int count)
		{
			throw new NotImplementedException();
		}
		public IAnimate<TSource> SetDelay(int delay)
		{
			throw new NotImplementedException();
		}
		public IAnimate<TSource> To<TProperty>(System.Linq.Expressions.Expression<Func<TSource, TProperty>> expression)
		{
			throw new NotImplementedException();
		}
	}
}
