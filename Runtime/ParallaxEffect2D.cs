using UnityEngine;

namespace Services
{
    public class ParallaxEffect2D : MonoBehaviour
    {
        [SerializeField] private float _effect;

        private Transform _cam;
        private float _length;
        private float _startPosX;

        private Vector3 _camPosition;
        private Vector3 _effectPosition;

        void Start()
        {
            _cam = Camera.main.transform;
            _startPosX = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void Update()
        {
            _camPosition = _cam.position;
            float temp = (_camPosition.x * (1 - _effect));
            float distance = (_camPosition.x * _effect);

            _effectPosition = transform.position;
            _effectPosition.x = _startPosX + distance;
            transform.position = _effectPosition;

            if (temp > _startPosX + _length)
                _startPosX += _length;
            else if (temp < _startPosX - _length)
                _startPosX -= _length;
        }
    }
}
