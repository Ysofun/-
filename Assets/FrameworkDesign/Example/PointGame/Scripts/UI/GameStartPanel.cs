using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        /// <summary>
        /// 所有Enmey节点的父节点
        /// </summary>
        public GameObject enemies;

        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }

        private IGameModel m_GameModel;

        void Start()
        {
            transform.Find("BtnGameStart").GetComponent<Button>().onClick.AddListener(() =>
             {
                gameObject.SetActive(false);
                this.SendCommand<StartGameCommand>();
            });

            transform.Find("BtnBuyLife").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<BuyLifeCommand>();
            });

            m_GameModel = this.GetModel<IGameModel>();

            m_GameModel.goldCount.AddValueChangedListener(OnGoldValueChanged);
            m_GameModel.life.AddValueChangedListener(OnLifeValueChanged);

            OnGoldValueChanged(m_GameModel.goldCount.Value);
            OnLifeValueChanged(m_GameModel.life.Value);

            transform.Find("BestScoreText").GetComponent<Text>().text = "最高分：" + m_GameModel.bestScore.Value;
        }

        private void OnLifeValueChanged(int obj)
        {
            transform.Find("LifeText").GetComponent<Text>().text = "生命：" + obj;
        }

        private void OnGoldValueChanged(int obj)
        {
            if (obj > 0)
            {
                transform.Find("BtnBuyLife").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("BtnBuyLife").gameObject.SetActive(false);
            }

            transform.Find("GoldText").GetComponent<Text>().text = "金币：" + obj;
        }

        private void OnDestroy()
        {
            m_GameModel.goldCount.RemoveValueChangedListener(OnGoldValueChanged);
            m_GameModel.life.RemoveValueChangedListener(OnLifeValueChanged);
            m_GameModel = null;
        }
    }
}

