namespace FrameworkDesign.Example
{
    public class BuyLifeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.goldCount.Value--;
            gameModel.life.Value++;
        }
    }
}