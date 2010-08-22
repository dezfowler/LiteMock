using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace LiteMock
{
	public partial class Mock<T>
	{
		/// <summary>
		/// Represents a call to a mock object
		/// </summary>
		public class Call
		{
			protected Mock<T> mock;
			protected LambdaExpression call;
			protected Predicate<MethodCallMessageWrapper> criteria;

			internal Call(Mock<T> mock, LambdaExpression call)
			{
				this.mock = mock;
				this.call = call;
				string memberName = GetMemberName();
				// set initial criteria for the call
				this.criteria = mc => mc.MethodName == memberName;
			}
			
			protected virtual string GetMemberName()
			{
				if (call.Body.NodeType == ExpressionType.MemberAccess)
				{
					MemberExpression memberExp = (MemberExpression)call.Body;
					
					if (memberExp.Member.MemberType == MemberTypes.Property)
					{
						return "set_" + memberExp.Member.Name;
					}
				}

				if (call.Body.NodeType == ExpressionType.Call)
				{
					MethodCallExpression methodExp = (MethodCallExpression)call.Body;
					return methodExp.Method.Name;
				}

				throw new MockException("Unrecognised expression");
			}

			internal void AddBehaviour(CallBehaviour behaviour)
			{
				behaviour.Profile = criteria;
				mock.proxy.CallBehaviours.Add(behaviour);
			}

			protected object[] GetMethodCallArguments()
			{
				if (call.Body.NodeType != ExpressionType.Call) throw new MockException("Check arguments requires a method call expression.");

				MethodCallExpression methodExp = (MethodCallExpression)call.Body;

				return methodExp.Arguments.Select(exp =>
				{
					ConstantExpression constant = exp as ConstantExpression;
					if (constant == null) throw new MockException("Only constant method arguments supported");
					return constant.Value;
				}).ToArray();
			}

			/// <summary>
			/// Ensures arguments for the call were as expected.
			/// </summary>
			/// <returns></returns>
			public Mock<T> CheckArguments()
			{
				AddBehaviour(new CheckArgumentsBehaviour(GetMethodCallArguments()));
				return this.mock;
			}

			/// <summary>
			/// Ignores the call.
			/// </summary>
			/// <returns></returns>
			public Mock<T> Ignore()
			{
				AddBehaviour(new IgnoreBehaviour());
				return this.mock;
			}

			/// <summary>
			/// Will cause an exception of type <seealso cref="ForcedException"/> to be thrown.
			/// </summary>
			/// <returns></returns>
			public Mock<T> Throw()
			{
				AddBehaviour(new ExceptionBehaviour(new ForcedException()));
				return this.mock;
			}

			/// <summary>
			/// Only match a call with exact arguments.
			/// </summary>
			/// <returns></returns>
			public Call WithExactArguments()
			{
				var existing = this.criteria;
				var methodArgs = GetMethodCallArguments();
				this.criteria = mc => existing(mc) && mc.InArgs.SequenceEqual(methodArgs);
				return this;
			}
		}
	}
}
