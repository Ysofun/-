using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour, IController
    {
        void Start()
        {
            this.AddEventListener<GamePassEvent>(OnGamePass).UnregisterWhenGameObjectDestroyed(gameObject);
            this.AddEventListener<OnCountDownEndEvent>(OnGameOver).UnregisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnGameOver(OnCountDownEndEvent obj)
        {
            transform.Find("Canvas/GameOverPanel").gameObject.SetActive(true);
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
        }

        private void OnGamePass(GamePassEvent gamePassEvent)
        {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
        }

        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }
    }
}
