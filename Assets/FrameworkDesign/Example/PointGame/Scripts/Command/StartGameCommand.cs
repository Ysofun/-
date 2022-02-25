namespace FrameworkDesign.Example
{
    public class StartGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();

            gameModel.killCount.Value = 0;
            gameModel.scoreCount.Value = 0;
            this.EventTrigger<GameStartEvent>();
        }
    }

}
