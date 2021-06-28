using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PWH.ScriptableArcitecture;
using XNode;

namespace PWH.DialogSystem
{
    public abstract class VariableNode<T> : Node
    {
        [Output(ShowBackingValue.Always)]
        public ValueAsset<T> valueAsset;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(valueAsset))
                if(valueAsset)
                    return valueAsset;
           return null;
        }
    }
}