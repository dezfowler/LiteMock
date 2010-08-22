using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Reflection;
using System.Runtime.Serialization;

namespace LiteMock
{
	/// <summary>
	/// Proxy providing mocking functionality.
	/// </summary>
	class MockingProxy : RealProxy
	{
		public CallBehaviour CatchAllBehaviour = new ExceptionBehaviour(new MockException("Unexpected call"));
		public readonly List<CallBehaviour> CallBehaviours = new List<CallBehaviour>();
		
		public MockingProxy(Type type) : base(type) { }

		public override IMessage Invoke(IMessage msg)
		{
			MethodCallMessageWrapper mc = new MethodCallMessageWrapper((IMethodCallMessage)msg);

			CallBehaviour behaviour = CallBehaviours.FirstOrDefault(i => i.Profile(mc));

			if (behaviour != null)
			{
				return behaviour.Process(mc);
			}

			return CatchAllBehaviour.Process(mc);
		}
	}
}
