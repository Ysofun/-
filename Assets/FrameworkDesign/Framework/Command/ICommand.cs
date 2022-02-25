namespace FrameworkDesign
{
    /// <summary>
    /// 命令模式接口，为所有实现类提供一个执行的方法
    /// </summary>
    public interface ICommand : ICanGetArchitecture, ICanSetArchitecture, ICanGetSystem, ICanGetModel, ICanGetUtility, ICanTriggerEvent, ICanSendCommand
    {
        void Execute();
    }

    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture m_Architecture;
        IArchitecture ICanGetArchitecture.GetArchitecture()
        {
            return m_Architecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            m_Architecture = architecture;
        }

        void ICommand.Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();
    }
}
