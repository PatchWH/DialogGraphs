using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    public abstract class PassThroughNode : DialogNode
    {
        public abstract void OnPassThrough();

        [System.NonSerialized]
        public bool alreadyPassedThrough = false;

        public override DialogNode GetNext()
        {
            List<NodePort> connections = exitPort.GetConnections();

            foreach(NodePort connection in connections)
            {
                if (connection.node is PassThroughNode passThroughNode)
                {
                    passThroughNode.OnPassThrough();

                    return passThroughNode.GetNext();
                }
                else if(connection.node is DialogNode dialogNode)
                {
                    return dialogNode;
                }
            }

            return null;
        }

        public override void Reset()
        {
            alreadyPassedThrough = false;
        }
    }
}