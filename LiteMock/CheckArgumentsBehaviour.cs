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
	/// When a call occurs the arguments are checked against some pre-defined 
	/// set and an exception is thrown if they do not match.
	/// </summary>
	class CheckArgumentsBehaviour : IgnoreBehaviour
	{
		object[] expectedArgs;

		public CheckArgumentsBehaviour(object[] expectedArgs)
		{
			this.expectedArgs = expectedArgs;
		}

		public override IMessage Process(MethodCallMessageWrapper mc)
		{
			if (!mc.InArgs.SequenceEqual(expectedArgs))
			{
				string[] expected = expectedArgs.Select(o => o.ToString()).ToArray();
				string[] actual = mc.InArgs.Select(o => o.ToString()).ToArray();
				string message = String.Format("Expected arguments {0} but found {1}", String.Join(", ", expected), String.Join(", ", actual));
				var exception = new MockException(message);
				return new ExceptionBehaviour(exception).Process(mc);
			}
			return base.Process(mc);
		}
	}
}
