using UnityEngine;
using UnityEditor;

namespace Services.EventsSystem.ScriptEditor
{
    public class AreaTriggerEditorMenu
    {
        [MenuItem("GameObject/3D Object/Area Event/Box Area")]
        public static void CreateAreaEventTriggerBox()
        {
            Transform cameraEditor = Camera.main.transform;
            GameObject gameObject = new GameObject("Box Area");
            gameObject.AddComponent(typeof(AreaEventTrigger));

            BoxCollider area = (BoxCollider)gameObject.AddComponent(typeof(BoxCollider));
            area.isTrigger = true;

            gameObject.transform.position = cameraEditor.position + (cameraEditor.forward.normalized * 10f);
        }
    }
}