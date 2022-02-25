namespace FrameworkDesign
{
    public interface ISystem : ICanGetArchitecture, ICanSetArchitecture, ICanGetModel, ICanGetUtility, ICanTriggerEvent, ICanRegisterEvent, ICanGetSystem
    {
        void Init();
    }

    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture m_Architecture = null;
        IArchitecture ICanGetArchitecture.GetArchitecture()
        {
            return m_Architecture;
        }

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            m_Architecture = architecture;
        }

        void ISystem.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }

}
