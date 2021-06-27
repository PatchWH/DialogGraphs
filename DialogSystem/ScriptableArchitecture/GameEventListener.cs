using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace PWH.ScriptableArcitecture
{
    public class GameEventListener : MonoBehaviour
    {
        public List<eventResponseDuo> eventResponseDuos = new List<eventResponseDuo>();

        public void OnEnable()
        {
            foreach(eventResponseDuo duo in eventResponseDuos)
            {
                duo.gameEvent.RegisterListener(this);
            }
        }

        public void OnDisable()
        {
            foreach (eventResponseDuo duo in eventResponseDuos)
            {
                duo.gameEvent.UnregisterListener(this);
            }
        }

        public void OnEventRaised(GameEvent _event)
        {
            List<eventResponseDuo> eventDuos = eventResponseDuos.Where(x => x.gameEvent == _event).ToList();

            foreach (eventResponseDuo duo in eventDuos)
            {
                duo.response?.Invoke();
            }
        }
    }

    [System.Serializable]
    public struct eventResponseDuo
    {
        public GameEvent gameEvent;
        public UnityEvent response;
    }
}