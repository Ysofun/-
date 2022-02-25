namespace FrameworkDesign
{
    public interface IController : ICanGetArchitecture, ICanGetSystem, ICanGetModel, ICanSendCommand, ICanRegisterEvent
    {
    }
}
