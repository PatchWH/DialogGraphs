using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using PWH.ScriptableArcitecture;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/ScriptableArcitecture/RaiseGameEvent")]
    public class RaiseGameEventNode : PassThroughNode
    {
        public GameEvent gameEvent;

        public override void OnPassThrough()
        {
            if (alreadyPassedThrough)
                return;

            alreadyPassedThrough = true;

            gameEvent.Raise();
        }
    }
}