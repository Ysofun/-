namespace FrameworkDesign.Example
{
    public interface IScoreSystem : ISystem { }

    public class ScoreSystem : AbstractSystem, IScoreSystem
    {
        protected override void OnInit()
        {            
            var gameModel = this.GetModel<IGameModel>();

            this.AddEventListener<GamePassEvent>(e =>
            {
                var countDownEndSystem = this.GetSystem<ICountDownEndSystem>();
                var timeScore = countDownEndSystem.CurrentRemainSeconds * 10;
                gameModel.scoreCount.Value += timeScore;

                if (gameModel.scoreCount.Value > gameModel.bestScore.Value)
                {
                    gameModel.bestScore.Value = gameModel.scoreCount.Value;
                }
            });

            this.AddEventListener<OnEnemyKillEvent>(e =>
            {
                gameModel.scoreCount.Value += 10;
            });

            this.AddEventListener<OnMissEvent>(e =>
            {
                gameModel.scoreCount.Value -= 5;
            });
        }
    }
}
