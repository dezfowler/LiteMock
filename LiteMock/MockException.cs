using System;
using System.Runtime.Serialization;

namespace LiteMock
{
	/// <summary>
	/// Represents an error occuring within a mock object.
	/// </summary>
	[global::System.Serializable]
	public class MockException : Exception
	{
		public MockException() { }
		public MockException(string message) : base(message) { }
		public MockException(string message, Exception inner) : base(message, inner) { }
		protected MockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
