using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Linq.Expressions;

namespace LiteMock
{
	/// <summary>
	/// Class for setting up mock.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public partial class Mock<T>
	{
		static readonly Type type = typeof(T);
		MockingProxy proxy;

		public Mock()
		{
			proxy = new MockingProxy(type);
		}

		/// <summary>
		/// Creates a transparent proxy for type <typeparamref name="T"/> and 
		/// returns it.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Object
		{
			get
			{
				return (T)proxy.GetTransparentProxy();
			}
		}

		/// <summary>
		/// Set up a behaviour for the specified call.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="call"></param>
		/// <returns></returns>
		public Call<TResult> When<TResult>(Expression<Func<T, TResult>> call)
		{
			return new Call<TResult>(this, call);
		}

		/// <summary>
		/// Set up a behaviour for the specified call.
		/// </summary>
		/// <param name="call"></param>
		/// <returns></returns>
		public Call When(Expression<Action<T>> call)
		{
			return new Call(this, call);
		}

		/// <summary>
		/// Sets the default behaviour to ignoring calls. Default value returned for 
		/// value types and null for reference types.
		/// </summary>
		/// <returns></returns>
		public Mock<T> IgnoreCalls()
		{
			this.proxy.CatchAllBehaviour = new IgnoreBehaviour();
			return this;
		}
	}
}
