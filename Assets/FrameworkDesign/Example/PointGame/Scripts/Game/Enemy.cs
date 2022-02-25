using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }

        private void OnMouseDown()
        {
            gameObject.SetActive(false);
            this.SendCommand<KillEnemyCommand>();
        }
    }
}