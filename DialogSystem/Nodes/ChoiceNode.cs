using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/Choice")]
    public class ChoiceNode : DialogNode
    {
        [Tooltip("This only has to be unique in a set of choices, aka you can have two choices with index 0, as long as they are not tied to the same MessageNode")]
        public int index;

        [TextArea]
        public string text;

        public override DialogNode GetNext()
        {
            List<NodePort> connections = exitPort.GetConnections();

            if(exitPort.Connection == null)
            {
                return null;
            }

            if (exitPort.Connection.node is PassThroughNode passThroughNode)
            {
                passThroughNode.OnPassThrough();
                return passThroughNode.GetNext();
            }
            else if(exitPort.Connection.node is MessageNode messageNode)
            {
                return messageNode;
            }

            return null;
        }
    }
}