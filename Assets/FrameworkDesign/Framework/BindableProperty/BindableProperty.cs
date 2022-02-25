using System;

namespace FrameworkDesign
{
    /// <summary>
    /// 可绑定属性，通过属性封装字段，在属性值改变时，调用委托中引用的方法。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T m_value;
        public T Value
        {
            get => m_value;
            set
            {
                if (!m_value.Equals(value))
                {
                    m_value = value;
                    OnValueChanged?.Invoke(Value);
                }
            }
        }

        private Action<T> OnValueChanged = v => { };

        public IUnregister AddValueChangedListener(Action<T> action)
        {
            OnValueChanged += action;
            return new BindablePropertyUnregister<T>() { BindableProperty = this, OnValueChanged = OnValueChanged };
        }

        public void RemoveValueChangedListener(Action<T> action)
        {
            OnValueChanged -= action;
        }
    }

    public class BindablePropertyUnregister<T> : IUnregister where T : IEquatable<T>
    {
        public BindableProperty<T> BindableProperty { get; set; }
        public Action<T> OnValueChanged { get; set; }

        public void Unregister()
        {
            BindableProperty.RemoveValueChangedListener(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }

}
