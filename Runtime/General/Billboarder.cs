using UnityEngine;

public class Billboarder : MonoBehaviour
{
    public Transform cameraTransform;
    [SerializeField] private bool autoUseMain = true;

    private Transform seftTransform;
    private Quaternion originalRoate;

    private void Start()
    {
        if (!cameraTransform && autoUseMain)
            cameraTransform = Camera.main.transform;

        seftTransform = transform;
        originalRoate = Quaternion.Euler(Vector3.zero);
    }

    private void Update()
    {
        if (!cameraTransform)
            return;

        seftTransform.rotation = cameraTransform.rotation * originalRoate;
    }
}
