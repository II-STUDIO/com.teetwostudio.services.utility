using UnityEngine;

namespace Services
{
    //
    // Summary:
    //     Rotate transfrom with unity update.
    public class RotatorWithUnity : MonoBehaviour
    {
        [SerializeField] private Vector3 axis = Vector3.forward;
        [SerializeField] private float multple = 1f;

        Transform m_transform;

        private void Start()
        {
            m_transform = transform;
        }

        private void Update()
        {
            m_transform.Rotate(axis * multple * SystemTime.DeltaTime);
        }
    }
}