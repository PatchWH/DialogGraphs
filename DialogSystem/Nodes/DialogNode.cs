using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    public abstract class DialogNode : Node
    {
        [Input] public Empty entry;
        [Output] public Empty exit;

        public NodePort exitPort => GetOutputPort("exit");

        public abstract DialogNode GetNext();

        public DialogGraph _graph => graph as DialogGraph;

        public virtual DialogNode DoChoice(int choiceIndex)
        {
            if (GetChoices() == null)
            {
                Debug.LogWarning("Tried to DoChoice on Node with no choices");
                return null;
            }
            else
            {
                foreach (ChoiceNode choiceNode in GetChoices())
                {
                    if (choiceNode.index == choiceIndex)
                    {
                        return choiceNode.GetNext();
                    }
                }
            }
            return null;
        }

        public virtual List<ChoiceNode> GetChoices()
        {
            List<ChoiceNode> choiceNodes = new List<ChoiceNode>();

            foreach(NodePort connection in exitPort.GetConnections())
            {
                if(connection.node is PassThroughNode passThroughNode)
                {
                    passThroughNode.OnPassThrough();

                    List<ChoiceNode> choices = passThroughNode.GetChoices();
                    if (choices != null)
                    {
                        foreach (ChoiceNode choiceNode in choices)
                        {
                            if (choiceNode == null)
                                continue;
                            choiceNodes.Add(choiceNode);
                        }
                    }
                }
                else if(connection.node is ChoiceNode choiceNode)
                {
                    choiceNodes.Add(choiceNode);
                }
            }

            if (choiceNodes.Count == 0)
                return null;

            return choiceNodes;
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

        public virtual void Reset()
        {}
    }

    [System.Serializable]
    public class Empty { }
}
