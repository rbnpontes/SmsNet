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
		/// <summary>
		/// Add to heap a new Animation
		/// </summary>
		IAnimate<TSource> Begin();
		/// <summary>
		/// Finish Animation, this method will collect all instruction for Animate when pass to Animator.Play()
		/// </summary>
		/// <exception>NullReferenceException</exception>
		IAnimate<TSource> End();
		/// <summary>
		/// Set Property for made Interpolation
		/// </summary>
		/// <typeparam name="TProperty">Property Type</typeparam>
		/// <param name="expression">Property Lambda Expression, example "x => x.Id" </param>
		IAnimate<TSource> From<TProperty>(Expression<Func<TSource, TProperty>> expression);
		/// <summary>
		/// Set Getter method for Interpolate between From value and To value
		/// </summary>
		/// <typeparam name="TProperty">Property Type</typeparam>
		/// <param name="expression">Property Lambda Expression, example "x => x.Id"</param>
		IAnimate<TSource> To<TProperty>(Func<TSource, TProperty> expression);
		/// <summary>
		/// Set Animation Duration
		/// </summary>
		/// <param name="time">Property Lambda Expression, example "x => x.Id"</param>
		IAnimate<TSource> SetDuration(int time);
		/// <summary>
		/// Set Loop count Iterations, this method will define how many times Animate will be executed
		/// </summary>
		/// <param name="count">
		/// Loop Iterations
		/// -1 = Infinite
		/// 0 > ... = Iteration Count
		/// </param>
		IAnimate<TSource> SetLoop(int count);
		/// <summary>
		/// Delay at init animation,
		/// </summary>
		/// <param name="delay">delay in milleseconds</param>
		IAnimate<TSource> SetDelay(int delay);
	}
}
