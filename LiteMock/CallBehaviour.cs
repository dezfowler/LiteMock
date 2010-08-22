using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Reflection;
using System.Runtime.Serialization;

namespace LiteMock
{
	/// <summary>
	/// Defines an action to be performed when encountering a method call 
	/// matching a particular profile.
	/// </summary>
	abstract class CallBehaviour
	{
		/// <summary>
		/// Method profile to be matched.
		/// </summary>
		public Predicate<MethodCallMessageWrapper> Profile { get; set; }

		/// <summary>
		/// Action to perform.
		/// </summary>
		/// <param name="mc"></param>
		/// <returns></returns>
		public abstract IMessage Process(MethodCallMessageWrapper mc);
	}
}
