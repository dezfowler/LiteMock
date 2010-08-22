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
	/// No action is performed, calls requiring a return value 
	/// receive the default value for the required type.
	/// </summary>
	class IgnoreBehaviour : CallBehaviour
	{
		public override IMessage Process(MethodCallMessageWrapper mc)
		{
			MethodInfo mi = (MethodInfo)mc.MethodBase;

			if (mi.ReturnType == typeof(void))
			{
				return ReturnBehaviour.Void.Process(mc);
			}
			else
			{
				object returnValue = null;
				if (mi.ReturnType.IsValueType)
				{
					returnValue = Activator.CreateInstance(mi.ReturnType);
				}
				return new ReturnBehaviour(returnValue).Process(mc);
			}
		}
	}
}
