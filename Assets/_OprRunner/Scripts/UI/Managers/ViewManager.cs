using _OprRunner.Scripts.Core.EventBus;
using UnityEngine;

namespace _OprRunner.Scripts.UI.Managers
{
    public class ViewManager : MonoBehaviour
    {
        [SerializeField] private Canvas[] _views;

        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnSubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameEventBus.OnViewChanged += ActivateView;
        }

        private void UnSubscribeToEvents()
        {
            GameEventBus.OnViewChanged -= ActivateView;
        }

        public void ActivateView(int id)
        {
            if (id >= _views.Length)
                return;
            foreach (var view in _views)
                view.enabled = false;
            _views[id].enabled = true;
        }
    }
}