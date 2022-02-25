namespace FrameworkDesign
{
    public interface IUtilityArchitecture
    {
        void RegisterUtility<T>(T utility);
        T GetUtility<T>() where T : class;
    }

}
