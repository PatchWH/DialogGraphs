using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/OpinionCheck")]
    public class OpinionCheckNode : PassThroughNode
    {
        public Character character;
        public BooleanOperators @operator;
        public int value;

        [Output]
        public Empty @else;

        public override void OnPassThrough()
        {
            return;
        }

        public override DialogNode GetNext()
        {
            switch (@operator)
            {
                case BooleanOperators.Equals:
                    if (_graph.playerCharacter.opinion == value)
                        return base.GetNext();
                    break;
                case BooleanOperators.Greater:
                    if (_graph.playerCharacter.opinion > value)
                        return base.GetNext();
                    break;
                case BooleanOperators.Less:
                    if (_graph.playerCharacter.opinion < value)
                        return base.GetNext();
                    break;
                case BooleanOperators.GreaterEqual:
                    if (_graph.playerCharacter.opinion >= value)
                        return base.GetNext();
                    break;
                case BooleanOperators.LessEqual:
                    if (_graph.playerCharacter.opinion <= value)
                        return base.GetNext();
                    break;
                default:
                    return GetElseNext();
            }

            return GetElseNext();
        }

        public DialogNode GetElseNext()
        {
            List<NodePort> connections = GetOutputPort("else").GetConnections();

            foreach (NodePort connection in connections)
            {
                if (connection.node is PassThroughNode passThroughNode)
                {
                    passThroughNode.OnPassThrough();

                    return passThroughNode.GetNext();
                }
                else if (connection.node is MessageNode messageNode)
                {
                    return messageNode;
                }
            }

            return null;
        }

        public override List<ChoiceNode> GetChoices()
        {
            switch (@operator)
            {
                case BooleanOperators.Equals:
                    if (_graph.playerCharacter.opinion == value)
                        return base.GetChoices();
                    break;
                case BooleanOperators.Greater:
                    if (_graph.playerCharacter.opinion > value)
                        return base.GetChoices();
                    break;
                case BooleanOperators.Less:
                    if (_graph.playerCharacter.opinion < value)
                        return base.GetChoices();
                    break;
                case BooleanOperators.GreaterEqual:
                    if (_graph.playerCharacter.opinion >= value)
                        return base.GetChoices();
                    break;
                case BooleanOperators.LessEqual:
                    if (_graph.playerCharacter.opinion <= value)
                        return base.GetChoices();
                    break;
                default:
                    return GetElseNext().GetChoices();
            }

            DialogNode next = GetElseNext();
            if (next != null)
                return GetElseNext().GetChoices();
            else
                return null;
        }
    }

    public enum BooleanOperators
    {
        Equals,
        Greater,
        Less,
        GreaterEqual,
        LessEqual,
    }
}