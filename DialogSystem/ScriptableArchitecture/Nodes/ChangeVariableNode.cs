using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using PWH.ScriptableArcitecture;

namespace PWH.DialogSystem
{
    public abstract class ChangeVariableNode<T> : PassThroughNode
    {
        public ValueAsset<T> valueAsset;
        public T newValue;

        public override void OnPassThrough()
        {
            if (alreadyPassedThrough)
                return;

            alreadyPassedThrough = true;

            valueAsset.Value = newValue;
        }
    }
}