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
	/// When a call requiring a return value occurs a specified value will 
	/// be returned.
	/// </summary>
	class ReturnBehaviour : CallBehaviour
	{
		object returnValue;
		public ReturnBehaviour(object returnValue)
		{
			this.returnValue = returnValue;
		}

		public override IMessage Process(MethodCallMessageWrapper mc)
		{
			return new ReturnMessage(returnValue, null, 0, mc.LogicalCallContext, mc);
		}

		public static readonly ReturnBehaviour Void = new ReturnBehaviour(typeof(void));
	}
}
