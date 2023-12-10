using UnityEngine;

namespace Services
{
    public class SystemBaseUpdater : MonoSingleton<SystemBaseUpdater>
    {
        [SerializeField] private bool dontDestroyOnLoad = true;

        private event CoroutinUpdatable coroutinUpdatable;

        protected override void Awake()
        {
            base.Awake();

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            SystemTime.UpdateDeltaTime();

            if (coroutinUpdatable != null)
                coroutinUpdatable(SystemTime.DeltaTime);
        }

        public void AddUpdater(CoroutinUpdatable updatable)
        {
            coroutinUpdatable += updatable;
        }

        public void RemoveUpdater(CoroutinUpdatable updatable)
        {
            coroutinUpdatable -= updatable;
        }
    }

    public delegate void CoroutinUpdatable(float deltaTime);
}