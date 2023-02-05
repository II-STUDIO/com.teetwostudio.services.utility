using System.Collections;

namespace Services
{
    public class SystemBaseUpdater : MonoSingleton<SystemBaseUpdater>
    {

        private CoroutinUpdatable coroutinUpdatable;

        protected override void Awake()
        {
            base.Awake();

            StartCoroutine(CoroutinUpdater());
        }

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

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }

    public delegate void CoroutinUpdatable(float deltaTime);
}