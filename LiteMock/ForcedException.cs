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
	/// Exception which will be thrown when the default Throw behaviour 
	/// is required.
	/// </summary>
	[global::System.Serializable]
	public class ForcedException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public ForcedException() { }
		public ForcedException(string message) : base(message) { }
		public ForcedException(string message, Exception inner) : base(message, inner) { }
		protected ForcedException(
		 System.Runtime.Serialization.SerializationInfo info,
		 System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
