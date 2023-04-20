using System;

namespace Services.Utility
{
    [Serializable]
    public struct IntHandle
    {
        public int value { get; private set; }

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

        public static IntHandle operator +(IntHandle a, IntHandle b)
        {
            a.Increase(b.value);
            return a;
        }

        public static IntHandle operator -(IntHandle a, IntHandle b)
        {
            a.Decrease(b.value);
            return a;
        }

        public static IntHandle operator +(IntHandle a, int b)
        {
            a.Increase(b);
            return a;
        }

        public static IntHandle operator -(IntHandle a, int b)
        {
            a.Decrease(b);
            return a;
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

        public static FloatHandle operator +(FloatHandle a, FloatHandle b)
        {
            a.Increase(b.value);
            return a;
        }

        public static FloatHandle operator -(FloatHandle a, FloatHandle b)
        {
            a.Decrease(b.value);
            return a;
        }

        public static FloatHandle operator +(FloatHandle a, int b)
        {
            a.Increase(b);
            return a;
        }

        public static FloatHandle operator -(FloatHandle a, int b)
        {
            a.Decrease(b);
            return a;
        }
    }
}