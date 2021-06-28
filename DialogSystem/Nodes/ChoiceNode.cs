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

        [TextArea, Input(ShowBackingValue.Unconnected)]
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

        PWH.ScriptableArcitecture.ValueAsset<string> connectedValueAsset = null;

        protected override void Init()
        {
            if (connectedValueAsset)
            {
                connectedValueAsset.OnValueChanged += OnConnectedValueAssetChanged;
                text = connectedValueAsset.Value;
            }
        }

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (to.fieldName == "text")
            {
                object outputValue = from.GetOutputValue();

                if (outputValue != null && outputValue is PWH.ScriptableArcitecture.StringVariable stringVariable)
                {
                    connectedValueAsset = stringVariable;
                    text = connectedValueAsset.Value;
                }
            }
        }

        void OnConnectedValueAssetChanged()
        {
            text = connectedValueAsset.Value;
        }

        public override void OnRemoveConnection(NodePort port)
        {
            if (port.fieldName == "text" && connectedValueAsset != null)
            {
                connectedValueAsset.OnValueChanged -= OnConnectedValueAssetChanged;
                connectedValueAsset = null;
            }
        }
    }
}