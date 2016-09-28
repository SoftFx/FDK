namespace LrpServer.Net.Threading
{
    using System;
    using System.Threading;

    static class TaskEx
    {
        public static void Start(Action func)
        {
            ThreadPool.QueueUserWorkItem(Execute0, func);
        }

        public static void Start<T0>(Action<T0> func, T0 arg0)
        {
            var arg = Tuple.Create(func, arg0);
            ThreadPool.QueueUserWorkItem(Execute1<T0>, arg);
        }

        public static void Start<T0, T1>(Action<T0, T1> func, T0 arg0, T1 arg1)
        {
            var arg = Tuple.Create(func, arg0, arg1);
            ThreadPool.QueueUserWorkItem(Execute2<T0, T1>, arg);
        }

        public static void Start<T0, T1, T2>(Action<T0, T1, T2> func, T0 arg0, T1 arg1, T2 arg2)
        {
            var arg = Tuple.Create(func, arg0, arg1, arg2);
            ThreadPool.QueueUserWorkItem(Execute3<T0, T1, T2>, arg);
        }

        public static void Start<T0, T1, T2, T3>(Action<T0, T1, T2, T3> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            var arg = Tuple.Create(func, arg0, arg1, arg2, arg3);
            ThreadPool.QueueUserWorkItem(Execute4<T0, T1, T2, T3>, arg);
        }

        public static void Start<T0, T1, T2, T3, T4>(Action<T0, T1, T2, T3, T4> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            var arg = Tuple.Create(func, arg0, arg1, arg2, arg3, arg4);
            ThreadPool.QueueUserWorkItem(Execute5<T0, T1, T2, T3, T4>, arg);
        }

        public static void Start<T0, T1, T2, T3, T4, T5>(Action<T0, T1, T2, T3, T4, T5> func, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            var arg = Tuple.Create(func, arg0, arg1, arg2, arg3, arg4, arg5);
            ThreadPool.QueueUserWorkItem(Execute6<T0, T1, T2, T3, T4, T5>, arg);
        }

        #region Members

        static void Execute0(object obj)
        {
            var action = (Action)obj;
            action();
        }

        static void Execute1<T0>(object obj)
        {
            var arg = (Tuple<Action<T0>, T0>)obj;
            arg.Item1(arg.Item2);
        }

        static void Execute2<T0, T1>(object obj)
        {
            var arg = (Tuple<Action<T0, T1>, T0, T1>)obj;
            arg.Item1(arg.Item2, arg.Item3);
        }

        static void Execute3<T0, T1, T2>(object obj)
        {
            var arg = (Tuple<Action<T0, T1, T2>, T0, T1, T2>)obj;
            arg.Item1(arg.Item2, arg.Item3, arg.Item4);
        }

        static void Execute4<T0, T1, T2, T3>(object obj)
        {
            var arg = (Tuple<Action<T0, T1, T2, T3>, T0, T1, T2, T3>)obj;
            arg.Item1(arg.Item2, arg.Item3, arg.Item4, arg.Item5);
        }

        static void Execute5<T0, T1, T2, T3, T4>(object obj)
        {
            var arg = (Tuple<Action<T0, T1, T2, T3, T4>, T0, T1, T2, T3, T4>)obj;
            arg.Item1(arg.Item2, arg.Item3, arg.Item4, arg.Item5, arg.Item6);
        }

        static void Execute6<T0, T1, T2, T3, T4, T5>(object obj)
        {
            var arg = (Tuple<Action<T0, T1, T2, T3, T4, T5>, T0, T1, T2, T3, T4, T5>)obj;
            arg.Item1(arg.Item2, arg.Item3, arg.Item4, arg.Item5, arg.Item6, arg.Item7);
        }

        #endregion
    }
}
