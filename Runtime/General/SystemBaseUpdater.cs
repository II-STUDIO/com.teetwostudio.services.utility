using System.Collections;

namespace Services
{
    public class SystemBaseUpdater : MonoSingleton<SystemBaseUpdater>
    {
        protected override void Awake()
        {
            if (IsInstanceValidable())
            {
                Destroy(gameObject);
                return;
            }

            base.Awake();

            DontDestroyOnLoad(this);

            StartCoroutine(CoroutinUpdater());
        }

        private CoroutinUpdatable coroutinUpdatable;

        private IEnumerator CoroutinUpdater()
        {
            while (true)
            {
                SystemTime.UpdateDeltaTime();

                if (coroutinUpdatable != null)
                    coroutinUpdatable(SystemTime.DeltaTime);

                yield return null;
            }
        }

        public void AddUpdater(CoroutinUpdatable updatable)
        {
            coroutinUpdatable += updatable;
        }

        public void RemoveUpdater(CoroutinUpdatable updatable)
        {
            coroutinUpdatable -= updatable;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();   
        }
    }

    public delegate void CoroutinUpdatable(float deltaTime);
}