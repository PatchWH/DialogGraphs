using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace PWH.DialogSystem
{
    [CreateNodeMenu("DialogSystem/ChangeOpinion")]
    public class ChangeOpinionNode : PassThroughNode
    {
        public OpinionOperator @operator;
        public int value;

        public override void OnPassThrough()
        {
            if (alreadyPassedThrough)
                return;

            alreadyPassedThrough = true;

            switch (@operator)
            {
                case OpinionOperator.Set:
                    _graph.playerCharacter.opinion = value;
                    break;
                case OpinionOperator.Add:
                    _graph.playerCharacter.opinion += value;
                    break;
                case OpinionOperator.Subtract:
                    _graph.playerCharacter.opinion -= value;
                    break;
            }
        }
    }

    public enum OpinionOperator
    {
        Set,
        Add,
        Subtract
    }
}