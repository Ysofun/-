namespace FrameworkDesign.Example
{
    public class MissCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            if (gameModel.life.Value > 0)
            {
                gameModel.life.Value--;
            }
            else
            {
                this.EventTrigger<OnMissEvent>();
            }     
        }
    }
}