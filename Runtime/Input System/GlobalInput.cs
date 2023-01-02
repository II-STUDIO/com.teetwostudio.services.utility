using UnityEngine;

namespace Services.InputSystem
{
    public class GlobalInput : MonoBehaviour
    {
        private static GlobalInput _instance;

        public static GlobalInput Instance
        {
            get
            {
                if (!_instance)
                    _instance = new GameObject("Input - Global").AddComponent<GlobalInput>();

                return _instance;
            }
        }

        public KeyboardInputHandle Keyboad { get; private set; } = new KeyboardInputHandle();
        public MouseInputHandle Mouse { get; private set; } = new MouseInputHandle();
        public AxisInputHandle Axis { get; private set; } = new AxisInputHandle();

        private void Update()
        {
            Keyboad.Update();
            Mouse.Update();
        }
    }
}