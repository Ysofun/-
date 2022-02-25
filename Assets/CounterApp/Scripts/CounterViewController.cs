using UnityEngine;
using UnityEngine.UI;
using FrameworkDesign;

namespace CounterApp
{
    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }

    public class CounterModel : AbstractModel, ICounterModel
    {
        public BindableProperty<int> Count { get; } = new BindableProperty<int>() { Value = 0 };

        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTER_COUNT", 0);

            Count.AddValueChangedListener(count => storage.SaveInt("COUNTER_COUNT", count));
        }
    }

    public class CounterViewController : MonoBehaviour, IController
    {
        private ICounterModel counterModel;

        void Start()
        {
            counterModel = GetArchitecture().GetModel<ICounterModel>();

            counterModel.Count.AddValueChangedListener(OnCountChange);

            transform.Find("BtnAdd").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    this.SendCommand<AddCountCommand>();
                });

            transform.Find("BtnSub").GetComponent<Button>()
                .onClick.AddListener(() =>
                {
                    this.SendCommand<SubCountCommand>();
                });

            OnCountChange(counterModel.Count.Value);
        }

        private void OnCountChange(int obj)
        {
            transform.Find("Number").GetComponent<Text>().text = counterModel.Count.Value.ToString();
        }

        private void OnDestroy()
        {
            counterModel.Count.RemoveValueChangedListener(OnCountChange);
        }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.MyInterface;
        }
    }
}
