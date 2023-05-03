using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Modules.EventSystem
{    
    public class EventManager : GameManager, IEventListener 
    { 
        private Dictionary<EVENT_TYPE, List<IEventListener>> listeners = new Dictionary<EVENT_TYPE, List<IEventListener>>();

        public void OnEvent(EVENT_TYPE _event_type, Component _sender, object _param = null)
        {
            
        }

        private void OnEnable()
        {
            
        }

        public void AddListener(EVENT_TYPE event_type, IEventListener _listener)
        {
            List<IEventListener> _listenList = null;
            if(listeners.TryGetValue(event_type, out _listenList))
            {
                _listenList.Add(_listener);
                return;
            }

            _listenList = new List<IEventListener>();
            _listenList.Add(_listener);
            listeners.Add(event_type, _listenList);
        }

        public void PostNotification(EVENT_TYPE event_type, Component _sender, object _param = null)
        {
            List<IEventListener> _listenList = null;
            if(!listeners.TryGetValue(event_type, out _listenList))
            {
                return;
            }
            for (int i = 0; i < _listenList.Count; ++i)
            {
                _listenList?[i].OnEvent(event_type,_sender, _param);
            }
        }

        public void RemoveEvent(EVENT_TYPE event_type) => listeners.Remove(event_type);
    }
}

