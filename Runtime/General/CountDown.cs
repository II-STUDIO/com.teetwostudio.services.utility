using Services.EventsSystem;
using System;
using UnityEngine;

namespace Services
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

        [Tooltip("Invoke when start countdown")]
        public EventAction<float> onStart;
        [Tooltip("Invoke every countodwn update")]
        public EventAction<float> onUpdate;
        [Tooltip("Invoke when countodwn complete")]
        public EventAction onComplete;


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

            onUpdate?.Invoke(_countDownStep);

            if (_countDownStep > 0f)
                return;

            _countDownStep = 0f;

            onComplete?.Invoke();
        }
    }
}
