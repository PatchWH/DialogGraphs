using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PWH.ScriptableArcitecture
{
    [CreateAssetMenu(menuName = "ScriptableArcitecture/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> listeners = new List<GameEventListener>();

        [TextArea]
        [SerializeField] string note;

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(this);
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}