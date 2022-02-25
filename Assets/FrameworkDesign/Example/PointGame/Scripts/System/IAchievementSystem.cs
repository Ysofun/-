using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface IAchievementSystem : ISystem { }

    public class AchievementItem
    {
        public string Name { get; set; }
        public Func<bool> CheckComplete { get; set; }
        public bool unlocked { get; set; }
    }


    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private List<AchievementItem> m_Items = new List<AchievementItem>();
        private bool m_Missed = false;

        protected override void OnInit()
        {
            this.AddEventListener<OnMissEvent>(e =>
            {
                m_Missed = true;
            });

            this.AddEventListener<GameStartEvent>(e =>
            {
                m_Missed = false;
            });

            m_Items.Add(new AchievementItem()
            {
                Name = "百分成就",
                CheckComplete = () => this.GetModel<IGameModel>().bestScore.Value >= 100
            });

            m_Items.Add(new AchievementItem()
            {
                Name = "手残成就",
                CheckComplete = () => this.GetModel<IGameModel>().scoreCount.Value < 0
            });

            m_Items.Add(new AchievementItem()
            {
                Name = "零失误成就",
                CheckComplete = () => !m_Missed
            });

            m_Items.Add(new AchievementItem()
            {
                Name = "零失误成就",
                CheckComplete = () => m_Items.Count(item => item.unlocked) >= 3
            });

            this.AddEventListener<GamePassEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));

                foreach (var item in m_Items)
                {
                    if (!item.unlocked && item.CheckComplete())
                    {
                        item.unlocked = true;
                        Debug.Log("解锁成就：" + item.Name);
                    }
                }
            });
        }
    }

}