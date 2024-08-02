#if UNITY_EDITOR
using UnityEngine;

public class RaySerializer : MonoBehaviour
{
    [SerializeField] private float distance = 1f;
    [SerializeField] private Color color = Color.green;
    [SerializeField] private Transform[] direstions = new Transform[0];

    private void OnDrawGizmos()
    {
        for(int i = 0; i < direstions.Length; i++)
        {
            DrawRay(direstions[i]);
        }
    }

    public void DrawRay(Transform direction)
    {
        if (!direction)
            return;

        Vector3 origin = direction.position;
        Vector3 targetPoint = origin + (direction.forward * distance);

        Gizmos.color = color;
        Gizmos.DrawLine(origin, targetPoint);
    }
}
#endif
