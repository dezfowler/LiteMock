using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace LiteMock.Test
{
	[TestFixture]
	public class MockTest
	{
		[Test]
		public void SimpleInterfaceMockIsNotNull()
		{
			var blah = new Mock<IBlah>().Object;
			Assert.IsNotNull(blah);
		}

		[Test]
		public void InheritingInterfaceMockIsNotNull()
		{
			var erm = new Mock<IErm>().Object;
			Assert.IsNotNull(erm);
		}

		[Test]
		[ExpectedException(typeof(MockException))]
		public void MemberAccessWithoutSetup()
		{
			var blah = new Mock<IBlah>().Object;
			int i = blah.Thing;
		}

		[Test]
		public void MemberAccessWithIgnoreCalls()
		{
			var blah = new Mock<IBlah>().IgnoreCalls().Object;
			Assert.AreEqual(default(int), blah.Thing);
			Assert.IsNull(blah.Parent);
		}

		[Test]
		[ExpectedException(typeof(MockException))]
		public void IgnoreSpecificCalls()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.GetThing()).Ignore();
			var blah = mock.Object;
			Assert.AreEqual(default(int), blah.GetThing());
			int i = blah.Thing;
		}

		[Test]
		public void ReadOnlyPropertyReturnsCorrectValue()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.ReadOnly).Return("thing");
			var blah = mock.Object;
			Assert.AreEqual("thing", blah.ReadOnly);
		}

		[Test]
		[ExpectedException(typeof(ForcedException))]
		public void PropertyGetThrows()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.Thing).Throw();
			var blah = mock.Object;
			int i = blah.Thing;
		}

		[Test]
		public void MethodCallReturnsValue()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.GetThing()).Return(10);
			var blah = mock.Object;
			Assert.AreEqual(10, blah.GetThing());
		}

		[Test]
		[ExpectedException(typeof(ForcedException))]
		public void MethodCallThrows()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.GetThing()).Throw();
			var blah = mock.Object;
			int i = blah.GetThing();
		}

		[Test]
		public void MethodCallValid()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.DoThing(5)).CheckArguments();
			var blah = mock.Object;
			blah.DoThing(5);
		}

		[Test]
		[ExpectedException(typeof(MockException))]
		public void MethodCallInvalid()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.DoThing(5)).CheckArguments();
			var blah = mock.Object;
			blah.DoThing(4);
		}

		[Test]
		[ExpectedException(typeof(ForcedException))]
		public void MethodCallWithArgsThrows()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.DoThing(5)).WithExactArguments().Throw();
			var blah = mock.Object;
			blah.DoThing(5);
		}

		[Test]
		public void MethodCallWithArgsDoentThrow()
		{
			var mock = new Mock<IBlah>();
			mock.When(o => o.DoThing(5)).WithExactArguments().Throw().IgnoreCalls();
			var blah = mock.Object;
			blah.DoThing(4);
		}

        //[Test]
        //[ExpectedException(typeof(ForcedException))]
        //public void PropertySetterThrows()
        //{
        //    // Arrange
        //    var mock = new Mock<IBlah>();
        //    mock.When(o => o.Thing = 5).Throw();

        //    // Act
        //    var blah = mock.Object;
        //    blah.Thing = 4;

        //    // Assert
        //}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ExampleRealWorldTest_EnsureExceptionOnNullCollection()
		{
			var blah = new Mock<IBlah>().Object;			
			var thing = new ThingThatNeedsIBlahAndSomethingElse(blah, null);			
		}
	}
}
