namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();

            gameModel.killCount.Value++;

            if (UnityEngine.Random.Range(0, 10) < 3)
            {
                gameModel.goldCount.Value += UnityEngine.Random.Range(1, 3);
            }

            this.EventTrigger<OnEnemyKillEvent>();

            if (gameModel.killCount.Value == 10)
            {
                this.EventTrigger<GamePassEvent>();
            }
        }
    }
}
