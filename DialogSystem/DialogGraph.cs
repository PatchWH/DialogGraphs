using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using XNode;


namespace PWH.DialogSystem
{
    [CreateAssetMenu(menuName = "DialogSystem/Graph")]
    public class DialogGraph : NodeGraph
    {
        public StartNode startNode;
        public Character playerCharacter => startNode.playerCharacter;
        MessageNode _current;
        public MessageNode current
        {
            get { return _current; }
            set { OnCurrentSet(value); _current = value; }
        }
        [System.NonSerialized]
        SubGraphNode currentSubGraphNode = null;
        
        public StartNode Start()
        {
            foreach(Node node in nodes)
            {
                if(node is DialogNode dialogNode)
                {
                    dialogNode.Reset();
                }
            }

            startNode = nodes.FirstOrDefault(x => x is StartNode) as StartNode;

            foreach(PassThroughNode node in nodes.Where(x => x is PassThroughNode passThroughNode))
            {
                node.alreadyPassedThrough = false;
            }

            if (startNode == null)
            {  
                Debug.LogWarning("DialogGraph has no RecipientNode and therefor no place to start");
                return null;
            }

            current = null;
            return startNode;
        }

        public void OnCurrentSet(MessageNode value)
        {
            if (value == null || playerCharacter == null)
                return;

            if (value.to == null || value.speaker == null)
                return;

            if (value.to == playerCharacter || value.speaker != playerCharacter)
                return;

            value.to.ApplyOpinions(value.purpose, value.tone);
        }

        public MessageNode GetNext()
        {
            DialogNode next = null;

            if (currentSubGraphNode != null)
            {
                next = StepSubgraph();
            }
            else
            {
                if (current == null)
                {
                    next = startNode.GetNext() as MessageNode;
                }
                else
                {
                    next = current.GetNext();

                    Debug.Log(next);

                    if (next is SubGraphNode subGraphNode)
                    {
                        currentSubGraphNode = subGraphNode;
                        next = StepSubgraph();
                    }
                }
            }

            current = next as MessageNode;
            return next as MessageNode;
        }

        public MessageNode StepSubgraph()
        {
            MessageNode next = currentSubGraphNode.GetNext() as MessageNode;
            if (currentSubGraphNode.finished)
            {
                currentSubGraphNode = null;
            }
            return next;
        }

        public MessageNode PeekNext()
        {
            if (current == null)
                return startNode.GetNext() as MessageNode;

            return current.GetNext() as MessageNode;
        }

        public MessageNode DoChoice(int choiceIndex)
        {
            if(currentSubGraphNode != null)
            {
                return currentSubGraphNode.DoChoice(choiceIndex) as MessageNode;
            }

            MessageNode next = current.DoChoice(choiceIndex) as MessageNode;
            current = next;
            return next;
        }
    }
}