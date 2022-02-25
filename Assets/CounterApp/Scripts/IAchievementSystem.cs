using FrameworkDesign;
using UnityEngine;

namespace CounterApp
{
    public interface IAchievementSystem : ISystem
    {

    }

    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        protected override void OnInit()
        {
            var counterModel = this.GetModel<ICounterModel>();
            var previousCount = counterModel.Count.Value;

            counterModel.Count.AddValueChangedListener(newCount =>
            {
                if (newCount >= 10 && previousCount < 10)
                {
                    Debug.Log("解锁成就：10");
                }

                else if (newCount >= 20 && previousCount < 20)
                {
                    Debug.Log("解锁成就：20");
                }

                previousCount = newCount;
            });
        }
    }
}
