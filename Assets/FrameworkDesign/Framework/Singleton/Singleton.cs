using System;
using System.Reflection;

namespace FrameworkDesign
{
    /// <summary>
    /// ����ģʽ���࣬ͨ�����������ʵ��������
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
                    //ͨ�������ȡ���Ǿ�̬��Ĳ������Ĺ��캯��
                    var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    //end

                    //�ҵ����в����б�Ϊ�յĹ��캯��
                    var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
                    //end

                    //�����ھ��׳�����
                    if (ctor == null)
                    {
                        throw new Exception("Non-Public Constructor() not found in " + typeof(T));
                    }

                    //���ù��캯��ʵ��������ת��Ϊ��Ӧ������
                    instance = ctor.Invoke(null) as T;
                }
                return instance;
            }
        }
    }
}

