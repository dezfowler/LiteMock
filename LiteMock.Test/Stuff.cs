using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LiteMock.Test
{
	public interface IBlah
	{
		int Thing { get; set; }
		string ReadOnly { get; }
		void DoThing(int blah);
		int GetThing();
		IBlah Parent { get; }
	}

	public class ThingThatNeedsIBlahAndSomethingElse
	{
		public ThingThatNeedsIBlahAndSomethingElse(IBlah blah, ICollection collection)
		{
			if (blah == null) throw new ArgumentNullException("blah");
			if (collection == null) throw new ArgumentNullException("collection");
		}
	}

	public interface IErm : IBlah, IList
	{
		string Foo { get; }
	}
}
