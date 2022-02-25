using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GamePassPanel : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return PointGame.MyInterface;
        }

        private void SetText(string TextName, string str)
        {
            transform.Find(TextName).GetComponent<Text>().text = str; 
        }

        void Start()
        {
            var gameModel = this.GetModel<IGameModel>();
            SetText("ScoreText", "�÷֣�" + gameModel.scoreCount.Value.ToString());
            SetText("BestScoreText", "��߷֣�" + gameModel.bestScore.Value.ToString());
            SetText("RemainTimeText", "ʣ��ʱ�䣺" + this.GetSystem<ICountDownEndSystem>().CurrentRemainSeconds.ToString() + "s");
        }
    }
}
