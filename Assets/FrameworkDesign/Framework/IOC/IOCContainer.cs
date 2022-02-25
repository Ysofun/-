using System.Collections.Generic;
using System;

namespace FrameworkDesign
{
    /// <summary>
    /// IOC������ͨ���ֵ䣬�������ͺ���ʵ���Ķ�Ӧ��ϵ
    /// ��Ҫע����ǣ���ʹ�û�����Ϊ����ʱ��ͬһ����Ĳ�ͬ������ụ�า��
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
