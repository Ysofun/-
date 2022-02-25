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
            SetText("ScoreText", "得分：" + gameModel.scoreCount.Value.ToString());
            SetText("BestScoreText", "最高分：" + gameModel.bestScore.Value.ToString());
            SetText("RemainTimeText", "剩余时间：" + this.GetSystem<ICountDownEndSystem>().CurrentRemainSeconds.ToString() + "s");
        }
    }
}
