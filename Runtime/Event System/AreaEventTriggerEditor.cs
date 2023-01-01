#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public static class AreaEventTriggerEditor
{
    [System.Serializable]
    public class VisualSetting
    {
        public bool enabled;
        public Color textColor = new Color(1f, 1f, 1f, 1f);
        public Color areaColor = new Color(1f, 1f, 1f, 1f);
        public Color lineColor = new Color(1f, 1f, 1f, 1f);
        public BoxCollider collider;
    }

    private static GameObject _eventConnect_Ref;

    public static void DrawAreaVisual(VisualSetting visualSetting, GameObject gameObject)
    {
        if (!visualSetting.enabled)
            return;

        if (!visualSetting.collider)
            return;

        Handles.color = visualSetting.textColor;
        Handles.Label(gameObject.transform.position, gameObject.name);


        Gizmos.color = visualSetting.areaColor;
        Gizmos.DrawCube(visualSetting.collider.bounds.center, visualSetting.collider.bounds.size);
    }

    public static void DrawConnectVisual(UnityEvent onEnter, UnityEvent onExit, VisualSetting visualSetting, Transform transform)
    {
        DrawConnectLine(onEnter, visualSetting, transform);
        DrawConnectLine(onExit, visualSetting, transform);
    }

    private static void DrawConnectLine(UnityEvent unityEvent, VisualSetting visualSetting, Transform transform)
    {
        int eventCount = unityEvent.GetPersistentEventCount();

        if (eventCount == 0)
            return;

        for (int i = 0; i < eventCount; i++)
        {
            _eventConnect_Ref = unityEvent.GetPersistentTarget(i) as GameObject;

            if (!_eventConnect_Ref)
                continue;

            Handles.color = visualSetting.lineColor;
            Handles.DrawDottedLine(transform.position, _eventConnect_Ref.transform.position, 10f);

            Handles.color = visualSetting.textColor;
            Handles.Label(_eventConnect_Ref.transform.position, _eventConnect_Ref.name + " | " + unityEvent.GetPersistentMethodName(i));
        }
    }
}
#endif