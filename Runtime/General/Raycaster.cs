using UnityEngine;

namespace Services
{
    /// <summary>
    /// Optimized reusable raycaster for 3D physics with optional non-allocating behavior.
    /// </summary>
    public class Raycaster
    {
        private Ray _ray;
        private Transform _cachedTransform;

        private readonly RaycastOption _option;

        /// <summary>
        /// Container for raycast hits.
        /// </summary>
        public RaycastHit[] Hits { get; private set; }

        /// <summary>
        /// Number of hits from the last cast. Max is Hits.Length.
        /// </summary>
        public int HitCount { get; private set; }

        /// <summary>
        /// Whether the ray is ready for casting.
        /// </summary>
        public bool IsReady { get; private set; }

        /// <summary>
        /// Automatically update the ray using the latest transform before each cast.
        /// </summary>
        public bool UseCachedTransform { get; set; }

        /// <summary>
        /// Returns the first hit from the last cast.
        /// </summary>
        public RaycastHit FirstHit => Hits[0];

        /// <summary>
        /// Returns the last hit from the last cast.
        /// </summary>
        public RaycastHit LastHit => Hits[HitCount - 1];

        /// <summary>
        /// Returns true if no objects were hit in the last cast.
        /// </summary>
        public bool IsEmpty => HitCount == 0;

        /// <summary>
        /// Initializes the raycaster with default capacity of 1 hit.
        /// </summary>
        public Raycaster(RaycastOption option) : this(option, 1) { }

        /// <summary>
        /// Initializes the raycaster with a custom max hit capacity.
        /// </summary>
        public Raycaster(RaycastOption option, int maxHitCapacity)
        {
            _option = option;
            Hits = new RaycastHit[maxHitCapacity];
            HitCount = 0;
        }

        /// <summary>
        /// Sets the ray using a transform's position and forward direction.
        /// </summary>
        public void SetRay(Transform transform)
        {
            if (transform == null)
            {
                Debug.LogError("Cannot set ray: Transform is null.");
                return;
            }

            _ray.origin = transform.position;
            _ray.direction = transform.forward;
            _cachedTransform = transform;
            IsReady = true;
        }

        /// <summary>
        /// Sets the ray manually.
        /// </summary>
        public void SetRay(Vector3 origin, Vector3 direction)
        {
            _ray.origin = origin;
            _ray.direction = direction;
            IsReady = true;
        }

        /// <summary>
        /// Sets the ray using a Ray object.
        /// </summary>
        public void SetRay(Ray ray)
        {
            _ray = ray;
            IsReady = true;
        }

        /// <summary>
        /// Draws the current ray for debugging.
        /// </summary>
        public void DrawRay(float distance, Color color)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * distance, color);
        }

        /// <summary>
        /// Casts the ray.
        /// </summary>
        public bool Cast()
        {
            return CastInternal(float.MaxValue, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
        }

        /// <summary>
        /// Casts the ray with a max distance.
        /// </summary>
        public bool Cast(float distance)
        {
            return CastInternal(distance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.UseGlobal);
        }

        /// <summary>
        /// Casts the ray with a max distance and layer mask.
        /// </summary>
        public bool Cast(float distance, LayerMask layerMask)
        {
            return CastInternal(distance, layerMask, QueryTriggerInteraction.UseGlobal);
        }

        /// <summary>
        /// Casts the ray ignoring trigger colliders.
        /// </summary>
        public bool CastIgnoreTrigger(float distance, LayerMask layerMask)
        {
            return CastInternal(distance, layerMask, QueryTriggerInteraction.Ignore);
        }

        private bool CastInternal(float distance, LayerMask layerMask, QueryTriggerInteraction queryTriggerInteraction)
        {
            if (!IsReady)
            {
                Debug.LogWarning("Ray is not ready. Call SetRay() first.");
                return false;
            }

            if (UseCachedTransform && _cachedTransform != null)
            {
                _ray.origin = _cachedTransform.position;
                _ray.direction = _cachedTransform.forward;
            }

            switch (_option)
            {
                case RaycastOption.NonAlloc:
                    HitCount = Physics.RaycastNonAlloc(_ray, Hits, distance, layerMask, queryTriggerInteraction);
                    break;
                case RaycastOption.Normal:
                    if (Physics.Raycast(_ray, out RaycastHit hit, distance, layerMask, queryTriggerInteraction))
                    {
                        Hits[0] = hit;
                        HitCount = 1;
                    }
                    else
                    {
                        HitCount = 0;
                    }
                    break;
            }

            return HitCount > 0;
        }
    }

    /// <summary>
    /// Raycasting method: Normal alloc or NonAlloc (optimized, requires pre-allocated buffer).
    /// </summary>
    public enum RaycastOption
    {
        NonAlloc,
        Normal
    }
}
