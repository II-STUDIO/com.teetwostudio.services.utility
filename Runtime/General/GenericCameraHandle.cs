using System;
using UnityEngine;

namespace Services
{
    /// <summary>
    /// The usibility of the camera property and funtion.
    /// </summary>
    [Serializable]
    public struct GenericCameraHandle
    {
        public GenericCameraHandle(Camera camera)
        {
            _camera = camera;
        }

        [SerializeField] private Camera _camera;

        public Camera CameraRef
        { 
            get => _camera; 
        }

        /// <summary>
        /// Automatic use main camera when the camera reference field is null or empty.
        /// </summary>
        public void AutoDetect(bool forceUseMain = false)
        {
            if (forceUseMain)
            {
                _camera = Camera.main;
                return;
            }

            if (_camera)
                return;

            _camera = Camera.main;
        }

        public Vector3 GetMouseWorldPoint()
        {
            if (!_camera)
                Debug.LogErrorFormat("Please assing camera or initialize the handle");

            return ScreenPointToWorld(Input.mousePosition);
        }

        public Ray GetMouseRay()
        {
            if (!_camera)
                Debug.LogErrorFormat("Please assing camera or initialize the handle");

            return ScreenPointToRay(Input.mousePosition);
        }

        public Ray ScreenPointToRay(Vector3 screenPoint)
        {
            if (!_camera)
                Debug.LogErrorFormat("Please assing camera or initialize the handle");

            return _camera.ScreenPointToRay(screenPoint);
        }

        public Vector3 ScreenPointToWorld(Vector3 screenPoint)
        {
            if (!_camera)
                Debug.LogErrorFormat("Please assing camera or initialize the handle");

            return _camera.ScreenToWorldPoint(screenPoint);
        }

        public Vector3 WorldToScreenPoint(Vector3 worldPoint)
        {
            if (!_camera)
                Debug.LogErrorFormat("Please assing camera or initialize the handle");

            return _camera.WorldToScreenPoint(worldPoint);
        }
    }
}