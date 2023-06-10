using Services.EventsSystem;
using System;
using UnityEngine;

namespace Services
{
    public struct CountDown
    {
        private float currentTime;

        [Tooltip("Invoke when start countdown")]
        public EventAction<float> onStart;
        [Tooltip("Invoke every countodwn update")]
        public EventAction<float> onUpdate;
        [Tooltip("Invoke when countodwn complete")]
        public EventAction onComplete;

        private bool isAutoUpdate;
        private bool isPause;

        public float CurrentTime
        {
            get => currentTime;
        }

        public bool IsCounting
        {
            get => currentTime > 0f;
        }

        public CountDown(bool autoupdate)
        {
            isAutoUpdate = autoupdate;
            isPause = false;
            onStart = null;
            onUpdate = null;
            onComplete = null;
            currentTime = 0f;
        }

        public void Start(float countDownTime)
        {
            onStart?.Invoke(countDownTime);

            if (countDownTime == 0f)
            {
                onComplete?.Invoke();
                return;
            }

            if (isAutoUpdate)
                SystemBaseUpdater.Instance.AddUpdater(Update);

            currentTime = countDownTime;
        }

        public void ForceComplete()
        {
            if (!IsCounting)
                return;

            Completed();
        }

        //
        // Summary:
        //     Like pause but can't resume counting until start.
        public void Clear()
        {
            currentTime = 0f;

            if (isAutoUpdate)
                SystemBaseUpdater.Instance.AddUpdater(Update);
        }

        //
        // Summary:
        //     Pause counting.
        public void Pause()
        {
            isPause = true;
        }

        //
        // Summary:
        //     Resume counting.
        public void Resume()
        {
            isPause = false;
        }

        public void Add(float countDownTime)
        {
            currentTime += countDownTime;
        }

        public void Update(float deltaTime)
        {
            if (isPause)
                return;

            if (currentTime == 0f)
                return;

            currentTime -= deltaTime;

            onUpdate?.Invoke(currentTime);

            if (IsCounting)
                return;

            Completed();
        }

        private void Completed()
        {
            currentTime = 0f;

            onComplete?.Invoke();

            if (isAutoUpdate)
                SystemBaseUpdater.Instance.AddUpdater(Update);
        }
    }
}
