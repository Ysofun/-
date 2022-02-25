using System;
using System.Reflection;

namespace FrameworkDesign
{
    /// <summary>
    /// 单例模式基类，通过反射机制来实例化单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    //通过反射获取到非静态类的不公开的构造函数
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    //end

                    //找到其中参数列表为空的构造函数
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    //end

                    //不存在就抛出错误
                    if (ctor == null)
                    {
                        throw new Exception("Non-Public Constructor() not found in " + typeof(T));
                    }

                    //调用构造函数实例化，并转化为对应的类型
                    instance = ctor.Invoke(null) as T;
                }
                return instance;
            }
        }
    }
}

