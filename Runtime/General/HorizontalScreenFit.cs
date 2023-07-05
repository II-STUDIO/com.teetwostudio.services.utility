using UnityEngine;

[ExecuteInEditMode]
public class HorizontalScreenFit : MonoBehaviour
{
    [SerializeField] private bool preview = false;
    [Space]
    // Set this to the in-world distance between the left & right edges of your scene.
    [SerializeField] private float sceneWidth = 10;

    [SerializeField] private Camera _camera;

    private void Start()
    {
        Apply();
    }

#if UNITY_EDITOR
    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        if (!_camera)
            return;

        if (!preview)
            return;

        Apply();
    }
#endif

    public void Apply()
    {
        float unitsPerPixel = sceneWidth / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
    }
}
