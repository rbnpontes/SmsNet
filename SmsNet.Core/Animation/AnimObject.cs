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
		public object Source { get; set; }
		public int Duration { get; set; }
		public int Loop { get; set; }
		public int Delay { get; set; }
		public Delegate FromGetter { get; set; }
		public Delegate FromSetter { get; set; }
		public Delegate To { get; set; }
		public Stopwatch Watch { get; set; }
	}
}
