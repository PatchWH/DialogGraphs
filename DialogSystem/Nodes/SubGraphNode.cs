using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/Sub-Graph")]
    public class SubGraphNode : DialogNode
    {
        public DialogGraph subgraph;

        [System.NonSerialized]
        public bool started = false;

        [System.NonSerialized]
        public bool finished = false;

        public override DialogNode GetNext()
        {
            if (!started)
            {
                started = true;
                subgraph.Start();
            }
            
            MessageNode next = subgraph.GetNext();


            if (next == null)
            {
                finished = true;
            }
            
            if(finished)
            {
                if (exitPort.Connection != null)
                {
                    if (exitPort.Connection.node is PassThroughNode passThroughNode)
                    {
                        passThroughNode.OnPassThrough();
                        return passThroughNode.GetNext();
                    }
                    else if (exitPort.Connection.node is MessageNode messageNode)
                    {
                        return messageNode;
                    }
                }

                return null;
            }

            return next;
        }

        public override DialogNode DoChoice(int choiceIndex)
        {
            return subgraph.DoChoice(choiceIndex);
        }

        public override void Reset()
        {
            started = false;
            finished = false;
        }
    }
}