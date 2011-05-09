using System;
using System.Threading;
using NUnit.Framework;

namespace Tests.Mocking
{
    internal class AssemblyDependentTestFixtureAttribute : TestFixtureAttribute
    {
        public AssemblyDependentTestFixtureAttribute(string assemblyName)
        {
            AppDomain.CurrentDomain.Load(assemblyName);
        }
    }
}