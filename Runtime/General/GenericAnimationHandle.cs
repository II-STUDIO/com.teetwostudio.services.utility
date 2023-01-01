using Cysharp.Threading.Tasks;
using Services.EventsSystem;
using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public struct GenericAnimationHandle
    {
        [SerializeField] private AnimationClip clip;

        private Animation _animation;
        private EventAction _onStart;
        private EventAction _onComplete;

        /// <summary>
        /// Total duration of animation clip.
        /// </summary>
        private int _clibMillisecond;

        public bool IsInitialized { get; private set; }

        public void Initialize(Animation animation)
        {
            _animation = animation;

            IsInitialized = true;

            _clibMillisecond = (int)(clip.length * 1000);
        }

        public void SetListener(EventAction onStart, EventAction onComplete)
        {
            _onStart = onStart;
            _onComplete = onComplete;
        }

        public async void Play()
        {
            if (!IsInitialized)
            {
                Debug.LogWarningFormat("Aniamtion handle need to initailize first");
                return;
            }

            _onStart?.Invoke();

            _animation.Play(clip.name);

            await UniTask.Delay(_clibMillisecond);

            _onComplete?.Invoke();
        }
    }
}