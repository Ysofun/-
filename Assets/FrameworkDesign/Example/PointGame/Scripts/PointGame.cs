namespace FrameworkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterSystem<ICountDownEndSystem>(new CountDownEndSystem());
            RegisterSystem<IAchievementSystem>(new AchievementSystem());
            RegisterSystem<IScoreSystem>(new ScoreSystem());
            RegisterModel<IGameModel>(new GameModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
    }

}
