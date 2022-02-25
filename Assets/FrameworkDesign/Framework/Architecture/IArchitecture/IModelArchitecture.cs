namespace FrameworkDesign
{
    public interface IModelArchitecture
    {
        void RegisterModel<T>(T model) where T : IModel;
        T GetModel<T>() where T : class, IModel;
    }

}
