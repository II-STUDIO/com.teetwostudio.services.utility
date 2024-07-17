using Services.EventsSystem;
using UnityEngine;

namespace Services
{
    public struct CountDown : ILoopUpdateEntity
    {
        private float currentTime;

        [Tooltip("Invoke when start countdown")]
        public EventAction<float> onStart;
        [Tooltip("Invoke every countodwn update")]
        public EventAction<float> onUpdate;
        [Tooltip("Invoke when countodwn complete")]
        public EventAction onComplete;

        private bool isPause;
        private bool isAutoUpdate;

        public float CurrentTime
        {
            get => currentTime;
        }

        public bool IsCounting
        {
            get => currentTime > 0f;
        }

        public bool IsUpdatable => true;

        public void Start(float countDownTime, bool isAutoUpdate = false)
        {
            onStart?.Invoke(countDownTime);

            if (countDownTime == 0f)
            {
                onComplete?.Invoke();
                return;
            }

            currentTime = countDownTime;

            this.isAutoUpdate = isAutoUpdate;

            if (isAutoUpdate)
            {
                LoopUpdater.EnableEntity(this);
            }
        }

        public void ForceComplete()
        {
            if (!IsCounting)
                return;

            Completed();
        }

        public void Clear()
        {
            currentTime = 0f;

            if (isAutoUpdate)
            {
                LoopUpdater.DisableEntity(this);

                isAutoUpdate = false;
            }
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Resume()
        {
            isPause = false;
        }

        public void Add(float countDownTime)
        {
            currentTime += countDownTime;
        }

        public void LoopUpdateEvent(float deltaTime)
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

            if (isAutoUpdate)
            {
                LoopUpdater.DisableEntity(this);

                isAutoUpdate = false;
            }

            onComplete?.Invoke();
        }
    }
}
