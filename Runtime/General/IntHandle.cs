using System;

namespace Services.Utility
{
    [Serializable]
    public struct IntHandle
    {
        private int value;

        public int Value
        {
            get => value;
        }

        public Action onValueChanged { get; set; }
        public Action<int> onValueChangedWithValue { get; set; }

        public void Increase(int value)
        {
            this.value += value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }

        public void Decrease(int value)
        {
            this.value -= value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }

        public void Set(int value)
        {
            this.value = value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }
    }

    [Serializable]
    public struct FloatHandle
    {
        public float value { get; private set; }

        public Action onValueChanged { get; set; }
        public Action<float> onValueChangedWithValue { get; set; }

        public void Increase(float value)
        {
            this.value += value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }

        public void Decrease(float value)
        {
            this.value -= value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }

        public void Set(float value)
        {
            this.value = value;

            onValueChanged?.Invoke();
            onValueChangedWithValue?.Invoke(this.value);
        }
    }
}