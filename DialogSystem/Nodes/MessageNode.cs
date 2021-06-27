using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/Message")]
    public class MessageNode : DialogNode
    {
        public Character speaker;
        public Character to;
        public Purpose purpose;
        public Tone tone;

        [TextArea]
        public string text;

        public float delay;

        public override DialogNode GetNext()
        {
            List<NodePort> connections = exitPort.GetConnections();

            if (GetChoices() != null)
            {
                return this;
            }

            if(exitPort.Connection != null)
            {
                if(exitPort.Connection.node is PassThroughNode passThroughNode)
                {
                    passThroughNode.OnPassThrough();
                    return passThroughNode.GetNext();
                }
                else if (exitPort.Connection.node is DialogNode dialogNode)
                {
                    return dialogNode;
                }
            }

            return null;
        }
    }
}