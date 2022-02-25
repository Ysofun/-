using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class CanDoEverything
    {
        public void CanDoSomething1()
        {
            Debug.Log("DoSomething1");
        }

        public void CanDoSomething2()
        {
            Debug.Log("DoSomething2");
        }

        public void CanDoSomething3()
        {
            Debug.Log("DoSomething3");
        }
    }

    public interface IHasEverything
    {
        CanDoEverything CanDoEverything { get; }
    }

    public interface ICanDoSomething1 : IHasEverything
    {

    }

    public static class ICanDoSomething1Extensions
    {
        public static void DoSomething1(this ICanDoSomething1 current)
        {
            current.CanDoEverything.CanDoSomething1();
        }
    }

    public interface ICanDoSomething2 : IHasEverything
    {

    }

    public static class ICanDoSomething2Extensions
    {
        public static void DoSomething2(this ICanDoSomething2 current)
        {
            current.CanDoEverything.CanDoSomething2();
        }
    }

    public interface ICanDoSomething3 : IHasEverything
    {

    }

    public static class ICanDoSomething3Extensions
    {
        public static void DoSomething3(this ICanDoSomething3 current)
        {
            current.CanDoEverything.CanDoSomething3();
        }
    }
    public class InterfaceRuleExample : MonoBehaviour
    {
        public class OnlyCanDo1 : ICanDoSomething1
        {
            public CanDoEverything CanDoEverything { get; } = new CanDoEverything();
        }

        public class OnlyCanDo23 : ICanDoSomething2, ICanDoSomething3
        {
            public CanDoEverything CanDoEverything { get; } = new CanDoEverything();
        }

        void Start()
        {
            var onlyCanDo1 = new OnlyCanDo1();
            onlyCanDo1.DoSomething1();

            var onlyCanDo23 = new OnlyCanDo23();
            onlyCanDo23.DoSomething2();
            onlyCanDo23.DoSomething3();
        }
    }

}
