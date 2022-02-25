using UnityEngine;

namespace FrameworkDesign.Example
{
    public class ErrorArea : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }

        private void OnMouseDown()
        {
            this.SendCommand<MissCommand>();
        }

    }
}
