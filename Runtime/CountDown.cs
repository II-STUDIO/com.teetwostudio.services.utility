using System;

namespace Services.Utility
{
    public struct CountDown
    {
        private float _countDownStep;

        public float currnetDuration
        {
            get => _countDownStep;
        }

        public bool isComplelted
        {
            get => _countDownStep == 0f;
        }

        public Action<float> onStart;
        public Action onComplete;


        public void Start(float countDownTime)
        {
            onStart?.Invoke(countDownTime);

            if (countDownTime == 0f)
            {
                onComplete?.Invoke();
                return;
            }

            _countDownStep = countDownTime;
        }

        public void ForceComplete()
        {
            if (isComplelted)
                return;

            _countDownStep = 0f;

            onComplete?.Invoke();
        }

        public void Clear()
        {
            _countDownStep = 0f;
        }

        public void Add(float countDownTime)
        {
            _countDownStep += countDownTime;
        }

        public void Update(float deltaTime)
        {
            if (_countDownStep == 0f)
                return;

            _countDownStep -= deltaTime;

            if (_countDownStep > 0f)
                return;

            _countDownStep = 0f;

            onComplete?.Invoke();
        }
    }
}
