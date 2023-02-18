using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public struct GenericTransition
    {
        [SerializeField] private Animation animation;
        [Space]
        [SerializeField] private GenericAnimationHandle fadeIn;
        [SerializeField] private GenericAnimationHandle fadeOut;

        public float FadeInTime
        {
            get => fadeIn.Time;
        }

        public float FadeOutTime
        {
            get => fadeOut.Time;
        }

        public bool IsProcessing { get; private set; }

        public bool IsInitialized { get; private set; }

        private ITransitionHandle _handle;

        public void Initialize(ITransitionHandle handle)
        {
            if (IsInitialized)
                return;

            if (!animation)
                return;

            _handle = handle;

            fadeIn.Initialize(animation);
            fadeOut.Initialize(animation);

            fadeIn.SetListener(OnFadeInStart, OnFadeInCompelte);
            fadeOut.SetListener(OnFadeOutStart, OnFadeOutComplete);

            IsInitialized = true;
        }

        public void FadeIn()
        {
            if (!IsInitialized)
            {
                Debug.LogWarningFormat("Panel trasition need to initialize first");
                return;
            }

            if (IsProcessing)
                return;

            fadeIn.Play();
        }

        public void FadeOut()
        {
            if (!IsInitialized)
            {
                Debug.LogWarningFormat("Panel trasition need to initialize first");
                return;
            }

            if (IsProcessing)
                return;

            fadeOut.Play();
        }

        #region FadeIn Event
        private void OnFadeInStart()
        {
            IsProcessing = true;

            if (_handle != null)
                _handle.OnFadeInBegin();
            //_handle.Enable();
        }

        private void OnFadeInCompelte()
        {
            IsProcessing = false;

            if (_handle != null)
                _handle.OnFadeInComplete();
        }

        #endregion

        #region FadeOut Event
        private void OnFadeOutStart()
        {
            IsProcessing = true;

            if (_handle != null)
                _handle.OnFadeOutBegin();
        }

        private void OnFadeOutComplete()
        {
            IsProcessing = false;

            if (_handle != null)
                _handle.OnFadeOutComplete();
            //_handle.Disable();
        }
        #endregion
    }

    public interface ITransitionHandle
    {
        public void OnFadeInBegin();
        public void OnFadeInComplete();
        public void OnFadeOutBegin();
        public void OnFadeOutComplete();
    }
}