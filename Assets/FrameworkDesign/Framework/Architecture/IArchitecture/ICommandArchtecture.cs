namespace FrameworkDesign
{
    public interface ICommandArchtecture
    {
        void SendCommand<T>() where T : ICommand, new();
        void SendCommand<T>(T command) where T : ICommand;
    }

}
