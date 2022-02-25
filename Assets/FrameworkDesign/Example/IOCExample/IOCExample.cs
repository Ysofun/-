using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var container = new IOCContainer();

            container.Register<IBluetoothManager>(new BluetoothManager());

            IBluetoothManager bluetoothManager = container.Get<IBluetoothManager>();

            bluetoothManager.Connect();
        }
    }

    public interface IBluetoothManager
    {
        void Connect();
    }

    public class BluetoothManager : IBluetoothManager
    {
        public void Connect()
        {
            Debug.Log("蓝牙连接成功");
        }
    }
}
