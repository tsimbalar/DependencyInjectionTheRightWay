using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace BoringBank.WebPortal
{
    public class TimingBehavior : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            var stopwatch = new Stopwatch();
            // Before invoking the method on the original target.
            Debug.WriteLine("> {0}.{1}", input.MethodBase.DeclaringType, input.MethodBase.Name);

            stopwatch.Start();
            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            stopwatch.Stop();
            // After invoking the method on the original target.
            if (result.Exception != null)
            {
                Debug.WriteLine(
                    "< {0}.{1} failed - after {3} ms",
                    input.MethodBase.DeclaringType, input.MethodBase.Name, result.Exception.GetType(), stopwatch.ElapsedMilliseconds);
            }
            else
            {
                Debug.WriteLine("< {0}.{1} - after {2} ms",
                    input.MethodBase.DeclaringType, input.MethodBase.Name,
                    stopwatch.ElapsedMilliseconds);
            }

            return result;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute { get { return true; } }
    }
}