namespace FrameworkDesign
{
    public interface ISystemArchitecture
    {
        void RegisterSystem<T>(T system) where T : ISystem;
        T GetSystem<T>() where T : class, ISystem;
    }

}
