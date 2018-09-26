using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public class LinearEasing : IEasing
	{
		public EasingType EasingType { get; set; }

		public float Evaluate(float x)
		{
			return x;
		}
	}
	public class SineEasing : IEasing
	{
		private static readonly double HalfPI = Math.PI / 2D;
		public EasingType EasingType { get; set; } = EasingType.InOut;
		public float Evaluate(float x)
		{
			float value = 0;
			switch (EasingType)
			{
				case EasingType.In:
					value = (float)Math.Sin((x - 1) * HalfPI) + 1f;
					break;
				case EasingType.Out:
					value = (float)Math.Sin(x * HalfPI);
					break;
				case EasingType.InOut:
					value = .5f * (1f - (float)Math.Cos(x * Math.PI));
					break;
			}
			return value;
		}
	}
}
