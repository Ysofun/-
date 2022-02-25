using UnityEngine;

namespace FrameworkDesign.Example
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface ICustomScript
    {
        void Start();
        void Update();
        void Destroy();
    }

    /// <summary>
    /// 抽象类
    /// </summary>
    public abstract class CustomScript : ICustomScript
    {
        void ICustomScript.Destroy()
        {
            OnStart();
        }

        void ICustomScript.Start()
        {
            OnUpdate();
        }

        void ICustomScript.Update()
        {
            OnDestroy();
        }

        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnDestroy();
    }

    /// <summary>
    /// 实现类
    /// </summary>
    public class MyScript : CustomScript
    {
        protected override void OnDestroy()
        {
            Debug.Log("MyScript_OnDestroy");
        }

        protected override void OnStart()
        {
            Debug.Log("MyScript_OnStart");
        }

        protected override void OnUpdate()
        {
            Debug.Log("MyScript_OnUpdate");
        }
    }
    public class InterfaceStructExample : MonoBehaviour
    {
        void Start()
        {
            ICustomScript script = new MyScript();
            script.Start();
            script.Update();
            script.Destroy();
        }
    }

}
