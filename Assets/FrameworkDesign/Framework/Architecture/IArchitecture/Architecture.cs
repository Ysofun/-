using System;
using System.Collections.Generic;

namespace FrameworkDesign
{
    /// <summary>
    /// �ܹ��ӿ�
    /// </summary>
    public interface IArchitecture : IUtilityArchitecture, IModelArchitecture, ISystemArchitecture, ICommandArchtecture, IEventArchitecture
    {

    }

    /// <summary>
    /// �ܹ�������ʵ��model�㡢Utility����IOC�����е�ע��ͻ�ȡ
    /// </summary>
    /// <typeparam name="T0">�̳иüܹ�������</typeparam>
    public abstract class Architecture<T0> : IArchitecture where T0 : Architecture<T0>, new()
    {

        #region ����
        /// <summary>
        /// T0�Ǽ̳�������ͣ���������һ����������Ϊ��ʵ�������ڵ���ģʽ
        /// </summary>
        private static T0 architecture = null;
        public static IArchitecture MyInterface
        {
            get
            {
                if (architecture == null)
                {
                    MakeSureContainer();
                }
                return architecture;
            }
        }

        private bool isInited = false;

        /// <summary>
        /// ȷ���������ڣ����û�У�������
        /// </summary>
        private static void MakeSureContainer()
        {
            if (architecture == null)
            {
                architecture = new T0();
                architecture.Init();

                foreach (var model in architecture.models)
                {
                    model.Init();
                }
                architecture.models.Clear();

                foreach (var system in architecture.systems)
                {
                    system.Init();
                }
                architecture.systems.Clear();

                architecture.isInited = true;
            }
        }
        #endregion

        #region System����
        private List<ISystem> systems = new List<ISystem>();

        public void RegisterSystem<T>(T system) where T : ISystem
        {
            system.SetArchitecture(this);
            container.Register<T>(system);

            if (isInited)
            {
                system.Init();
            }
            else
            {
                systems.Add(system);
            }
        }

        public T1 GetSystem<T1>() where T1 : class, ISystem
        {
            return container.Get<T1>();
        }
        #endregion

        #region Model����
        private List<IModel> models = new List<IModel>();

        public void RegisterModel<T1>(T1 model) where T1 : IModel
        {
            model.SetArchitecture(this);
            container.Register(model);
            if (isInited)
            {
                model.Init();
            }
            else
            {
                models.Add(model);
            }
        }

        public T1 GetModel<T1>() where T1 : class, IModel
        {
            return architecture.container.Get<T1>();
        }
        #endregion

        #region Utility����
        /// <summary>
        /// ע��Utility���ʵ��
        /// </summary>
        /// <typeparam name="T1">ע���ʵ�������ͣ�ʵ���Ļ���/�ӿڣ�</typeparam>
        /// <param name="instance">���ʵ��</param>
        public void RegisterUtility<T1>(T1 instance)
        {
            //������IOC������ע��ķ���
            container.Register(instance);
        }

        /// <summary>
        /// ���Utility���ʵ��
        /// </summary>
        /// <typeparam name="T1">ע���ʵ�������ͣ�ʵ���Ļ���/�ӿڣ�</typeparam>
        /// <returns></returns>
        public T1 GetUtility<T1>() where T1 : class
        {
            //���ô�IOC�����л�õķ���
            return container.Get<T1>();
        }
        #endregion

        /// <summary>
        /// һ��ί�У����Ǿ����ҵ�˼�����о����Ҿ��ý̵̳�д����һЩ���⣬������֤
        /// </summary>
        public static Action<T0> OnRegisterPatch = architecture => { };

        #region �����෽��
        /// <summary>
        /// Ϊ�������ṩ�ĳ�ʼ������
        /// </summary>
        protected abstract void Init();
        #endregion

        #region IOC��������
        /// <summary>
        /// IOC����
        /// </summary>
        private IOCContainer container = new IOCContainer();
        #endregion

        #region Command����
        public void SendCommand<T1>() where T1 : ICommand, new()
        {
            var command = new T1();
            command.SetArchitecture(this);
            command.Execute();
        }

        public void SendCommand<T1>(T1 command) where T1 : ICommand
        {
            command.Execute();
        }
        #endregion

        #region �¼�����
        private ITypeEventSystem m_TypeEventSystem = new TypeEventSystem();

        public void EventTrigger<T1>() where T1 : new()
        {
            m_TypeEventSystem.EventTrigger<T1>();
        }

        public void EventTrigger<T1>(T1 e)
        {
            m_TypeEventSystem.EventTrigger(e);
        }

        public IUnregister AddEventListener<T1>(Action<T1> action)
        {
            return m_TypeEventSystem.AddEventListener(action);
        }

        public void RemoveEventListener<T1>(Action<T1> action)
        {
            m_TypeEventSystem.RemoveEventListener(action);
        }
        #endregion
    }
}
