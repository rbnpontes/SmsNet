using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	internal sealed class AnimObject
	{
		public object Source { get;internal set; }
		public int Duration { get; internal set; }
		public int Loop { get; internal set; }
		public int Delay { get; internal set; }
		public object InitialValue { get; internal set; }
		public IEasing EasingMethod { get; internal set; }
		public Delegate Listener { get; internal set; }
		public Delegate FromGetter { get; internal set; }
		public Delegate FromSetter { get; internal set; }
		public Delegate To { get; internal set; }
		public Stopwatch Watch { get; internal set; }
	}
}
