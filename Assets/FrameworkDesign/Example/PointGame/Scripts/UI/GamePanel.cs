using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GamePanel : MonoBehaviour, IController
    {
        private ICountDownEndSystem m_CountDownSystem;
        private IGameModel m_GameModel;

        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }

        private void Awake()
        {
            m_CountDownSystem = this.GetSystem<ICountDownEndSystem>();
            m_GameModel = this.GetModel<IGameModel>();

            m_GameModel.goldCount.AddValueChangedListener(OnGoldValueChanged);
            m_GameModel.life.AddValueChangedListener(OnLifeValueChanged);
            m_GameModel.scoreCount.AddValueChangedListener(OnScoreValueChanged);

            OnScoreValueChanged(m_GameModel.scoreCount.Value);
            OnLifeValueChanged(m_GameModel.life.Value);
            OnGoldValueChanged(m_GameModel.goldCount.Value);
        }

        private void OnScoreValueChanged(int obj)
        {
            transform.Find("ScoreText").GetComponent<Text>().text = "分数：" + obj;
        }

        private void OnLifeValueChanged(int obj)
        {
            transform.Find("LifeText").GetComponent<Text>().text = "生命：" + obj;
        }

        private void OnGoldValueChanged(int obj)
        {
            transform.Find("GoldText").GetComponent<Text>().text = "金币：" + obj;
        }

        void Update()
        {
            if (Time.frameCount % 20 == 0)
            {
                transform.Find("CountDownText").GetComponent<Text>().text = "倒计时：" + m_CountDownSystem.CurrentRemainSeconds + "s";
                m_CountDownSystem.Update();
            }
        }

        private void OnDestroy()
        {
            m_GameModel.goldCount.RemoveValueChangedListener(OnGoldValueChanged);
            m_GameModel.life.RemoveValueChangedListener(OnLifeValueChanged);
            m_GameModel.scoreCount.RemoveValueChangedListener(OnScoreValueChanged);

            m_GameModel = null;
            m_CountDownSystem = null;
        }
    }
}