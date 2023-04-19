using UnityEngine;


namespace Modules.EventSystem
{    public interface IEventListener
    {
        void OnEvent(EVENT_TYPE _event_type , Component _sender, object _param = null);
    }

    public enum EVENT_TYPE
    {
        GAME_INIT,
        GAME_START, 
        GAME_STOP,
    }
}
