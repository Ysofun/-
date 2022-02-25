using System;

namespace FrameworkDesign.Example
{
    public interface ICountDownEndSystem : ISystem
    {
        int CurrentRemainSeconds { get; }
        void Update();
    }

    public class CountDownEndSystem : AbstractSystem, ICountDownEndSystem
    {
        private int m_GameTime = 10;
        private bool m_Started;
        private DateTime m_GameStartTime;

        public int CurrentRemainSeconds => m_GameTime - (int)(DateTime.Now - m_GameStartTime).TotalSeconds;

        public void Update()
        {
            if (m_Started)
            {
                if (DateTime.Now - m_GameStartTime > TimeSpan.FromSeconds(m_GameTime))
                {
                    this.EventTrigger<OnCountDownEndEvent>();
                    m_Started = false;
                }
            }
        }

        protected override void OnInit()
        {
            this.AddEventListener<GameStartEvent>(e =>
            {
                m_Started = true;
                m_GameStartTime = DateTime.Now;
            });

            this.AddEventListener<GamePassEvent>(e =>
            {
                m_Started = false;
            });
        }
    }
}