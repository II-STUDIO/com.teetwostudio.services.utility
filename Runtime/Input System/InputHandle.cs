using Services.EventsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Services.InputSystem
{
    public class KeyboardInputHandle
    {
        struct InputInfo
        {
            public KeyCode key;
            public EventAction onKeyDown;
            public EventAction onKeyUp;
        }

        public bool IsActive { get; private set; } = true;

        private List<InputInfo> _inputInfosList = new List<InputInfo>();
        private int _inputInfoCount = 0;

        private InputInfo _inputInfo_Ref;

        public void Active(bool value)
        {
            IsActive = value;
        }

        public void AddOnKeyDown(KeyCode key,EventAction action)
        {
            _inputInfosList.Add(new InputInfo { key = key, onKeyDown = action });
            _inputInfoCount++;
        }

        public void AddOnKeyUp(KeyCode key, EventAction action)
        {
            _inputInfosList.Add(new InputInfo { key = key, onKeyUp = action });
            _inputInfoCount++;
        }

        public void Update()
        {
            if (!IsActive)
                return;

            if (_inputInfoCount == 0)
                return;

            for (int i = 0; i < _inputInfoCount; i++)
            {
                _inputInfo_Ref = _inputInfosList[i];

                if (_inputInfo_Ref.onKeyDown != null)
                    CheckKeyDown();

                if (_inputInfo_Ref.onKeyUp != null)
                    CheckKeyUp();
            }
        }

        private void CheckKeyDown()
        {
            if (!Input.GetKeyDown(_inputInfo_Ref.key))
                return;

            _inputInfo_Ref.onKeyDown.Invoke();
        }

        private void CheckKeyUp()
        {
            if(!Input.GetKeyUp(_inputInfo_Ref.key))
                return;

            _inputInfo_Ref.onKeyUp.Invoke();
        }
    }

    public class MouseInputHandle
    {
        struct InputInfo
        {
            public int key;
            public EventAction onMouseDown;
            public EventAction onMouseUp;
        }

        public bool IsActive { get; private set; } = true;

        private List<InputInfo> _inputInfosList = new List<InputInfo>();
        private int _inputInfoCount = 0;

        private InputInfo _inputInfo_Ref;

        public void AddOnMouseDown(MouseKey key, EventAction action)
        {
            _inputInfosList.Add(new InputInfo { key = (int)key, onMouseDown = action });
            _inputInfoCount++;
        }

        public void AddOnMouseUp(MouseKey key, EventAction action)
        {
            _inputInfosList.Add(new InputInfo { key = (int)key, onMouseUp = action });
            _inputInfoCount++;
        }

        public void Active(bool value)
        {
            IsActive = value;
        }

        public void Update()
        {
            if (!IsActive)
                return;

            if (_inputInfoCount == 0)
                return;

            for (int i = 0; i < _inputInfoCount; i++)
            {
                _inputInfo_Ref = _inputInfosList[i];

                if (_inputInfo_Ref.onMouseDown != null)
                    CheckMouseDown();

                if (_inputInfo_Ref.onMouseUp != null)
                    CheckMouseUp();
            }
        }

        private void CheckMouseDown()
        {
            if (!Input.GetMouseButtonDown(_inputInfo_Ref.key))
                return;

            _inputInfo_Ref.onMouseDown.Invoke();
        }

        private void CheckMouseUp()
        {
            if (!Input.GetMouseButtonUp(_inputInfo_Ref.key))
                return;

            _inputInfo_Ref.onMouseUp.Invoke();
        }
    }

    public enum MouseKey
    {
        Left,
        Right,
        Middle
    }

    public class AxisInputHandle
    {
        private const string HorizontalKey = "Horizontal";
        private const string VerticalKey = "Vertical";
        private const string MouseXKey = "Mouse X";
        private const string MouseYKey = "Mouse Y";

        public bool IsActive { get; private set; } = true;

        public void Active(bool value)
        {
            IsActive = value;
        }

        #region Horizontal
        public float Horizontal
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxis(HorizontalKey);
            }
        }


        public float HorizontalRaw
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxisRaw(HorizontalKey);
            }
        }
        #endregion


        #region Vertical
        public float Vertical
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxis(VerticalKey);
            }
        }

        public float VerticalRaw
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxisRaw(VerticalKey);
            }
        }
        #endregion


        #region MouseX
        public float MouseX
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxis(MouseXKey);
            }
        }

        public float MouseXRaw
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxisRaw(MouseXKey);
            }
        }
        #endregion


        #region MouseY
        public float MouseY
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxis(MouseYKey);
            }
        }

        public float MouseYRaw
        {
            get
            {
                if (!IsActive)
                    return 0f;

                return Input.GetAxisRaw(MouseYKey);
            }
        }
        #endregion
    }
}