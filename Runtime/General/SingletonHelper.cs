using UnityEngine;
using Services;

namespace Services.Utility.Core
{
    public static class SingletonHelper
    {
        /// <summary>
        /// Find global instance.
        /// </summary>
        public static Inherister FindInstance<Inherister>(Inherister instance) where Inherister : MonoBehaviour
        {
            if (instance)
                return instance;

            Inherister[] inheristers = Object.FindObjectsOfType<Inherister>(true);

            if (inheristers == null || inheristers.Length == 0)
            {
                string name = typeof(Inherister).Name;
                Debug.LogWarning($"The type of <{name}> not arriv or found -> auto generate one.");
                return new GameObject(name + " - Singleton (Auto Create)").AddComponent<Inherister>();
            }

            if (inheristers.Length > 1)
                Debug.LogWarning($"The type of <{nameof(Inherister)}> arrived more that one.");

            return inheristers[0];
        }

        /// <summary>
        /// Get Instance of mono object.
        /// </summary>
        /// <typeparam name="Inherister"></typeparam>
        /// <param name="gameObject"></param>
        /// <param name="instance"></param>
        /// <param name="replacementType"></param>
        /// <returns></returns>
        public static Inherister GetInstance<Inherister>(GameObject gameObject, Inherister instance, SigletonPlacementType replacementType = SigletonPlacementType.Noramal) where Inherister : MonoBehaviour
        {
            Inherister inheriter = gameObject.GetComponent<Inherister>();

            if (inheriter == null)
                return instance;

            switch (replacementType)
            {
                case SigletonPlacementType.Noramal:
                    if (instance)
                        return instance;
                    break;
                case SigletonPlacementType.DontroyPrivious:
                    if (instance)
                        Object.Destroy(instance.gameObject);
                    break;
            }

            instance = inheriter;

            return instance;
        }
    }
}