using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace LiteMock
{
	public partial class Mock<T>
	{
		public class Call<TResult> : Call
		{
			public Call(Mock<T> mock, Expression<Func<T, TResult>> call) : base(mock, call) { }

			public Mock<T> Return(TResult result)
			{
				AddBehaviour(new ReturnBehaviour(result));
				return this.mock;
			}

			protected override string GetMemberName()
			{
				if (call.Body.NodeType == ExpressionType.MemberAccess)
				{
					MemberExpression memberExp = (MemberExpression)call.Body;
					if (memberExp.Member.MemberType == MemberTypes.Property)
					{
						return "get_" + memberExp.Member.Name;
					}
				}

				return base.GetMemberName();
			}			
		}
	}
}
