using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmsNet.Core.Animation
{
	public interface IAnimate<TSource>
	{
		/// <summary>
		/// Get or Set Fps Time for this Application
		/// </summary>
		int Fps { get; set; }
		/// <summary>
		/// Get or Set Animate Duration
		/// </summary>
		int Duration { get; set; }
		/// <summary>
		/// Get or Set how many time Animate will be executed
		/// -1 = Infinite Loop
		/// 0 > ... = Loop Iteration
		/// </summary>
		int Loop { get; set; }
		/// <summary>
		/// Get or Set init Delay of Animate
		/// </summary>
		int Delay { get; set; }
		IAnimate<TSource> Begin();
		IAnimate<TSource> End();
		IAnimate<TSource> From<TProperty>(Expression<Func<TSource, TProperty>> expression);
		IAnimate<TSource> To<TProperty>(Expression<Func<TSource, TProperty>> expression);
		IAnimate<TSource> SetDuration(int time);
		IAnimate<TSource> SetLoop(int count);
		IAnimate<TSource> SetFps(int fps);
		IAnimate<TSource> SetDelay(int delay);
	}
}
