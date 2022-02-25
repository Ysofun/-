namespace FrameworkDesign
{
    /// <summary>
    /// model��ͳһ�ӿ�
    /// </summary>
    public interface IModel : ICanGetArchitecture, ICanSetArchitecture, ICanGetUtility, ICanTriggerEvent
    {
        void Init();
    }

    public abstract class AbstractModel : IModel
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

        void IModel.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
    }

}
