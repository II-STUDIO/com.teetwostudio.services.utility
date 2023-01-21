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
        public float Time { get; private set; }

        public AnimationClip Clip
        {
            get => clip;
        }

        public void Initialize(Animation animation)
        {
            if (!animation)
            {
                Debug.LogWarningFormat("The animation handle can't initialize because input property 'animation' is null or empty");
                return;
            }

            _animation = animation;

            IsInitialized = true;

            if (!clip)
            {
                Debug.LogWarningFormat("The animation clib is null or empty");
                return;
            }

            if (!animation.GetClip(clip.name))
                animation.AddClip(clip, clip.name);

            Time = clip.length;

            _clibMillisecond = (int)(Time * 1000);
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

            if (!clip)
            {
                Debug.LogWarningFormat("The animation clib is null or empty");
                return;
            }

            _onStart?.Invoke();

            _animation.Play(clip.name);

            await UniTask.Delay(_clibMillisecond);

            _onComplete?.Invoke();
        }
    }
}