using UnityEngine;

namespace Services
{
    /// <summary>
    /// This is optimize raycast to not caste allocator physic and resuse the raycast hit property in 3D.
    /// </summary>
    public class Raycaster
    {
        /// <summary>
        /// Dedicate new not allocat physic raycaster defualt container size is -> 1.
        /// </summary>
        public Raycaster()
        {
            Hits = new RaycastHit[1];
            HitCount = 0;
        }

        /// <summary>
        /// Dedicate new not allocat physic raycaster with custom max container size.
        /// </summary>
        public Raycaster(int maxHitContainable)
        {
            Hits = new RaycastHit[maxHitContainable];
            HitCount = 0;
        }

        /// <summary>
        /// Container of the hit of raycast.
        /// </summary>
        public RaycastHit[] Hits { get; private set; }

        /// <summary>
        /// Count of currenct of lasted raycast hits maximum is follow 'Hits' lengh.
        /// </summary>
        public int HitCount { get; private set; }

        public bool IsReady { get; private set; }

        /// <summary>
        /// This property will automatical set the ray every cast funtion follow the lastest transform form 'SetRay(Transform transform)' funtion.
        /// </summary>
        public bool UseLastestTransform { get; set; }

        /// <summary>
        /// Return the firstest raycast hit of the 'Hits' cotnain.
        /// </summary>
        public RaycastHit FirstHit
        {
            get => Hits[0];
        }

        /// <summary>
        /// Return the lasted raycast hit of the 'Hits' cotnain.
        /// </summary>
        public RaycastHit LastHit
        {
            get => Hits[HitCount - 1];
        }

        /// <summary>
        /// Return when hits is empty.
        /// </summary>
        public bool IsEmpty
        {
            get => HitCount == 0;
        }


        private Ray _ray;
        private Transform _lastestRayTransform;

        /// <summary>
        /// Set the ray property with transform forward for cast the ray.
        /// </summary>
        /// <param name="transform"></param>
        public void SetRay(Transform transform)
        {
            if (!transform)
            {
                Debug.LogErrorFormat("Can't set ray because the transform is null or empty");
                return;
            }

            SetRay(transform.position, transform.forward);

            _lastestRayTransform = transform;
        }

        /// <summary>
        /// Set the ray property for cast the ray.
        /// </summary>
        /// <param name="ray"></param>
        public void SetRay(Ray ray)
        {
            _ray = ray;

            IsReady = true;
        }

        /// <summary>
        /// Set the ray property for cast the ray.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public void SetRay(Vector3 position, Vector3 direction)
        {
            _ray.origin = position;
            _ray.direction = direction;

            IsReady = true;
        }

        /// <summary>
        ///  Create reuse raycast to detect the colliders.
        /// </summary>
        /// <returns>True when hit colliders</returns>
        public bool Cast()
        {
            if (!IsReady)
            {
                Debug.LogWarningFormat("Ray is not ready pleas set ray first");
                return false;
            }

            CheckUseLastedTransformAndAutoSetup();

            HitCount = Physics.RaycastNonAlloc(_ray, Hits);

            return HitCount > 0;
        }

        /// <summary>
        /// Create reuse raycast to detect the colliders with limit distance.
        /// </summary>
        /// <param name="distance"></param>
        /// <returns>True when hit colliders</returns>
        public bool Cast(float distance)
        {
            if (!IsReady)
            {
                Debug.LogWarningFormat("Ray is not ready pleas set ray first");
                return false;
            }

            CheckUseLastedTransformAndAutoSetup();

            HitCount = Physics.RaycastNonAlloc(_ray, Hits, distance);

            return HitCount > 0;
        }

        /// <summary>
        /// Create reuse raycast to detect the colliders with limit distance and layer filters.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        /// <returns>True when hit colliders that matches layer mask</returns>
        public bool Cast(float distance, LayerMask layerMask)
        {
            if (!IsReady)
            {
                Debug.LogWarningFormat("Ray is not ready pleas set ray first");
                return false;
            }

            CheckUseLastedTransformAndAutoSetup();

            HitCount = Physics.RaycastNonAlloc(_ray, Hits, distance, layerMask);

            return HitCount > 0;
        }

        /// <summary>
        /// Create reuse raycast to detect the colliders with limit distance and layer filters this will ignore trigger collider in the process.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="layerMask"></param>
        /// <returns>True when hit colliders that matches layer mask</returns>
        public bool CastIgnoreTrigger(float distance, LayerMask layerMask)
        {
            if (!IsReady)
            {
                Debug.LogWarningFormat("Ray is not ready pleas set ray first");
                return false;
            }

            CheckUseLastedTransformAndAutoSetup();

            HitCount = Physics.RaycastNonAlloc(_ray, Hits, distance, layerMask, QueryTriggerInteraction.Ignore);

            return HitCount > 0;
        }

        private void CheckUseLastedTransformAndAutoSetup()
        {
            if (!UseLastestTransform)
                return;

            if (!_lastestRayTransform)
            {
                Debug.LogWarningFormat("The use lastest transform is not ready nedd to set ray follow 'SetRay(Transform transform)' frist");
                return;
            }

            SetRay(_lastestRayTransform);
        }
    }
}