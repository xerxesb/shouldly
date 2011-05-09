using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace Shouldly.Mocking
{
    internal class RhinoMocksReflectionFacade
    {
        private static readonly Assembly RhinoAssembly;
        private static readonly MethodInfo GenerateMockInfo;
        private static readonly MethodInfo AssertWasCalledInfo;

        static RhinoMocksReflectionFacade()
        {
            RhinoAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Rhino.Mocks")).FirstOrDefault();
        }

        public static T GenerateMock<T>()
        {
            return (T)RhinoAssembly.GetType("Rhino.Mocks.MockRepository").CallMethod(new[] { typeof(T) }, "GenerateMock", new[] { new object[0] });
        }


        public static void AssertWasCalled<T>(T mock, Action<T> action)
        {
            RhinoAssembly.GetType("Rhino.Mocks.RhinoMocksExtensions").CallMethod(new[] { typeof(T) }, "AssertWasCalled", new[] { typeof(T), typeof(Action<T>) }, new object[] { mock, action });
        }
    }

    //public class RhinoMocksAdapter
    //{
    //    private static readonly Assembly RhinoAssembly;
    //    private static readonly MethodInfo GenerateMockInfo;
    //    private static readonly MethodInfo AssertWasCalledInfo;

    //    static RhinoMocksAdapter()
    //    {
    //        RhinoAssembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Rhino.Mocks")).FirstOrDefault();

    //        GenerateMockInfo = (from method in AllStaticMethodsInClass("Rhino.Mocks.MockRepository")
    //                            where method.Name == "GenerateMock"
    //                            where method.IsGenericMethod
    //                            where method.GetParameters().Count() == 1
    //                            select method).First();

    //        AssertWasCalledInfo = (from method in AllStaticMethodsInClass("Rhino.Mocks.RhinoMocksExtensions")
    //                               where method.Name == "AssertWasCalled"
    //                               let parameters = method.GetParameters()
    //                               where parameters.Length == 2 &&
    //                               parameters[1].ParameterType.IsGenericType && typeof(Action<>).GetGenericTypeDefinition().IsAssignableFrom(parameters[1].ParameterType.GetGenericTypeDefinition())
    //                               select method).First();

    //    }

    //    public static T GenerateMock<T>()
    //    {
    //        return (T)CreateInstanceOfGenericType(typeof(T));
    //    }

    //    private static object CreateInstanceOfGenericType(Type type)
    //    {
    //        var method = GenerateMockInfo.MakeGenericMethod(type);
    //        return method.Invoke(null, new[] { new object[0] });
    //    }

    //    private static IEnumerable<MethodInfo> AllStaticMethodsInClass(string className)
    //    {
    //        return RhinoAssembly.GetType(className).GetMethods(BindingFlags.Static | BindingFlags.Public);
    //    }
    //}
}
