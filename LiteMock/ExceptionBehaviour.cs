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
	/// When a call occurs a specified exception will be thrown.
	/// </summary>
	class ExceptionBehaviour : CallBehaviour
	{
		Exception exception;
		public ExceptionBehaviour(Exception exception)
		{
			this.exception = exception;
		}

		public override IMessage Process(MethodCallMessageWrapper mc)
		{
			return new ReturnMessage(exception, mc);
		}
	}
}
