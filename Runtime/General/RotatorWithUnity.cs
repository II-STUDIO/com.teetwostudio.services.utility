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

        private Transform _transform;
        private Vector3 _rotationPerSecond;

        private void Awake()
        {
            _transform = transform;
            _rotationPerSecond = axis * multple;
        }

        private void Update()
        {
            _transform.Rotate(_rotationPerSecond * Time.deltaTime, Space.Self);
        }
    }
}