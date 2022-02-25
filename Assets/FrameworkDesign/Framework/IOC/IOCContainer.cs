using System.Collections.Generic;
using System;

namespace FrameworkDesign
{
    /// <summary>
    /// IOC容器，通过字典，储存类型和类实例的对应关系
    /// 需要注意的是，当使用基类作为类型时，同一基类的不同派生类会互相覆盖
    /// </summary>
    public class IOCContainer
    {
        public Dictionary<Type, object> Instances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);
            
            if (Instances.ContainsKey(key))
            {
                Instances[key] = instance;
            }
            else
            {
                Instances.Add(key, instance);
            }
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);

            object retObj;

            if (Instances.TryGetValue(key, out retObj))
            {
                return (retObj as T);
            }

            return null;
        }
    }

}
