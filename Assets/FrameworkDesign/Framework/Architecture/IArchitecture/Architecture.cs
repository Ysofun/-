using System;
using System.Collections.Generic;

namespace FrameworkDesign
{
    /// <summary>
    /// 架构接口
    /// </summary>
    public interface IArchitecture : IUtilityArchitecture, IModelArchitecture, ISystemArchitecture, ICommandArchtecture, IEventArchitecture
    {

    }

    /// <summary>
    /// 架构，用于实现model层、Utility层在IOC容器中的注册和获取
    /// </summary>
    /// <typeparam name="T0">继承该架构的类型</typeparam>
    public abstract class Architecture<T0> : IArchitecture where T0 : Architecture<T0>, new()
    {

        #region 单例
        /// <summary>
        /// T0是继承类的类型，生成这样一个变量，是为了实现类似于单例模式
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
        /// 确保单例存在，如果没有，就生成
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

        #region System处理
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

        #region Model处理
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

        #region Utility处理
        /// <summary>
        /// 注册Utility层的实例
        /// </summary>
        /// <typeparam name="T1">注册的实例的类型（实例的基类/接口）</typeparam>
        /// <param name="instance">类的实例</param>
        public void RegisterUtility<T1>(T1 instance)
        {
            //调用向IOC容器中注册的方法
            container.Register(instance);
        }

        /// <summary>
        /// 获得Utility层的实例
        /// </summary>
        /// <typeparam name="T1">注册的实例的类型（实例的基类/接口）</typeparam>
        /// <returns></returns>
        public T1 GetUtility<T1>() where T1 : class
        {
            //调用从IOC容器中获得的方法
            return container.Get<T1>();
        }
        #endregion

        /// <summary>
        /// 一个委托，但是经过我的思考和研究，我觉得教程的写法有一些问题，还待考证
        /// </summary>
        public static Action<T0> OnRegisterPatch = architecture => { };

        #region 派生类方法
        /// <summary>
        /// 为派生类提供的初始化方法
        /// </summary>
        protected abstract void Init();
        #endregion

        #region IOC容器处理
        /// <summary>
        /// IOC容器
        /// </summary>
        private IOCContainer container = new IOCContainer();
        #endregion

        #region Command处理
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

        #region 事件处理
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
