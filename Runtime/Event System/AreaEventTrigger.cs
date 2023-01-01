using UnityEngine;
using UnityEngine.Events;

namespace Services.EventsSystem
{
    [AddComponentMenu("Service/Utilities/AreaEvent")]
    public class AreaEventTrigger : MonoBehaviour
    {
        enum Action
        {
            None,
            Disabled,
            Destory
        }

        [Header("Fillter")]
        [SerializeField] private string[] _tags = new string[] { "Player" };

        [Header("Events")]
        [SerializeField] private UnityEvent _onEnter;
        [SerializeField] private UnityEvent _onExit;

        [Header("Self Action")]
        [SerializeField] private Action _enterAction;
        [SerializeField] private Action _exitAction;

        public UnityEvent OnEnterEvent
        {
            get => _onEnter;
        }

        public UnityEvent OnExitEvent
        {
            get => _onExit;
        }

#if UNITY_EDITOR
        [Space]
        [SerializeField] private AreaEventTriggerEditor.VisualSetting _visualSetting;

        private void OnDrawGizmos()
        {
            AreaEventTriggerEditor.DrawAreaVisual(_visualSetting, gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            AreaEventTriggerEditor.DrawConnectVisual(_onEnter, _onExit, _visualSetting, transform);
        }
#endif

        private void OnTriggerEnter(Collider other)
        {
            if (!IsValidateTag(other.tag))
                return;

            _onEnter?.Invoke();

            ExecuteSelfAction(_enterAction);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!IsValidateTag(other.tag))
                return;

            _onExit?.Invoke();

            ExecuteSelfAction(_exitAction);
        }

        private bool IsValidateTag(string targetTag)
        {
            for (int i = 0; i < _tags.Length; i++)
            {
                if (_tags[i].Equals(targetTag))
                    return true;
            }

            return false;
        }

        private void ExecuteSelfAction(Action action)
        {
            switch (action)
            {
                case Action.Disabled:
                    gameObject.SetActive(false);
                    break;
                case Action.Destory:
                    Destroy(gameObject);
                    break;
            }
        }
    }
}