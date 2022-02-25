namespace FrameworkDesign.Example
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> killCount { get; }
        BindableProperty<int> goldCount { get; }
        BindableProperty<int> scoreCount { get; }
        BindableProperty<int> bestScore { get; }
        BindableProperty<int> life { get; }
    }


    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> killCount { get; } = new BindableProperty<int>() { Value = 0 };
        public BindableProperty<int> goldCount { get; } = new BindableProperty<int>() { Value = 0 };
        public BindableProperty<int> scoreCount { get; } = new BindableProperty<int>() { Value = 0 };
        public BindableProperty<int> bestScore { get; } = new BindableProperty<int>() { Value = 0 };
        public BindableProperty<int> life { get; } = new BindableProperty<int> { Value = 0 };

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();

            bestScore.Value = storage.LoadInt(nameof(bestScore), 0);
            bestScore.AddValueChangedListener(v => storage.SaveInt(nameof(bestScore), v));

            life.Value = storage.LoadInt(nameof(life), 0);
            life.AddValueChangedListener(v => storage.SaveInt(nameof(life), v));

            goldCount.Value = storage.LoadInt(nameof(goldCount), 0);
            goldCount.AddValueChangedListener(v => storage.SaveInt(nameof(goldCount), v));
        }
    }
}

