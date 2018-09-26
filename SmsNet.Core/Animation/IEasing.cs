using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public enum EasingType
	{
		In,
		Out,
		InOut
	}
	public interface IEasing
	{
		EasingType EasingType { get; set; }
		float Evaluate(float x);
	}
}
