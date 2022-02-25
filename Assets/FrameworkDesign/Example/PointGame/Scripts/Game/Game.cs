using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour, IController
    {
        private void Start()
        {
            this.AddEventListener<GameStartEvent>(OnGameStart).UnregisterWhenGameObjectDestroyed(gameObject);
            this.AddEventListener<OnCountDownEndEvent>(e =>
            {
                transform.Find("Enemies").gameObject.SetActive(false);
            }).UnregisterWhenGameObjectDestroyed(gameObject);

            this.AddEventListener<GamePassEvent>(e =>
            {
                transform.Find("Enemies").gameObject.SetActive(false);
            }).UnregisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGameStart(GameStartEvent gameStartEvent)
        {
            var enemyRoot = transform.Find("Enemies");
            enemyRoot.gameObject.SetActive(true);
            foreach (Transform child in enemyRoot)
            {
                child.gameObject.SetActive(true);
            }
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }
    }
}
