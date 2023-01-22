using UnityEngine;

namespace Services
{
    public class MonoSingleton<Inherister> : MonoBehaviour where Inherister : MonoBehaviour
    {
        public static Inherister Instance
        {
            get
            {
                if (!_instance)
                {
                    Inherister[] inheristers = FindObjectsOfType<Inherister>();

                    if (inheristers == null)
                    {
                        Debug.LogError("The type of <{nameof(Inherister)}> not arriv or found.");
                        return null;
                    }

                    if (inheristers.Length > 1)
                        Debug.LogWarning($"The type of <{nameof(Inherister)}> arrived more that one.");

                    _instance = inheristers[0];
                }

                return _instance;
            }
        }

        private static Inherister _instance;

        protected virtual void Awake()
        {
            SetInstance(GetComponent<Inherister>());
        }

        /// <summary>
        /// Set this panel to instance
        /// </summary>
        /// <param name="inheriter"></param>
        protected void SetInstance(Inherister inheriter, SigletonPlacementType replacementType = SigletonPlacementType.Noramal)
        {
            if (inheriter == null)
                return;

            switch (replacementType)
            {
                case SigletonPlacementType.Noramal:
                    if (_instance)
                        return;
                    break;
                case SigletonPlacementType.DontroyPrivious:
                    if (_instance)
                        Destroy(Instance.gameObject);
                    break;
            }

            _instance = inheriter;
        }
    }
}